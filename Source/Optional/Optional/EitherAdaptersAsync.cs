namespace Proxoft.Optional;

public static class EitherAdaptersAsync
{
    public static Task<Option<T>> Option<L, R, T>(
        this Either<L, R> either,
        Func<R, Task<Option<T>>> right,
        Func<L, Task<Option<T>>> left)
    {
        return Task.FromResult(either)
            .Option(right, left);
    }

    public static Task<Option<T>> Option<L, R, T>(
        this Task<Either<L, R>> either,
        Func<R, Option<T>> right,
        Func<L, Option<T>> left)
    {
        return either
            .Option(
                r => Task.FromResult(right(r)),
                l => Task.FromResult(left(l))
            );
    }

    public static Task<Option<T>> Option<L, R, T>(
        this Task<Either<L, R>> either,
        Func<R, Task<Option<T>>> right,
        Func<L, Task<Option<T>>> left)
    {
        return either
            .ContinueWith(e => e.Result.Reduce(right, left))
            .Unwrap();
    }
}
