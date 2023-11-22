namespace Proxoft.Optional;

public static class OptionAdaptersAsync
{
    public static Task<Either<L, R>> Either<L, R>(
        this Task<Option<R>> option,
        L ifNone)
    {
        return option
            .Either(ifNone: () => Task.FromResult<Either<L,R>>(ifNone));
    }

    public static Task<Either<L, R>> Either<L, R>(
        this Option<R> option,
        Func<Task<Either<L, R>>> ifNone)
    {
        return Task.FromResult(option)
            .Either(ifNone);
    }

    public static Task<Either<L, R>> Either<L, R>(
        this Task<Option<R>> option,
        Func<Task<Either<L, R>>> ifNone)
    {
        return option
            .Either(
                ifSome: o => Task.FromResult<Either<L, R>>(o),
                ifNone: ifNone);
    }

    public static Task<Either<L, R>> Either<T, L, R>(
        this Task<Option<T>> option,
        Func<T, Task<Either<L, R>>> ifSome,
        Func<Task<Either<L, R>>> ifNone)
    {
        return option
            .ContinueWith(o =>
            {
                return o.Result
                    .Map(ifSome)
                    .Reduce(() => ifNone());
            })
            .Unwrap();
    }
}
