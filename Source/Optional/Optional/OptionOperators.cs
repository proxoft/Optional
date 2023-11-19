namespace Proxoft.Optional;

public static class OptionOperators
{
    public static Option<TR> Map<T, TR>(
        this Option<T> option,
        Func<T, TR> map)
    {
        return option is Some<T> s
            ? map(s)
            : None<TR>.Instance;
    }

    public static Option<TR> MapOption<T, TR>(
        this Option<T> option,
        Func<T, Option<TR>> map)
    {
        return option is Some<T> s
            ? map(s)
            : None<TR>.Instance;
    }

    public static Option<T> When<T>(this T value, Func<T, bool> predicate)
    {
        return predicate(value)
            ? value
            : None<T>.Instance;
    }

    public static Option<T> When<T>(this T value, bool predicate)
    {
        return value.When(_ => predicate);
    }

    public static Option<T> WhenNotNull<T>(this T? value)
        where T: class
    {
        return value is not null
            ? new Some<T>(value)
            : None<T>.Instance;
    }

    public static Option<T> WhenNotNull<T>(this T? value)
        where T : struct
    {
        return value is not null
            ? new Some<T>(value.Value)
            : None<T>.Instance;
    }

    public static T Reduce<T>(
        this Option<T> option,
        Func<T> ifNone)
    {
        return option is Some<T> s
            ? s
            : ifNone();
    }

    public static T Reduce<T>(
        this Option<T> option,
        T ifNone)
    {
        return option.Reduce(() => ifNone);
    }
}
