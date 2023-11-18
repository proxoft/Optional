namespace Proxoft.Optional;

public static class EitherOperatorsAsync
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
