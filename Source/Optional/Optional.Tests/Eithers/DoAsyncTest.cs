namespace Proxoft.Optional.Tests.Eithers;

public class DoAsyncTest
{
    private readonly Either<Foo, int> _right = 89;
    private readonly Either<Foo, int> _left = Foo.F1;

    [Fact]
    public async Task DoWhenRight1()
    {
        string x = "nothing";
        await Task.FromResult(_right)
            .Do(
                r => x = r.ToString(),
                l => x = l.ToString()
            );

        x.Should()
            .Be("89");
    }

    [Fact]
    public async Task DoWhenRight2()
    {
        string x = "nothing";

        await _right
            .Do(
                r =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                },
                l =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("89");
    }

    [Fact]
    public async Task DoWhenRight3()
    {
        string x = "nothing";

        await Task.FromResult(_right)
            .Do(
                r =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                },
                l =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("89");
    }

    [Fact]
    public async Task DoWhenLeft()
    {
        string x = "nothing";

        await Task.FromResult(_left)
            .Do(
                r => x = r.ToString(),
                l => x = l.ToString()
            );

        x.Should()
            .Be("F1");
    }

    [Fact]
    public async Task DoWhenLeft2()
    {
        string x = "nothing";

        await _left
            .Do(
                r =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                },
                l =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("F1");
    }

    [Fact]
    public async Task DoWhenLeft3()
    {
        string x = "nothing";

        await Task.FromResult(_left)
            .Do(
                r =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                },
                l =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("F1");
    }

    [Fact]
    public async Task DoOnlyRightWhenRight1()
    {
        string x = "nothing";

        await Task.FromResult(_right)
            .Do(
                (int r) =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                }
            );

        x.Should()
         .Be("89");
    }

    [Fact]
    public async Task DoOnlyRightWhenRight2()
    {
        string x = "nothing";

        await _right
            .Do(
                (int r) =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                }
            );

        x.Should()
         .Be("89");
    }

    [Fact]
    public async Task DoOnlyRightWhenRight3()
    {
        string x = "nothing";

        await Task.FromResult(_right)
            .Do(
                (int r) =>
                {
                    x = r.ToString();
                    return Task.CompletedTask;
                }
            );

        x.Should()
         .Be("89");
    }

    [Fact]
    public async Task DoOnlyLeftWhenLeft1()
    {
        string x = "nothing";

        await Task.FromResult(_left)
            .Do(
                (Foo l) => x = l.ToString()
            );

        x.Should()
            .Be("F1");
    }

    [Fact]
    public async Task DoOnlyLeftWhenLeft2()
    {
        string x = "nothing";

        await _left
            .Do(
                (Foo l) =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("F1");
    }

    [Fact]
    public async Task DoOnlyLeftWhenLeft3()
    {
        string x = "nothing";

        await Task.FromResult(_left)
            .Do(
                (Foo l) =>
                {
                    x = l.ToString();
                    return Task.CompletedTask;
                }
            );

        x
            .Should()
            .Be("F1");
    }
}
