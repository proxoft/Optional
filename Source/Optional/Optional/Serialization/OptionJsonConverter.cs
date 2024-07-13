using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.Optional.Serialization;

public class OptionJsonConverter<T> : JsonConverter<Option<T>>
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
        string optionalType = reader.ReadOptionalType();

        Option<T> result;
        switch (optionalType)
        {
            case "some":
                reader.ReadValuePropertyToken();
                T? value = JsonSerializer.Deserialize<T>(ref reader, options);
                if(value is null)
                {
                    throw new Exception("value cannot be null");
                }
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
            writer.WriteString("$optional", "some");
            writer.WritePropertyName("value");
            T v = (T)some;
            JsonSerializer.Serialize(writer, v, options);
        }
        else
        {
            writer.WriteString("$optional", "none");
        }
        writer.WriteEndObject();
    }
}

file static class ReaderExtensions
{
    public static string ReadOptionalType(this ref Utf8JsonReader reader)
    {
        if(!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new InvalidOperationException("Error reading token for property '$optional'");
        }

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new InvalidOperationException("Error reading token for property '$optional': expected string");
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
