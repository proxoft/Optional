namespace Proxoft.Optional.Tests.Options;

public class OptionAdapterTest
{
    [Fact]
    public void SomeMapsToEither1()
    {
        Option<int> some = 5;

        Either<string, int> either = some
            .Either(none: "missing");

        either
            .Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)either;
        v.Should()
            .Be(5);
    }

    [Fact]
    public void SomeMapsToEither2()
    {
        Option<int> some = 5;

        Either<string, int> either = some
            .Either(none: () => "missing");

        either
            .Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)either;
        v.Should()
            .Be(5);
    }

    [Fact]
    public void NoneMapsToEither1()
    {
        Option<int> some = None.Instance;

        Either<string, int> either = some
            .Either(none: "missing");

        either
            .Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)either;
        v.Should()
            .Be("missing");
    }

    [Fact]
    public void NoneMapsToEither2()
    {
        Option<int> some = None.Instance;

        Either<string, int> either = some
            .Either(none: () => "missing");

        either
            .Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)either;
        v.Should()
            .Be("missing");
    }

    [Fact]
    public void SomeMapsToEitherWithTypeChange()
    {
        Option<int> some = 5;

        Either<string, decimal> either = some
            .Either<int, string, decimal>(
                v => v * 2m,
                () => "missing"
            );

        either
            .Should()
            .BeOfType<Right<string, decimal>>();

        decimal v = (Right<string, decimal>)either;
        v.Should()
            .Be(10);
    }

    [Fact]
    public void SomeMapsToEitherWithTypeSwitch()
    {
        static Option<string> TryOrError(int value)
        {
            // none stands for no error
            return None.Instance;
        }

        int value = 7;
        Either<string, int> either = TryOrError(value)
            .Either<string, string, int>(
                error => error,
                () => value
            );

        either
            .Should()
            .BeOfType<Right<string, int>>();

        int v = (Right<string, int>)either;
        v.Should()
            .Be(7);
    }

    [Fact]
    public void SomeMapsToEitherWithTypeSwitch2()
    {
        static Option<string> TryOrError(int value)
        {
            // none stands for no error
            return "something wrong happened";
        }

        int value = 7;
        Either<string, int> either = TryOrError(value)
            .Either<string, string, int>(
                error => error,
                () => value
            );

        either
            .Should()
            .BeOfType<Left<string, int>>();

        string v = (Left<string, int>)either;
        v.Should()
            .Be("something wrong happened");
    }
}
