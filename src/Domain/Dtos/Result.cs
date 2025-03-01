public readonly struct Result<T, Error>
{
    private readonly bool _success;
    public readonly T Value;
    public readonly Error ErrorValue;

    private Result(T v, Error e, bool success)
    {
        Value = v;
        ErrorValue = e;
        _success = success;
    }

    public bool IsOk => _success;

    public static implicit operator Result<T, Error>(T v) => new(v, default!, true);
    public static implicit operator Result<T, Error>(Error e) => new(default!, e, false);

    // public R Match<R>(
    //         Func<T, R> success,
    //         Func<E, R> failure) =>
    //     _success ? success(Value) : failure(Error);
}