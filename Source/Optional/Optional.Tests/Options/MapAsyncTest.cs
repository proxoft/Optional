namespace Proxoft.Optional.Tests.Options;

public class MapAsyncTest
{
    [Fact]
    public async Task GivenSome_MapAsync1()
    {
        Option<int> option = 10;

        Option<string> mapped = await Task.FromResult(option)
            .Map(x => Task.FromResult(x.ToString()));

        mapped
            .Should()
            .BeOfType<Some<string>>();

        string v = (Some<string>)mapped;
        v.Should()
         .Be("10");
    }

    [Fact]
    public async Task GivenSome_MapAsync2()
    {
        Option<int> option = 10;

        Option<string> mapped = await Task.FromResult(option)
            .Map(x => x.ToString());

        mapped
            .Should()
            .BeOfType<Some<string>>();

        string v = (Some<string>)mapped;
        v.Should()
         .Be("10");
    }

    [Fact]
    public async Task GivenSome_MapAsync3()
    {
        Option<int> option = 10;

        Option<string> mapped = await option
            .Map(x => Task.FromResult(x.ToString()));

        mapped
            .Should()
            .BeOfType<Some<string>>();

        string v = (Some<string>)mapped;
        v.Should()
         .Be("10");
    }

    [Fact]
    public async Task MapWhenNoneAsync1()
    {
        Option<int> option = None.Instance;

        Option<string> mapped = await Task.FromResult(option)
            .Map(x => Task.FromResult(x.ToString()));

        mapped
            .Should()
            .BeOfType<None<string>>();
    }

    [Fact]
    public async Task MapWhenNoneAsync2()
    {
        Option<int> option = None.Instance;

        Option<string> mapped = await option
            .Map(x => Task.FromResult(x.ToString()));

        mapped
            .Should()
            .BeOfType<None<string>>();
    }

    [Fact]
    public async Task MapWhenNoneAsync3()
    {
        Option<int> option = None.Instance;

        Option<string> mapped = await Task.FromResult(option)
            .Map(x => x.ToString());

        mapped
            .Should()
            .BeOfType<None<string>>();
    }
}
