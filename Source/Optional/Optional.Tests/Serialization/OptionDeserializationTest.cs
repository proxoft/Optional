using System.Text.Json;
using Proxoft.Optional.Serialization;

namespace Proxoft.Optional.Tests.Serialization;

public class OptionDeserializationTest
{
    JsonSerializerOptions _options;

    public OptionDeserializationTest()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new OptionJsonConverter<decimal>());
        _options.Converters.Add(new OptionJsonConverter<Foo>());
        _options.Converters.Add(new OptionJsonConverter<decimal[]>());
    }

    [Fact]
    public void DeserializeSomeDecimal()
    {
        string json = "{\"$optional\":\"some\",\"value\":3.14}";
        Option<decimal>? mayBe = JsonSerializer.Deserialize<Option<decimal>>(json, _options);

        mayBe
            .Should()
            .NotBeNull();

        mayBe
            .Should()
            .BeAssignableTo<Some<decimal>>();

        Some<decimal> some = (Some<decimal>)mayBe!;
        some
            .Map(x => x)
            .Reduce(0)
            .Should()
            .Be(3.14m);
    }

    [Fact]
    public void DeserializeNoneDecimal()
    {
        string json = "{\"$optional\":\"none\"}";
        Option<decimal>? mayBe = JsonSerializer.Deserialize<Option<decimal>>(json, _options);

        mayBe.Should().NotBeNull();
        mayBe
            .Should()
            .BeAssignableTo<None<decimal>>();
    }

    [Fact]
    public void DeserializeSomeFoo()
    {
        string json = "{\"$optional\":\"some\",\"value\":{\"FooName\":\"Parent\",\"Child\":{\"FooName\":\"Child\",\"Child\":null}}}";

        Option<Foo>? maybe = JsonSerializer.Deserialize<Option<Foo>>(json, _options);

        maybe
            .Should()
            .NotBeNull();

        maybe
            .Should()
            .BeAssignableTo<Some<Foo>>();

        Foo foo = (Some<Foo>)maybe!;

        foo.FooName
            .Should()
            .Be("Parent");

        foo.Child
            .Should()
            .NotBeNull();

        foo.Child!
            .FooName
            .Should()
            .Be("Child");

        foo.Child
            .Child
            .Should()
            .BeNull();
    }

    [Fact]
    public void DeserializeSomeDecimalArray()
    {
        string json = "{\"$optional\":\"some\",\"value\":[1.8,6.2,9.4]}";

        Option<decimal[]>? maybe = JsonSerializer.Deserialize<Option<decimal[]>>(json, _options);

        maybe
            .Should()
            .NotBeNull();

        maybe
            .Should()
            .BeAssignableTo<Some<decimal[]>>();

        decimal[] numbers = (Some<decimal[]>)maybe!;
        numbers
            .Should()
            .BeEquivalentTo([1.8m, 6.2m, 9.4m]);
        }
}
