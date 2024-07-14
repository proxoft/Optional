using System.Text.Json;

namespace Proxoft.Optional.Serialization;

internal static class Utf8ReaderExtensions
{
    public static string ReadPropertyValueOfString(this ref Utf8JsonReader reader, string propertyName)
    {
        reader.ReadPropertyToken(propertyName);

        if (!reader.Read() || reader.TokenType != JsonTokenType.String)
        {
            throw new InvalidOperationException($"Error reading json: expected string token when reading property '{propertyName}'");
        }

        string value = reader.GetString() ?? "";
        return value;
    }

    public static void ReadPropertyToken(this ref Utf8JsonReader reader, string propertyName)
    {
        if (!reader.Read() || reader.TokenType != JsonTokenType.PropertyName)
        {
            throw new InvalidOperationException($"Error reading json: expected token type property with name '{propertyName}'");
        }

        string currentPropertyName = reader.GetString() ?? string.Empty;
        if (currentPropertyName != propertyName)
        {
            throw new InvalidOperationException($"Error reading json: expected property name '{propertyName}, but found {currentPropertyName}'");
        }
    }

    public static void ReadEndToken(this ref Utf8JsonReader reader)
    {
        if (!reader.Read() || reader.TokenType != JsonTokenType.EndObject)
        {
            throw new InvalidOperationException("Expected EndObject token.");
        }
    }
}
