namespace Proxoft.Optional.Tests.Options;

public class DoAsyncTest
{
    [Fact]
    public async Task GivenSomeDo1()
    {
        Option<int> option = 1;
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
                return Task.CompletedTask;
            });

        doValue
            .Should()
            .Be(1);
    }

    [Fact]
    public async Task GivenSomeDo2()
    {
        Task<Option<int>> option = Task.FromResult<Option<int>>(10);
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
            });

        doValue
            .Should()
            .Be(10);
    }

    [Fact]
    public async Task GivenSomeDo3()
    {
        Task<Option<int>> option = Task.FromResult<Option<int>>(10);
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
                return Task.CompletedTask;
            });

        doValue
            .Should()
            .Be(10);
    }

    [Fact]
    public async Task GivenNoneDo1()
    {
        Option<int> option = None.Instance;
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
                return Task.CompletedTask;
            });

        doValue
            .Should()
            .Be(0);
    }

    [Fact]
    public async Task GivenNoneDo2()
    {
        Task<Option<int>> option = Task.FromResult<Option<int>>(None.Instance);
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
            });

        doValue
            .Should()
            .Be(0);
    }

    [Fact]
    public async Task GivenNoneDo3()
    {
        Task<Option<int>> option = Task.FromResult<Option<int>>(None.Instance);
        int doValue = 0;
        await option
            .Do(v =>
            {
                doValue = v;
                return Task.CompletedTask;
            });

        doValue
            .Should()
            .Be(0);
    }
}
