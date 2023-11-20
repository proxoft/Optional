namespace Proxoft.Optional;

public static class OptionAdaptersAsync
{
    public static Task<Either<L, R>> Either<L, R>(
        this Task<Option<R>> option,
        L ifNone)
    {
        return option
            .ContinueWith(
                o => o.Result.Either(ifNone)
            );
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
            .ContinueWith(ot =>
            {
                return ot.Result
                    .Map(v => Task.FromResult<Either<L, R>>(v))
                    .Reduce(ifNone);
            })
            .Unwrap();
    }

    public static Task<Either<L, R>> Either<T, L, R>(
        this Task<Option<T>> option,
        Func<T, Task<Either<L, R>>> some,
        Func<Task<Either<L, R>>> none)
    {
        return option
            .ContinueWith(o =>
            {
                return o.Result
                    .Map(some)
                    .Reduce(none);
            })
            .Unwrap();
    }
}
