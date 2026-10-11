namespace NorthWind.Result.Entities;

public class Result<ErrorResultType>
{
    public bool HasError { get; private set; }
    public ErrorResultType ErrorValue { get; private set; }

    public Result()
    {
        HasError = false;
        ErrorValue = default!;
    }

    public Result(ErrorResultType errorValue)
    {
        HasError = true;
        ErrorValue = errorValue;
    }

    public void HandleResult(
        Action<ErrorResultType> whenHasErrorAction,
        Action? whenIsSuccessAction = null)
    {
        if (HasError)
        {
            whenHasErrorAction(ErrorValue);
        }
        else
        {
            whenIsSuccessAction?.Invoke();
        }
    }
}

public class Result<SuccessResultType, ErrorResultType> : Result<ErrorResultType>
{
    public SuccessResultType SuccessValue { get; private set; }

    public Result(SuccessResultType successValue) : base()
    {
        SuccessValue = successValue;
    }

    public Result(ErrorResultType errorValue) : base(errorValue)
    {
        SuccessValue = default!;
    }

    public void HandleResult(
        Action<SuccessResultType> whenIsSuccessAction,
        Action<ErrorResultType> whenHasErrorAction)
    {
        if (HasError)
        {
            whenHasErrorAction(ErrorValue);
        }
        else
        {
            whenIsSuccessAction(SuccessValue);
        }
    }
}
