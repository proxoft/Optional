namespace Proxoft.Optional.Tests.Options;

public class MapTest
{
    [Fact]
    public void MapWhenSome()
    {
        Option<int> option = 10;

        Option<string> mapped = option.Map(x => x.ToString());

        mapped
            .Should()
            .BeOfType<Some<string>>();

        string v = (Some<string>)mapped;
        v.Should()
         .Be("10");
    }

    [Fact]
    public void MapWhenNone()
    {
        Option<int> option = None.Instance;

        Option<string> mapped = option.Map(x => x.ToString());

        mapped
            .Should()
            .BeOfType<None<string>>();
    }

    [Fact]
    public void GiveSomeMapOptionWhenReturnsSome()
    {
        static Option<decimal> Guarded(int value)
        {
            return value > 10 ? value * 2m : None.Instance;
        }

        Option<int> option = 15;

        Option<decimal> mapped = option.MapOption(Guarded);

        mapped
           .Should()
           .BeOfType<Some<decimal>>();

        decimal v = (Some<decimal>)mapped;
        v.Should()
         .Be(30);
    }

    [Fact]
    public void GiveSomeMapOptionWhenReturnsNone()
    {
        static Option<decimal> Guarded(int value)
        {
            return value > 10 ? value * 2m : None.Instance;
        }

        Option<int> option = 10;

        Option<decimal> mapped = option.MapOption(Guarded);

        mapped
           .Should()
           .BeOfType<None<decimal>>();
    }

    [Fact]
    public void GiveNoneMapOption()
    {
        static Option<decimal> Guarded(int value)
        {
            throw new Exception("execution flow cannot reach this point");
        }

        Option<int> option = None.Instance;

        Option<decimal> mapped = option.MapOption(Guarded);

        mapped
           .Should()
           .BeOfType<None<decimal>>();
    }
}
