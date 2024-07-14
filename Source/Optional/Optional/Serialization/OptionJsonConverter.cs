using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.Optional.Serialization;

public class OptionJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        Type genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();
        bool canConvert = genericTypeDefinition == typeof(Option<>)
            || genericTypeDefinition == typeof(Some<>)
            || genericTypeDefinition == typeof(None<>);

        return canConvert;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] typeArguments = typeToConvert.GetGenericArguments();
        Type contentType = typeArguments[0];

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(OptionJsonConverter<>).MakeGenericType([contentType]),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: [],
                culture: null)!;

        return converter;
    }
}

file class OptionJsonConverter<T> : JsonConverter<Option<T>>
{
    public override bool CanConvert(Type typeToConvert)
    {
        bool canConvert = typeToConvert == typeof(Option<T>)
            || typeToConvert == typeof(Some<T>)
            || typeToConvert == typeof(None<T>);

        return canConvert;
    }

    public override Option<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string optionalType = reader.ReadPropertyValueOfString("option");

        Option<T> result;
        switch (optionalType)
        {
            case "some":
                reader.ReadPropertyToken("value");
                T? value = JsonSerializer.Deserialize<T>(ref reader, options) ?? throw new Exception("value cannot be null");
                result = value;
                break;
            case "none":
                result = None<T>.Instance;
                break;
            default:
                throw new ArgumentException("allowed values are 'none' or 'some'");
        }

        reader.ReadEndToken();
        return result;
    }

    public override void Write(Utf8JsonWriter writer, Option<T> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        if (value is Some<T> some)
        {
            writer.WriteString("option", "some");
            writer.WritePropertyName("value");
            T v = (T)some;
            JsonSerializer.Serialize(writer, v, options);
        }
        else
        {
            writer.WriteString("option", "none");
        }
        writer.WriteEndObject();
    }
}