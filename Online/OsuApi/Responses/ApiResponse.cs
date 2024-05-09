namespace osuToolsV2.Online.OsuApi.Responses;

public class ApiResponse<TError, TData, TResponseObject>
{
    public ApiResponse(TError? error, TData? data, TResponseObject originalResponse)
    {
        Error = error;
        Data = data;
        OriginalResponse = originalResponse;
    }

    public TError? Error { get; }
    public TData? Data { get; }
    public TResponseObject OriginalResponse { get; }

    public virtual bool IsSuccess => Error == null;
}