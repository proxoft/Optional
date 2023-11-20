using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxoft.Optional.Tests.Options;

public class ReduceAsyncTest
{
    [Fact]
    public async Task ReduceWhenSome1()
    {
        Option<int> option = 10;

        int result = await Task.FromResult(option)
            .Reduce(() => Task.FromResult(20));

        result
            .Should()
            .Be(10);
    }

    [Fact]
    public async Task ReduceWhenSome2()
    {
        Option<int> option = 10;

        int result = await Task.FromResult(option)
            .Reduce(() => 20);

        result
            .Should()
            .Be(10);
    }

    [Fact]
    public async Task ReduceWhenSome3()
    {
        Option<int> option = 10;

        int result = await option
            .Reduce(() => Task.FromResult(20));

        result
            .Should()
            .Be(10);
    }

    [Fact]
    public async Task ReduceWhenNoneGivenFunc1()
    {
        Option<int> option = None.Instance;

        int result = await Task.FromResult(option)
            .Reduce(() => Task.FromResult(20));

        result
            .Should()
            .Be(20);
    }

    [Fact]
    public async Task ReduceWhenNoneGivenFunc2()
    {
        Option<int> option = None.Instance;

        int result = await Task.FromResult(option)
            .Reduce(() => 20);

        result
            .Should()
            .Be(20);
    }

    [Fact]
    public async Task ReduceWhenNoneGivenFunc3()
    {
        Option<int> option = None.Instance;

        int result = await option
            .Reduce(() => Task.FromResult(20));

        result
            .Should()
            .Be(20);
    }
}
