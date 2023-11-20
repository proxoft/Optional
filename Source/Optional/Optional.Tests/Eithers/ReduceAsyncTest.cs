namespace Proxoft.Optional.Tests.Eithers;

public class ReduceAsyncTest
{
    private readonly Either<Foo, int> _left = Foo.F1;
    private readonly Either<Foo, int> _right = 10;

    [Fact]
    public async Task ReduceOfRightToNewType()
    {
        string v = await Task.FromResult(_right)
            .Reduce(
                r => r.ToString(),
                l => l.ToString()
            );

        v.Should()
            .Be("10");
    }

    [Fact]
    public async Task ReduceOfLeftToNewType()
    {
        string v = await Task.FromResult(_left)
            .Reduce(
                r => r.ToString(),
                l => l.ToString()
            );

        v.Should()
            .Be("F1");
    }

    [Fact]
    public async Task ReduceOfRightToSameTypeAsRight()
    {
        int r = await Task.FromResult(_right)
            .Reduce(l => (int)l);

        r.Should().Be(10);
    }

    [Fact]
    public async Task ReduceOfLeftToSameTypeAsRight()
    {
        var either = Task.FromResult(_left);

        int r = await either
            .Reduce(l => (int)l);

        r.Should().Be(21);
    }

    [Fact]
    public async Task ReduceOfRightToSameTypeAsLeft()
    {
        Foo r = await Task.FromResult(_right)
            .Reduce(r => Foo.F2);

        r.Should().Be(Foo.F2);
    }

    [Fact]
    public async Task ReduceOfLeftToSameTypeAsLeft()
    {
        var either = Task.FromResult(_left);

        Foo r = await either
            .Reduce(r => Foo.F2);

        r.Should().Be(Foo.F1);
    }

    [Fact]
    public async Task ReduceOfRightReturningTask()
    {
        string x = await _right.Reduce(
            r => Task.FromResult("right"),
            l => Task.FromResult("left")
        );

        x.Should().Be("right");
    }

    [Fact]
    public async Task ReduceOfLeftReturningTask()
    {
        string x = await _left.Reduce(
            r => Task.FromResult("right"),
            l => Task.FromResult("left")
        );

        x.Should().Be("left");
    }
}
