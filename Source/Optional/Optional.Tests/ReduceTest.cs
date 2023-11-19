namespace Proxoft.Optional.Tests;

public class ReduceTest
{
    [Fact]
    public void ReduceOfRightToSameTypeAsRight()
    {
        Either<string, int> either = 10;

        int r = either
            .Reduce(_ => 0);

        r.Should()
         .Be(10);
    }

    [Fact]
    public void ReduceOfLeftToSameTypeAsRight()
    {
        Either<string, int> either = "xx";

        int r = either
            .Reduce(_ => 8);

        r.Should()
         .Be(8);
    }

    [Fact]
    public void ReduceOfRightToSameTypeAsLeft()
    {
        Either<string, int> either = 10;

        string r = either
            .Reduce(r => r.ToString());

        r.Should()
         .Be("10");
    }

    [Fact]
    public void ReduceOfLeftToSameTypeAsLeft()
    {
        Either<string, int> either = "left";

        string r = either
            .Reduce(r => r.ToString());

        r.Should()
         .Be("left");
    }

    [Fact]
    public void ReduceOfRightToNewType()
    {
        Either<double, int> either = 10;

        string r = either
            .Reduce(
                r => r.ToString(),
                l => l.ToString()
            );

        r.Should()
         .Be("10");
    }

    [Fact]
    public void ReduceOfLeftToNewType()
    {
        Either<double, int> either = 27f;

        string r = either
            .Reduce(
                r => r.ToString(),
                l => l.ToString()
            );

        r.Should()
         .Be("27");
    }
}
