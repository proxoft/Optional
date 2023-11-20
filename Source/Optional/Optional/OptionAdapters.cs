namespace Proxoft.Optional;

public static class OptionAdapters
{
    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        L ifNone)
    {
        return option.Either(() => new Left<L, R>(ifNone));
    }

    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        Func<Either<L, R>> ifNone)
    {
        return option
            .Map(r => (Either<L, R>)r)
            .Reduce(ifNone);
    }

    public static Either<L, R> Either<T, L, R>(
        this Option<T> option,
        Func<T, Either<L, R>> some,
        Func<Either<L,R>> none)
    {
        return option
            .Map(some)
            .Reduce(none);
    }
}
