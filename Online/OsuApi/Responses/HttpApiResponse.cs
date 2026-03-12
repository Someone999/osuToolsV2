namespace osuToolsV2.Online.OsuApi.Responses;

public class HttpApiResponse<TError, TData> : ApiResponse<TError, TData, HttpResponseMessage>
{
    public HttpApiResponse(TError? error, TData? data, HttpResponseMessage originalResponse) : 
        base(error, data, originalResponse)
    {
    }

    public override bool IsSuccess => Error == null && Data != null;
}