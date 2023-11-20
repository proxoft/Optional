namespace Proxoft.Optional.Tests.Eithers;

public class MapTest
{
    private readonly Either<Foo, int> _right = 45;
    private readonly Either<Foo, int> _left = Foo.F1;

    [Fact]
    public void MapToSameRightWhenRight()
    {
        var x = _right
            .Map(r => r * 2);

        x.Should()
            .BeOfType<Right<Foo, int>>();

        int v = (Right<Foo, int>)x;
        v.Should()
            .Be(90);
    }

    [Fact]
    public void MapToNewRightWhenRight()
    {
        var x = _right
            .Map(r => r.ToString());

        x.Should()
            .BeOfType<Right<Foo, string>>();

        string v = (Right<Foo, string>)x;
        v.Should()
            .Be("45");
    }

    [Fact]
    public void MapToNewRightWhenLeft()
    {
        var x = _left
            .Map(r => r.ToString());

        x.Should()
            .BeOfType<Left<Foo, string>>();

        Foo v = (Left<Foo, string>)x;
        v.Should()
            .Be(Foo.F1);
    }

    [Fact]
    public void MapToNewLeftWhenRight()
    {
        var x = _right
             .Map(
                 r => r,
                 l => l.ToString()
             );

        x.Should()
         .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)x;
        v.Should()
            .Be(45);
    }

    [Fact]
    public void MapToNewLeftWhenLeft()
    {
        var x = _left
             .Map(
                 r => r,
                 l => l.ToString()
             );

        x.Should()
         .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("F1");
    }

    [Fact]
    public void MapToNewRightNewLeftWhenRight()
    {
        var x = _right
             .Map(
                 r => r * 10m,
                 l => l.ToString()
             );

        x.Should()
         .BeOfType<Right<string, decimal>>();

        decimal v = (Right<string, decimal>)x;
        v.Should()
            .Be(450m);
    }

    [Fact]
    public void MapToNewRightNewLeftWhenLeft()
    {
        var x = _left
             .Map(
                 r => r * 10m,
                 l => l.ToString()
             );

        x.Should()
         .BeOfType<Left<string, decimal>>();

        string v = (Left<string, decimal>)x;
        v.Should()
            .Be("F1");
    }

    [Fact]
    public void MapFlattenOfReturnedRightWhenRight()
    {
        static Either<string, int> Guard(int number)
        {
            return number == 42
                ? "choose different number"
                : number;
        }

        Either<string, int> right = 100;
        var x = right
            .Map(r => Guard(r));

        x.Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)x;
        v.Should()
            .Be(100);
    }

    [Fact]
    public void MapFlattenOfReturnedLeftWhenRight()
    {
        static Either<string, int> Guard(int number)
        {
            return number == 42
                ? "choose different number"
                : number;
        }

        Either<string, int> right = 42;
        var x = right
            .Map(r => Guard(r));

        x.Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("choose different number");
    }

    [Fact]
    public void MapFlattenWhenLeft()
    {
        static Either<string, int> Guard(int number)
        {
            return number == 42
                ? "choose different number"
                : number;
        }

        Either<string, int> right = "error";
        var x = right
            .Map(r => Guard(r));

        x.Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("error");
    }
}
