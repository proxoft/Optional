namespace Proxoft.Optional;

public static class EitherAdapters
{
    public static Option<R> Option<L, R>(
        this Either<L, R> either,
        Func<R, Option<R>> right)
    {
        return either
            .Reduce(right, _ => None.Instance);
    }

    public static Option<T> Option<L, R, T>(
        this Either<L, R> either,
        Func<R, Option<T>> right,
        Func<L, Option<T>> left)
    {
        return either
            .Reduce(right, left);
    }
}
