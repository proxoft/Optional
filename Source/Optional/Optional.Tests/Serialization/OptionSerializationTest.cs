using System.Text.Json;
using Proxoft.Optional.Serialization;

namespace Proxoft.Optional.Tests.Serialization;

public class OptionSerializationTest
{
    JsonSerializerOptions _options;

    public OptionSerializationTest()
    {
        _options = new JsonSerializerOptions();
        _options.Converters.Add(new OptionJsonConverter<int>());
        _options.Converters.Add(new OptionJsonConverter<Foo>());
        _options.Converters.Add(new OptionJsonConverter<int[]>());
    }

    [Fact]
    public void SerializeSomeInt()
    {
        Option<int> someInt = new Some<int>(42);

        string json = JsonSerializer.Serialize(someInt, _options);
        json
            .Should()
            .Be("{\"$optional\":\"some\",\"value\":42}");
    }

    [Fact]
    public void SerializeNoneInt()
    {
        Option<int> noneInt = None.Instance;

        string json = JsonSerializer.Serialize(noneInt, _options);
        json
            .Should()
            .Be("{\"$optional\":\"none\"}");
    }

    [Fact]
    public void SerializeNoneInt2()
    {
        Option<int> noneInt = None<int>.Instance;

        string json = JsonSerializer.Serialize(noneInt, _options);
        json
            .Should()
            .Be("{\"$optional\":\"none\"}");
    }

    [Fact]
    public void SerializeSomeFoo()
    {
        Option<Foo> someFoo = new Foo()
        {
            FooName = "Parent",
            Child = new Foo
            {
                FooName = "Child"
            }
        };

        string json = JsonSerializer.Serialize(someFoo, _options);
        json
            .Should()
            .Be("{\"$optional\":\"some\",\"value\":{\"FooName\":\"Parent\",\"Child\":{\"FooName\":\"Child\",\"Child\":null}}}");
    }

    [Fact]
    public void SerializeArrayOfInt()
    {
        Option<int[]> some = new int[] { 1, 6, 9 };
        string json = JsonSerializer.Serialize(some, _options);

        json
            .Should()
            .Be("{\"$optional\":\"some\",\"value\":[1,6,9]}");
    }
}
