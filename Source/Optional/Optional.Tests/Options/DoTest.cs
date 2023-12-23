namespace Proxoft.Optional.Tests.Options;

public class DoTest
{
    [Fact]
    public void GivenSomeDo()
    {
        Option<int> option = 1;
        int doValue = 0;
        option
            .Do(v =>
            {
                doValue = v;
            });

        doValue
            .Should()
            .Be(1);
    }

    [Fact]
    public void GivenNoneDoNothing()
    {
        Option<int> option = None.Instance;
        int doValue = 0;
        option
            .Do(v =>
            {
                doValue = v;
            });

        doValue
            .Should()
            .Be(0);
    }
}
