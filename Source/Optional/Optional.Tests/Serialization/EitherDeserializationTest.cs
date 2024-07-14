using System.Text.Json;
using Proxoft.Optional.Serialization;

namespace Proxoft.Optional.Tests.Serialization;

public class EitherDeserializationTest
{
    private readonly JsonSerializerOptions _options;

    public EitherDeserializationTest()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new EitherJsonConverter());
    }

    [Fact]
    public void DeserializeRightOfStringInt()
    {
        string json = "{\"either\":\"right\",\"value\":54}";

        Either<string, int>? either = JsonSerializer.Deserialize<Either<string, int>>(json, _options);

        either
            .Should()
            .NotBeNull();

        either
            .Should()
            .BeAssignableTo<Right<string, int>>();

        int value = (Right<string, int>)either!;
        value
            .Should()
            .Be(54);
    }

    [Fact]
    public void DeserializeLeftOfStringInt()
    {
        string json = "{\"either\":\"left\",\"value\":\"troubles\"}";

        Either<string, int>? either = JsonSerializer.Deserialize<Either<string, int>>(json, _options);

        either
            .Should()
            .NotBeNull();

        either
            .Should()
            .BeAssignableTo<Left<string, int>>();

        string value = (Left<string, int>)either!;
        value
            .Should()
            .Be("troubles");
    }

    [Fact]
    public void DeserializeRightOfStringFoo()
    {
        string json = "{\"either\":\"right\",\"value\":{\"FooName\":\"First instance\",\"Child\":{\"FooName\":\"Second instance\",\"Child\":null}}}";

        Either<string, Foo>? either = JsonSerializer.Deserialize<Either<string, Foo>>(json, _options);

        either
            .Should()
            .NotBeNull();

        either
            .Should()
            .BeAssignableTo<Right<string, Foo>>();

        Foo value = (Right<string, Foo>)either!;
        value.FooName
            .Should()
            .Be("First instance");

        value.Child
            .Should()
            .NotBeNull();

        value.Child!
            .FooName
            .Should()
            .Be("Second instance");

        value.Child
            .Child
            .Should()
            .BeNull();
    }

    [Fact]
    public void DeserializeLeftOfStringFoo()
    {
        string json = "{\"either\":\"left\",\"value\":\"error\"}";

        Either<string, Foo>? either = JsonSerializer.Deserialize<Either<string, Foo>>(json, _options);

        either
            .Should()
            .NotBeNull();

        either
            .Should()
            .BeAssignableTo<Left<string, Foo>>();

        string value = (Left<string, Foo>)either!;
        value
            .Should()
            .Be("error");
    }
}
