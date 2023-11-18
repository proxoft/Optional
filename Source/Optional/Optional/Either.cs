namespace Proxoft.Optional;

public abstract class Either<TLeft, TRight>
{
    public static implicit operator Either<TLeft, TRight>(TRight right) => new Right<TLeft, TRight>(right);
    public static implicit operator Either<TLeft, TRight>(TLeft left) => new Left<TLeft, TRight>(left);
}

public class Right<TLeft, TRight>(TRight value) : Either<TLeft, TRight>
{
    private readonly TRight _value = value;

    public static implicit operator TRight(Right<TLeft, TRight> right)
    {
        return right._value;
    }
}

public class Left<TLeft, TRight>(TLeft value) : Either<TLeft, TRight>
{
    private readonly TLeft _value = value;

    public static implicit operator TLeft(Left<TLeft, TRight> left)
    {
        return left._value;
    }
}
