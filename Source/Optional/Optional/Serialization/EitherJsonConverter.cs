using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Proxoft.Optional.Serialization;

public class EitherJsonConverter : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        if (!typeToConvert.IsGenericType)
        {
            return false;
        }

        Type genericTypeDefinition = typeToConvert.GetGenericTypeDefinition();
        bool canConvert = genericTypeDefinition == typeof(Either<,>)
            || genericTypeDefinition == typeof(Left<,>)
            || genericTypeDefinition == typeof(Right<,>);

        return canConvert;
    }

    public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        Type[] typeArguments = typeToConvert.GetGenericArguments();
        Type leftType = typeArguments[0];
        Type rightType = typeArguments[1];

        JsonConverter converter = (JsonConverter)Activator.CreateInstance(
                typeof(EitherJsonConverter<,>).MakeGenericType([leftType, rightType]),
                BindingFlags.Instance | BindingFlags.Public,
                binder: null,
                args: [],
                culture: null)!;

        return converter;
    }
}

file class EitherJsonConverter<L, R> : JsonConverter<Either<L, R>>
{
    public override Either<L, R> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        string eitherType = reader.ReadPropertyValueOfString("either");
        reader.ReadPropertyToken("value");
        Either<L, R> result;
        switch (eitherType)
        {
            case "right":
                R rightValue = JsonSerializer.Deserialize<R>(ref reader, options) ?? throw new Exception("value cannot be null");
                result = rightValue;
                break;
            case "left":
                L leftValue = JsonSerializer.Deserialize<L>(ref reader, options) ?? throw new Exception("value cannot be null");
                result = leftValue;
                break;
            default:
                throw new ArgumentOutOfRangeException($"Unsupported either type: {eitherType}");
        }

        reader.ReadEndToken();
        return result;
    }

    public override void Write(Utf8JsonWriter writer, Either<L, R> value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value is Right<L, R> right)
        {
            writer.WriteString("either", "right");
            writer.WritePropertyName("value");
            R rightValue = right;
            JsonSerializer.Serialize(writer, rightValue, options);
        }
        else
        {
            writer.WriteString("either", "left");
            writer.WritePropertyName("value");
            L leftValue = (Left<L, R>)value;
            JsonSerializer.Serialize(writer, leftValue, options);
        }

        writer.WriteEndObject();
    }
}
