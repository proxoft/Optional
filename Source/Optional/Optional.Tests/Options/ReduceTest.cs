namespace Proxoft.Optional.Tests.Options;

public class ReduceTest
{
    [Fact]
    public void ReduceWhenSome()
    {
        Option<int> option = 10;

        int result = option.Reduce(20);
        result
            .Should()
            .Be(10);
    }

    [Fact]
    public void ReduceWhenNoneGivenValue()
    {
        Option<int> option = None.Instance;

        int result = option.Reduce(20);
        result
            .Should()
            .Be(20);
    }

    [Fact]
    public void ReduceWhenNoneGivenFunc()
    {
        Option<int> option = None.Instance;

        int result = option.Reduce(() => 20);
        result
            .Should()
            .Be(20);
    }
}
