using System.Collections.Generic;
using System.Linq;

namespace Backend.Application.Wrappers;

public class BaseResult
{
    public bool Success { get; set; }
    public List<Error> Errors { get; set; }

    public static BaseResult Ok()
    {
        return new BaseResult { Success = true };
    }

    public static BaseResult Failure()
    {
        return new BaseResult { Success = false };
    }

    public static BaseResult Failure(Error error)
    {
        return new BaseResult { Success = false, Errors = [error] };
    }

    public static BaseResult Failure(IEnumerable<Error> errors)
    {
        return new BaseResult { Success = false, Errors = errors.ToList() };
    }

    public static implicit operator BaseResult(Error error)
    {
        return new BaseResult { Success = false, Errors = [error] };
    }

    public static implicit operator BaseResult(List<Error> errors)
    {
        return new BaseResult { Success = false, Errors = errors };
    }

    public BaseResult AddError(Error error)
    {
        Errors ??= [];
        Errors.Add(error);
        Success = false;
        return this;
    }
}

public class BaseResult<TData> : BaseResult
{
    public TData Data { get; set; }

    public static BaseResult<TData> Ok(TData data)
    {
        return new BaseResult<TData> { Success = true, Data = data };
    }

    public new static BaseResult<TData> Failure()
    {
        return new BaseResult<TData> { Success = false };
    }

    public new static BaseResult<TData> Failure(Error error)
    {
        return new BaseResult<TData> { Success = false, Errors = [error] };
    }

    public new static BaseResult<TData> Failure(IEnumerable<Error> errors)
    {
        return new BaseResult<TData> { Success = false, Errors = errors.ToList() };
    }

    public static implicit operator BaseResult<TData>(TData data)
    {
        return new BaseResult<TData> { Success = true, Data = data };
    }

    public static implicit operator BaseResult<TData>(Error error)
    {
        return new BaseResult<TData> { Success = false, Errors = [error] };
    }

    public static implicit operator BaseResult<TData>(List<Error> errors)
    {
        return new BaseResult<TData> { Success = false, Errors = errors };
    }
}