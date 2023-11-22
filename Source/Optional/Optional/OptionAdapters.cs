namespace Proxoft.Optional;

public static class OptionAdapters
{
    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        L none)
    {
        return option.Either(() => none);
    }

    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        Func<L> none)
    {
        return option
            .Map(r => (Either<L, R>)r)
            .Reduce(() => new Left<L, R>(none()));
    }

    public static Either<L, R> Either<T, L, R>(
        this Option<T> option,
        Func<T, Either<L, R>> some,
        Func<Either<L, R>> none)
    {
        return option
            .Map(some)
            .Reduce(() => none());
    }

}
