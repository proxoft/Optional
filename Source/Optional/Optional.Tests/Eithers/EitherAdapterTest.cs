namespace Proxoft.Optional.Tests.Eithers;

public class EitherAdapterTest
{
    [Fact]
    public void RightMapsTopOption1()
    {
        Either<string, int> x = 26;

        Option<decimal> option = x.Option(
            r => new Some<decimal>(r),
            _ => None.Instance
        );

        option
            .Should()
            .BeOfType<Some<decimal>>();

        decimal v = (Some<decimal>)option;
        v.Should()
            .Be(26);
    }

    [Fact]
    public void RightMapsTopOption2()
    {
        Either<string, int> x = 10;

        Option<int> option = x.Option(
            r => new Some<int>(r)
        );

        option
            .Should()
            .BeOfType<Some<int>>();

        int v = (Some<int>)option;
        v.Should()
            .Be(10);
    }

    [Fact]
    public void LeftMapsTopOption1()
    {
        Either<string, int> x = "y";

        Option<decimal> option = x.Option(
            r => new Some<decimal>(r),
            _ => None.Instance
        );

        option
            .Should()
            .BeOfType<None<decimal>>();
    }

    [Fact]
    public void LeftMapsTopOption2()
    {
        Either<string, int> x = "x";

        Option<int> option = x.Option(
            r => new Some<int>(r)
        );

        option
            .Should()
            .BeOfType<None<int>>();
    }
}
