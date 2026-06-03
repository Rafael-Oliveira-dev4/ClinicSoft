namespace ClinicSoft.Application.Common.Responses;

public sealed class Result<TResult, TOk, TError>
{
    public readonly bool IsSuccess;
    public readonly TOk? Ok;
    public readonly TResult? Value;
    public readonly TError? Error;

    private Result(TResult result)
    {
        IsSuccess = true;
        Value = result;
        Ok = default;
        Error = default;
    }

    private Result(TOk ok)
    {
        IsSuccess = true;
        Value = default;
        Ok = ok;
        Error = default;
    }

    private Result(TError error)
    {
        IsSuccess = false;
        Value = default;
        Ok = default;
        Error = error;
    }

    public static implicit operator Result<TResult, TOk, TError>(TResult value) => new(value);
    public static implicit operator Result<TResult, TOk, TError>(TOk ok) => new(ok);
    public static implicit operator Result<TResult, TOk, TError>(TError error) => new(error);
}

public sealed record Success
{
    public static readonly Success Ok = new();
}

public sealed class Result<TOk, TError>
{
    public readonly bool IsSuccess;
    public readonly TOk? Ok;
    public readonly TError? Error;

    private Result(TOk ok) { IsSuccess = true; Ok = ok; Error = default; }
    private Result(TError error) { IsSuccess = false; Ok = default; Error = error; }

    public static implicit operator Result<TOk, TError>(TOk ok) => new(ok);
    public static implicit operator Result<TOk, TError>(TError error) => new(error);
}
