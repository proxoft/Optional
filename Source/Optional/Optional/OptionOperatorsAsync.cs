namespace Proxoft.Optional;

public static class OptionOperatorsAsync
{
    public static Task<T> Reduce<T>(
        this Task<Option<T>> option,
        Func<T> ifNone)
    {
        return option.Reduce(() => Task.FromResult(ifNone()));
    }

    public static Task<T> Reduce<T>(
        this Option<T> option,
        Func<Task<T>> ifNone)
    {
        return Task.FromResult(option).Reduce(ifNone);
    }

    public static Task<T> Reduce<T>(
        this Task<Option<T>> option,
        Func<Task<T>> ifNone)
    {
        return option
            .ContinueWith(t =>
            {
                return t.Result is Some<T> s
                    ? Task.FromResult<T>(s)
                    : ifNone();
            })
            .Unwrap();
    }
}
