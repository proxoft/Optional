namespace Proxoft.Optional;

public static class EitherOperatorsMapAsync
{
    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Task<Either<L, R>> either,
        Func<R, NR> map
        )
    {
        return either
            .Map(
                right: map,
                left: l => l
            );
    }

    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Either<L, R> either,
        Func<R, Task<NR>> map
        )
    {
        return either
            .Map(
                right: map,
                left: l => Task.FromResult(l)
            );
    }

    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Task<Either<L, R>> either,
        Func<R, Task<NR>> map
        )
    {
        return either
            .Map(
                right: map,
                left: l => Task.FromResult(l)
            );
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
    public static Task<Either<NL, NR>> Map<L, R, NL, NR>(
        this Task<Either<L, R>> either,
        Func<R, NR> right,
        Func<L, NL> left)
    {
        return either
            .ContinueWith(
                e => e.Result.Map(right, left)
            );
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
    public static Task<Either<NL, NR>> Map<L, R, NL, NR>(
        this Either<L, R> either,
        Func<R, Task<NR>> right,
        Func<L, Task<NL>> left)
    {
        return Task.FromResult(either).Map(right, left);
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
    public static Task<Either<NL, NR>> Map<L, R, NL, NR>(
        this Task<Either<L, R>> either,
        Func<R, Task<NR>> right,
        Func<L, Task<NL>> left)
    {
        return either
            .ContinueWith(
                e => {
                    
                    return e.Result is Right<L, R> r
                        ? right(r).ContinueWith<Either<NL, NR>>(t => t.Result)
                        : left((Left<L, R>)e.Result).ContinueWith<Either<NL, NR>>(t => t.Result);
                }
             )
             .Unwrap();
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
    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Task<Either<L, R>> either,
        Func<R, Either<L, NR>> map)
    {
        return either.ContinueWith(
                t => t.Result is Right<L, R> r
                    ? map(r)
                    : t.Result.SwapRightTypeOnLeftInstance<L, R, NR>()
            );
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
    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Either<L, R> either,
        Func<R, Task<Either<L, NR>>> map)
    {
        return Task.FromResult(either).Map(map);
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
    public static Task<Either<L, NR>> Map<L, R, NR>(
        this Task<Either<L, R>> either,
        Func<R, Task<Either<L, NR>>> map)
    {
        return either.ContinueWith(
                t => t.Result is Right<L, R> r
                    ? map(r)
                    : Task.FromResult(t.Result.SwapRightTypeOnLeftInstance<L, R, NR>())
            )
            .Unwrap();
    }
}

public static class EitherOperatorsDoAsync
{
    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Func<R, Task> rightAction,
        Func<L, Task> leftAction)
    {

        return either.ContinueWith(e => e.Result.Do(rightAction, leftAction)).Unwrap();
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Func<R, Task> rightAction)
    {
        return either
            .Do(
                rightAction,
                _ => Task.CompletedTask
            );
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Func<L, Task> leftAction)
    {
        return either
            .Do(
                _ => Task.CompletedTask,
                leftAction
            );
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Either<L, R> either,
        Func<R, Task> rightAction,
        Func<L, Task> leftAction)
    {
        return either is Right<L, R> r
            ? rightAction(r).ContinueWith(_ => either)
            : leftAction((Left<L, R>)either).ContinueWith(_ => either);
    }

    public static Task<Either<L, R>> Do<L, R>(
       this Either<L, R> either,
       Func<R, Task> rightAction)
    {
        return either.Do(rightAction, _ => Task.CompletedTask);
    }

    public static Task<Either<L, R>> Do<L, R>(
       this Either<L, R> either,
       Func<L, Task> leftAction)
    {
        return either.Do(_ => Task.CompletedTask, leftAction);
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Action<R> rightAction,
        Action<L> leftAction)
    {
        return either.ContinueWith(e => e.Result.Do(rightAction, leftAction));
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Action<R> rightAction)
    {
        return either.Do(rightAction, _ => { });
    }

    public static Task<Either<L, R>> Do<L, R>(
        this Task<Either<L, R>> either,
        Action<L> leftAction)
    {
        return either.Do(_ => { }, leftAction);
    }
}

public static class EitherOperatorsReduceAsync
{
    public static Task<TR> Reduce<L, TR>(
        this Task<Either<L, TR>> either,
        Func<L, TR> leftReduce)
    {
        return either.ContinueWith(e => e.Result.Reduce(leftReduce));
    }

    public static Task<TR> Reduce<TR, R>(
        this Task<Either<TR, R>> either,
        Func<R, TR> rightReduce)
    {
        return either.ContinueWith(e => e.Result.Reduce(rightReduce));
    }

    public static Task<T> Reduce<L, R, T>(
        this Task<Either<L, R>> either,
        Func<R, T> rightReduce,
        Func<L, T> leftReduce)
    {
        return either.ContinueWith(e => e.Result.Reduce(rightReduce, leftReduce));
    }
}
