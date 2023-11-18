namespace Proxoft.Optional;

public static class EitherOperators
{
    /// <summary>
    /// Map right value to a new value and/or type
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="NR"></typeparam>
    /// <param name="either"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public static Either<L, NR> Map<L, R, NR>(
        this Either<L, R> either,
        Func<R, NR> map)
    {
        return either is Right<L, R> r
            ? new Right<L, NR>(map(r))
            : new Left<L, NR>((Left<L, R>)either);
    }

    /// <summary>
    /// Map both right and left to new values and/or types
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="NL"></typeparam>
    /// <typeparam name="NR"></typeparam>
    /// <param name="either"></param>
    /// <param name="right"></param>
    /// <param name="left"></param>
    /// <returns></returns>
    public static Either<NL, NR> Map<L, R, NL, NR>(
        this Either<L, R> either,
        Func<R, NR> right,
        Func<L, NL> left)
    {
        return either is Right<L, R> r
            ? new Right<NL, NR>(right(r))
            : new Left<NL, NR>(left((Left<L, R>)either));
    }

    /// <summary>
    /// Map right and return the inner Either
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="NR"></typeparam>
    /// <param name="either"></param>
    /// <param name="map"></param>
    /// <returns></returns>
    public static Either<L, NR> Map<L, R, NR>(
        this Either<L, R> either,
        Func<R, Either<L, NR>> map)
    {
        return either is Right<L, R> r
            ? map(r)
            : new Left<L, NR>((Left<L, R>)either);
    }

    /// <summary>
    /// Do if either is "right" and continue pipeline
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAction"></param>
    /// <returns></returns>
    public static Either<L, R> Do<L, R>(
        this Either<L, R> either,
        Action<R> rightAction)
    {
        return either.Do(rightAction, _ => { });
    }

    /// <summary>
    /// Do if either is "left" and continue pipeline
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="leftAction"></param>
    /// <returns></returns>
    public static Either<L, R> Do<L, R>(
        this Either<L, R> either,
        Action<L> leftAction)
    {
        return either.Do(_ => { }, leftAction);
    }

    /// <summary>
    /// Do and continue pipeline
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="rightAction"></param>
    /// <param name="leftAction"></param>
    /// <returns></returns>
    public static Either<L, R> Do<L, R>(
        this Either<L, R> either,
        Action<R> rightAction,
        Action<L> leftAction)
    {
        if (either is Right<L, R> r)
        {
            rightAction(r);
        }
        else
        {
            leftAction((Left<L, R>)either);
        }

        return either;
    }

    /// <summary>
    /// Reduce both right and left into single value (of same type than TRight)
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="TR"></typeparam>
    /// <param name="either"></param>
    /// <param name="left"></param>
    /// <returns></returns>
    public static TR Reduce<L, TR>(
        this Either<L, TR> either,
        Func<L, TR> left)
    {
        return either.Reduce(
            right: r => r,
            left: left
        );
    }

    /// <summary>
    /// Reduce both right and left into single value (of same type than TLeft)
    /// </summary>
    /// <typeparam name="TL"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <param name="either"></param>
    /// <param name="right"></param>
    /// <returns></returns>
    public static TL Reduce<TL, R>(
        this Either<TL, R> either,
        Func<R, TL> right)
    {
        return either.Reduce(
            right: right,
            left: l => l
        );
    }

    /// <summary>
    /// Reduce both right and left into single value
    /// </summary>
    /// <typeparam name="L"></typeparam>
    /// <typeparam name="R"></typeparam>
    /// <typeparam name="T"></typeparam>
    /// <param name="either"></param>
    /// <param name="right"></param>
    /// <param name="left"></param>
    /// <returns></returns>
    public static T Reduce<L, R, T>(
        this Either<L, R> either,
        Func<R, T> right,
        Func<L, T> left)
    {
        return either is Right<L, R> r
           ? right(r)
           : left((Left<L, R>)either);
    }
}
