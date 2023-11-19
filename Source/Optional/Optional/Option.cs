namespace Proxoft.Optional;

public abstract class Option<T>
{
    public static implicit operator Option<T>(T value) => new Some<T>(value);

    public static implicit operator Option<T>(None _) => None<T>.Instance;
}

public sealed class Some<T>(T value) : Option<T>
{
    private readonly T _value = value;

    public static implicit operator T(Some<T> some) => some._value;
}

public sealed class None<T> : Option<T>
{
    public static readonly None<T> Instance = new();

    public static implicit operator None<T>(None _) => Instance;
}

public sealed class None
{
    public static readonly None Instance = new();

    
}
