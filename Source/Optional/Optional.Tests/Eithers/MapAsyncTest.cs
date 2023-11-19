namespace Proxoft.Optional.Tests.Eithers;

public class MapAsyncTest
{
    private readonly Either<Foo, int> _right = 45;
    private readonly Either<Foo, int> _left = Foo.F1;

    [Fact]
    public async Task MapAsyncRightUsingSynchronous()
    {
        var x = await Task.FromResult(_right)
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
    public async Task MapAsyncRightUsingAsynchronous()
    {
        var x = await Task.FromResult(_right)
            .Map(
                r => Task.FromResult(r * 10m),
                l => Task.FromResult(l.ToString())
        );

        x.Should()
           .BeOfType<Right<string, decimal>>();

        decimal v = (Right<string, decimal>)x;
        v.Should()
            .Be(450m);
    }

    [Fact]
    public async Task MapSyncRightUsingAsynchronousWhenRight()
    {
        var x = await _right
            .Map(
                r => Task.FromResult(r * 10m),
                l => Task.FromResult(l.ToString())
        );

        x.Should()
           .BeOfType<Right<string, decimal>>();

        decimal v = (Right<string, decimal>)x;
        v.Should()
            .Be(450m);
    }

    [Fact]
    public async Task MapAsyncLeftUsingSynchronous()
    {
        var x = await Task.FromResult(_left)
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
    public async Task MapAsyncLeftUsingAsynchronous()
    {
        var x = await Task.FromResult(_left)
            .Map(
                r => Task.FromResult(r * 10m),
                l => Task.FromResult(l.ToString())
        );

        x.Should()
           .BeOfType<Left<string, decimal>>();

        string v = (Left<string, decimal>)x;
        v.Should()
            .Be("F1");
    }

    [Fact]
    public async Task MapSyncLeftUsingAsynchronousWhenRight()
    {
        var x = await _left
            .Map(
                r => Task.FromResult(r * 10m),
                l => Task.FromResult(l.ToString())
        );

        x.Should()
           .BeOfType<Left<string, decimal>>();

        string v = (Left<string, decimal>)x;
        v.Should()
            .Be("F1");
    }

    [Fact]
    public async Task MapToSameRightWhenRight1()
    {
        var x = await Task.FromResult(_right)
            .Map(r => r * 2);

        x.Should()
            .BeOfType<Right<Foo, int>>();

        int v = (Right<Foo, int>)x;
        v.Should()
            .Be(90);
    }

    [Fact]
    public async Task MapToSameRightWhenRight2()
    {
        var x = await _right
            .Map(r => Task.FromResult(r * 2));

        x.Should()
            .BeOfType<Right<Foo, int>>();

        int v = (Right<Foo, int>)x;
        v.Should()
            .Be(90);
    }

    [Fact]
    public async Task MapToSameRightWhenRight3()
    {
        var x = await Task.FromResult(_right)
            .Map(r => Task.FromResult(r * 2));

        x.Should()
            .BeOfType<Right<Foo, int>>();

        int v = (Right<Foo, int>)x;
        v.Should()
            .Be(90);
    }

    [Fact]
    public async Task MapToSameRightWhenLeft()
    {
        var x = await _left
            .Map(r => Task.FromResult(r * 2));

        x.Should()
            .BeOfType<Left<Foo, int>>();

        Foo v = (Left<Foo, int>)x;
        v.Should()
            .Be(Foo.F1);
    }

    [Fact]
    public async Task MapToNewRightWhenRight1()
    {
        var x = await Task.FromResult(_right)
            .Map(r => r.ToString());

        x.Should()
            .BeOfType<Right<Foo, string>>();

        string v = (Right<Foo, string>)x;
        v.Should()
            .Be("45");
    }

    [Fact]
    public async Task MapToNewRightWhenRight2()
    {
        var x = await _right
            .Map(r => Task.FromResult(r.ToString()));

        x.Should()
            .BeOfType<Right<Foo, string>>();

        string v = (Right<Foo, string>)x;
        v.Should()
            .Be("45");
    }

    [Fact]
    public async Task MapToNewRightWhenRight3()
    {
        var x = await Task.FromResult(_right)
            .Map(r => Task.FromResult(r.ToString()));

        x.Should()
            .BeOfType<Right<Foo, string>>();

        string v = (Right<Foo, string>)x;
        v.Should()
            .Be("45");
    }

    [Fact]
    public async Task MapToNewRightWhenLeft1()
    {
        var x = await Task.FromResult(_left)
            .Map(r => r.ToString());

        x.Should()
            .BeOfType<Left<Foo, string>>();

        Foo v = (Left<Foo, string>)x;
        v.Should()
            .Be(Foo.F1);
    }

    [Fact]
    public async Task MapToNewRightWhenLeft2()
    {
        var x = await _left
            .Map(r => Task.FromResult(r.ToString()));

        x.Should()
            .BeOfType<Left<Foo, string>>();

        Foo v = (Left<Foo, string>)x;
        v.Should()
            .Be(Foo.F1);
    }

    [Fact]
    public async Task MapToNewRightWhenLeft3()
    {
        var x = await Task.FromResult(_left)
            .Map(r => Task.FromResult(r.ToString()));

        x.Should()
            .BeOfType<Left<Foo, string>>();

        Foo v = (Left<Foo, string>)x;
        v.Should()
            .Be(Foo.F1);
    }

    [Fact]
    public async Task MapFlattenWhenRight1()
    {
        Either<string, int> right = 100;
        var x = await Task.FromResult(right)
           .Map(r => Guard(r));

        x.Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)x;
        v.Should()
            .Be(100);
    }

    [Fact]
    public async Task MapFlattenWhenRight2()
    {
        Either<string, int> right = 100;
        var x = await right
           .Map(r => GuardAsync(r));

        x.Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)x;
        v.Should()
            .Be(100);
    }

    [Fact]
    public async Task MapFlattenWhenRight3()
    {
        Either<string, int> right = 100;
        var x = await Task.FromResult(right)
           .Map(r => Guard(r));

        x.Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)x;
        v.Should()
            .Be(100);
    }

    [Fact]
    public async Task MapFlattenWhenLeft1()
    {
        Either<string, int> right = 42;
        var x = await Task.FromResult(right)
           .Map(r => Guard(r));

        x.Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("choose different number");
    }

    [Fact]
    public async Task MapFlattenWhenLeft2()
    {
        Either<string, int> right = 42;
        var x = await right
           .Map(r => GuardAsync(r));

        x.Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("choose different number");
    }

    [Fact]
    public async Task MapFlattenWhenLeft3()
    {
        Either<string, int> right = 42;
        var x = await Task.FromResult(right)
           .Map(r => GuardAsync(r));

        x.Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)x;
        v.Should()
            .Be("choose different number");
    }

    static Either<string, int> Guard(int number)
    {
        return number == 42
            ? "choose different number"
            : number;
    }

    static Task<Either<string, int>> GuardAsync(int number)
    {
        Either<string, int> either = number == 42
            ? "choose different number"
            : number;

        return Task.FromResult(either);
    }
}
