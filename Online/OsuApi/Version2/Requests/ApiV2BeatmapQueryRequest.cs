using System.Net.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;
using osuToolsV2.Online.OsuApi.Version2.Beatmap;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class ApiV2BeatmapQueryRequest : IApiV2Request<ApiV2BeatmapExtended, int>
{
    public async Task<OsuApiV2Response<ApiV2BeatmapExtended>> QueryAsync(OsuOAuthToken token, OsuApiQueryContext<int> context)
    {
        HttpClient httpClient = new HttpClient();
        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = new Uri($"https://osu.ppy.sh/api/v2/beatmaps/{context.Context}");
        requestMessage.Method = HttpMethod.Get;
        requestMessage.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        var res = await httpClient.SendAsync(requestMessage);
        var content = await res.Content.ReadAsStringAsync();
        var jObj = JsonConvert.DeserializeObject<JObject>(content);
        if (jObj == null)
        {
            throw new Exception("Failed to deserialize");
        }

        bool isError = jObj.ContainsKey("error");
        OsuApiV2ErrorInfo? errorInfo = null;
        ApiV2BeatmapExtended? extended = null;
        if (isError)
        {
            errorInfo = jObj.ToObject<OsuApiV2ErrorInfo>();
        }
        else
        {
            extended = jObj.ToObject<ApiV2BeatmapExtended>();
        }

        return new OsuApiV2Response<ApiV2BeatmapExtended>(errorInfo, extended, res);
    }
}