namespace Proxoft.Optional;

public static class OptionOperatorsAsync
{
    public static Task<Option<NT>> Map<T, NT>(
        this Task<Option<T>> option,
        Func<T, NT> map)
    {
        return option
            .ContinueWith(
                t => t.Result.Map(map)
            );
    }

    public static Task<Option<NT>> Map<T, NT>(
        this Option<T> option,
        Func<T, Task<NT>> map)
    {
        return Task.FromResult(option)
            .Map(map);
    }

    public static Task<Option<NT>> Map<T, NT>(
        this Task<Option<T>> option,
        Func<T, Task<NT>> map)
    {
        return option
            .ContinueWith(
                t => t.Result is Some<T> s
                    ? map(s).ContinueWith<Option<NT>>(m => m.Result)
                    : Task.FromResult<Option<NT>>(None.Instance)
            )
            .Unwrap();
    }

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
