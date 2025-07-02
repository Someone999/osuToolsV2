using Newtonsoft.Json;
using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version1.Multiplayer.Matches;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;
using osuToolsV2.Online.OsuApi.Version2.Multiplayer;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class ApiV2MatchQueryRequest : IApiV2Request<ApiV2Match, int>
{
    private const string ApiUrl = "https://osu.ppy.sh/api/v2/matches/";
    public async Task<OsuApiV2Response<ApiV2Match>> QueryAsync(OsuOAuthToken token, OsuApiQueryContext<int> context)
    {
        var realUrl = ApiUrl + context.Context;
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, realUrl);
        requestMessage.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        HttpClient client = new HttpClient();
        var res = await client.SendAsync(requestMessage);
        if (!res.IsSuccessStatusCode)
        {
            var errInfo = new OsuApiV2ErrorInfo();
            return new OsuApiV2Response<ApiV2Match>(errInfo, null, res);
        }
        
        var str = await res.Content.ReadAsStringAsync();
        var data = JsonConvert.DeserializeObject<ApiV2Match>(str);
        return new OsuApiV2Response<ApiV2Match>(null, data, res);
    }
}