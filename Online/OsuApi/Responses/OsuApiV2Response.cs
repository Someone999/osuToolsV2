using osuToolsV2.Online.OsuApi.Version2.Requests;

namespace osuToolsV2.Online.OsuApi.Responses;

public class OsuApiV2Response<TResponse> : HttpApiResponse<OsuApiV2ErrorInfo, TResponse>
{
    public OsuApiV2Response(OsuApiV2ErrorInfo? error, TResponse? data, HttpResponseMessage originalResponse) 
        : base(error, data, originalResponse)
    {
    }
}