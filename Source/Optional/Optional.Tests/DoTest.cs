namespace Proxoft.Optional.Tests;

public class DoTest
{
    private readonly Either<Foo, int> _right = 89;
    private readonly Either<Foo, int> _left = Foo.F1;

    [Fact]
    public void DoWhenRight()
    {
        string x = "nothing";

        _right.Do(
            r => x = r.ToString(),
            l => x = l.ToString()
        );

        x.Should()
            .Be("89");
    }

    [Fact]
    public void DoOnlyRightWhenRight()
    {
        string x = "nothing";

        _right.Do(
            (int r) => x = r.ToString()
        );

        x.Should()
            .Be("89");
    }

    [Fact]
    public void DoOnlyLeftWhenRight()
    {
        string x = "nothing";

        _right.Do(
            (Foo l) => x = l.ToString()
        );

        x.Should()
            .Be("nothing");
    }

    [Fact]
    public void DoWhenLeft()
    {
        string x = "nothing";

        _left.Do(
            r => x = r.ToString(),
            l => x = l.ToString()
        );

        x.Should()
            .Be("F1");
    }

    [Fact]
    public void DoOnlyRightWhenLeft()
    {
        string x = "nothing";

        _left.Do(
            (int r) => x = r.ToString()
        );

        x.Should()
            .Be("nothing");
    }

    [Fact]
    public void DoOnlyLeftWhenLeft()
    {
        string x = "nothing";

        _left.Do(
            (Foo l) => x = l.ToString()
        );

        x.Should()
            .Be("F1");
    }
}
