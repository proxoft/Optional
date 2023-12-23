namespace Proxoft.Optional;

public static class OptionAdapters
{
    /// <summary>
    /// Converts Option{R} to Either{L, R} (right is value of Option)
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="option"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        L none)
    {
        return option.Either(() => none);
    }

    /// <summary>
    /// Converts Option{R} to Either{L, R} (right is value of Option)
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="option"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public static Either<L, R> Either<L, R>(
        this Option<R> option,
        Func<L> none)
    {
        return option
            .Map(r => (Either<L, R>)r)
            .Reduce(() => new Left<L, R>(none()));
    }

    /// <summary>
    /// Converts Option{T} to any Either{L, R}. Enables also swap Option{T} to left.
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="option"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public static Either<L, R> Either<T, L, R>(
        this Option<T> option,
        Func<T, Either<L, R>> some,
        Func<Either<L, R>> none)
    {
        return option
            .Map(some)
            .Reduce(() => none());
    }

    /// <summary>
    /// Alias for Either
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="option"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public static Either<L, R> Else<L, R>(
        this Option<R> option,
        L none)
    {
        return option.Else(() => none);
    }

    /// <summary>
    /// Alias for Either
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="option"></param>
    /// <param name="none"></param>
    /// <returns></returns>
    public static Either<L, R> Else<L, R>(
        this Option<R> option,
        Func<L> none)
    {
        return option
            .Map(r => (Either<L, R>)r)
            .Reduce(() => new Left<L, R>(none()));
    }
}
