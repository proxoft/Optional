namespace Proxoft.Optional.Tests.Options;

public class WhenTest
{
    private class Foo
    {
    }

    [Fact]
    public void GivenNullToWhenNotNull()
    {
        Foo? value = null;
        Option<Foo> option = value.WhenNotNull();
        option
            .Should()
            .BeOfType<None<Foo>>();
    }

    [Fact]
    public void GivenValueToWhenNotNull()
    {
        Foo? value = new();
        Option<Foo> option = value.WhenNotNull();
        option
            .Should()
            .BeOfType<Some<Foo>>();

        Foo v2 = (Some<Foo>)option;
        v2.Should()
          .Be(value);
    }

    [Fact]
    public void GivePredicateFuncWhenReturnsSome()
    {
        int value = 78;
        Option<int> option = value.When(v => v > 10);

        option
            .Should()
            .BeOfType<Some<int>>();

        int v2 = (Some<int>)option;
        v2.Should()
          .Be(78);
    }

    [Fact]
    public void GivePredicateFuncWhenReturnsNone()
    {
        int value = 10;
        Option<int> option = value.When(v => v > 10);

        option
            .Should()
            .BeOfType<None<int>>();
    }

    [Fact]
    public void GivePredicateValueWhenReturnsSome()
    {
        int value = 78;
        Option<int> option = value.When(true);

        option
            .Should()
            .BeOfType<Some<int>>();

        int v2 = (Some<int>)option;
        v2.Should()
          .Be(78);
    }

    [Fact]
    public void GivePredicateValueWhenReturnsNone()
    {
        int value = 10;
        Option<int> option = value.When(false);

        option
            .Should()
            .BeOfType<None<int>>();
    }
}
