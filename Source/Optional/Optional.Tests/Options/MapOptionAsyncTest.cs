namespace Proxoft.Optional.Tests.Options;

public class MapOptionAsyncTest
{
    [Fact]
    public async Task GiveSomeMapOptionWhenReturnsSome1()
    {
        Option<int> option = 15;

        Option<decimal> mapped = await Task.FromResult(option)
            .MapOption(v => Task.FromResult(Guarded(v)));

        mapped
           .Should()
           .BeOfType<Some<decimal>>();

        decimal v = (Some<decimal>)mapped;
        v.Should()
         .Be(30);
    }

    [Fact]
    public async Task GiveSomeMapOptionWhenReturnsSome2()
    {
        Option<int> option = 15;

        Option<decimal> mapped = await option
            .MapOption(v => Task.FromResult(Guarded(v)));

        mapped
           .Should()
           .BeOfType<Some<decimal>>();

        decimal v = (Some<decimal>)mapped;
        v.Should()
         .Be(30);
    }

    [Fact]
    public async Task GiveSomeMapOptionWhenReturnsSome3()
    {
        Option<int> option = 15;

        Option<decimal> mapped = await Task.FromResult(option)
            .MapOption(v => Guarded(v));

        mapped
           .Should()
           .BeOfType<Some<decimal>>();

        decimal v = (Some<decimal>)mapped;
        v.Should()
         .Be(30);
    }

    [Fact]
    public async Task GiveNoneMapOption1()
    {
        Option<int> option = None.Instance;

        Option<decimal> mapped = await Task.FromResult(option)
            .MapOption(v => Task.FromResult(GuardedThrows(v)));

        mapped
           .Should()
           .BeOfType<None<decimal>>();
    }

    [Fact]
    public async Task GiveNoneMapOption2()
    {
        Option<int> option = None.Instance;

        Option<decimal> mapped = await Task.FromResult(option)
            .MapOption(GuardedThrows);

        mapped
           .Should()
           .BeOfType<None<decimal>>();
    }

    [Fact]
    public async Task GiveNoneMapOption3()
    {
        Option<int> option = None.Instance;

        Option<decimal> mapped = await option
            .MapOption(v => Task.FromResult(GuardedThrows(v)));

        mapped
           .Should()
           .BeOfType<None<decimal>>();
    }

    static Option<decimal> Guarded(int value)
    {
        return value > 10 ? value * 2m : None.Instance;
    }

    static Option<decimal> GuardedThrows(int value)
    {
        throw new Exception("execution shouldn't reach this point");
    }
}
