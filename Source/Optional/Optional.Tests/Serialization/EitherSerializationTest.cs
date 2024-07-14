using System.Text.Json;
using Proxoft.Optional.Serialization;

namespace Proxoft.Optional.Tests.Serialization;

public class EitherSerializationTest
{
    private readonly JsonSerializerOptions _options;

    public EitherSerializationTest()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new EitherJsonConverter());
    }

    [Fact]
    public void SerializeRightOfStringInt()
    {
        Either<string, int> either = 54;

        string json = JsonSerializer.Serialize(either, _options);

        json.Should()
            .Be("{\"either\":\"right\",\"value\":54}");
    }

    [Fact]
    public void SerializeLeftOfStringInt()
    {
        Either<string, int> either = "some error";

        string json = JsonSerializer.Serialize(either, _options);

        json.Should()
            .Be("{\"either\":\"left\",\"value\":\"some error\"}");

    }

    [Fact]
    public void SerializeRightOfStringFoo()
    {
        Either<string, Foo> either = new Foo()
        {
            FooName = "First instance",
            Child = new Foo()
            {
                FooName = "Second instance"
            }
        };

        string json = JsonSerializer.Serialize(either, _options);

        json.Should()
            .Be("{\"either\":\"right\",\"value\":{\"FooName\":\"First instance\",\"Child\":{\"FooName\":\"Second instance\",\"Child\":null}}}");
    }

    [Fact]
    public void SerializeLeftOfStringFoo()
    {
        Either<string, Foo> either = "some issue";
        string json = JsonSerializer.Serialize(either, _options);

        json.Should()
            .Be("{\"either\":\"left\",\"value\":\"some issue\"}");
    }
}
