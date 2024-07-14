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

file class OptionJsonConverterConstants
{
    public const string OptionPropertyName = "option";
    public const string ValuePropertyName = "value";
    public const string SomeTypeValue = "some";
    public const string NoneTypeValue = "none";
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
        string optionalType = reader.ReadOptionType();

        Option<T> result;
        switch (optionalType)
        {
            case "some":
                reader.ReadValuePropertyToken();
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
            writer.WriteString(OptionJsonConverterConstants.OptionPropertyName, OptionJsonConverterConstants.SomeTypeValue);
            writer.WritePropertyName(OptionJsonConverterConstants.ValuePropertyName);
            T v = (T)some;
            JsonSerializer.Serialize(writer, v, options);
        }
        else
        {
            writer.WriteString(OptionJsonConverterConstants.OptionPropertyName, OptionJsonConverterConstants.NoneTypeValue);
        }
        writer.WriteEndObject();
    }
}

file static class ReaderExtensions
{
    public static string ReadOptionType(this ref Utf8JsonReader reader)
    {
        if(!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new InvalidOperationException("Error reading json: expected token type property with name 'option'");
        }

        string propertyName = reader.GetString() ?? "";
        if (propertyName != OptionJsonConverterConstants.OptionPropertyName)
        {
            throw new InvalidOperationException($"Error reading json: expected property name '{OptionJsonConverterConstants.OptionPropertyName}, but found {propertyName}'");
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new InvalidOperationException($"Error reading json: expected string token when reading property '{OptionJsonConverterConstants.OptionPropertyName}'");
        }

        string optionalType = reader.GetString() ?? "";
        return optionalType;
    }

    public static void ReadValuePropertyToken(this ref Utf8JsonReader reader)
    {
        if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new InvalidOperationException("Error reading token for property '$optional'");
        }

        string propertyName = reader.GetString() ?? "";
        if (propertyName != "value")
        {
            throw new InvalidOperationException($"{propertyName} does not exist");
        }

        reader.Read();
    }

    public static void ReadEndToken(this ref Utf8JsonReader reader)
    {
        if(!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
        {
            throw new InvalidOperationException("Expected EndObject token.");
        }
    }
}
