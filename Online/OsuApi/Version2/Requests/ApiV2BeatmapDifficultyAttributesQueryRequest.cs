using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;
using osuToolsV2.Online.OsuApi.Version2.Beatmap;
using osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class ApiV2BeatmapDifficultyAttributesQueryParameter
{
    [JsonIgnore]
    public int BeatmapId { get; set; }
    
    [JsonProperty("mods")]
    public LegacyGameMod? Mod { get; set; }
    
    [JsonProperty("ruleset")]
    public string? Ruleset { get; set; }
    
    [JsonProperty("ruleset_id")]
    public LegacyRuleset? RulesetId { get; set; }
}


public class ApiV2BeatmapDifficultyAttributesQueryRequest<TApiV2DifficultyAttributes>
    : IApiV2Request<TApiV2DifficultyAttributes, ApiV2BeatmapDifficultyAttributesQueryParameter> 
    where TApiV2DifficultyAttributes: ApiV2DifficultyAttributes
{
    public async Task<OsuApiV2Response<TApiV2DifficultyAttributes>> QueryAsync(OsuOAuthToken token, 
        OsuApiQueryContext<ApiV2BeatmapDifficultyAttributesQueryParameter> context)
    {
        HttpClient httpClient = new HttpClient();
        HttpRequestMessage requestMessage = new HttpRequestMessage();
        var param = context.Context;
        if (param == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        
        
        requestMessage.RequestUri = new Uri($"https://osu.ppy.sh/api/v2/beatmaps/{param.BeatmapId}/attributes");
        requestMessage.Method = HttpMethod.Post;
        requestMessage.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        const string jsonContentType = "application/json";
        requestMessage.Content = new StringContent(JsonConvert.SerializeObject(param), null, jsonContentType);
        var res = await httpClient.SendAsync(requestMessage);
        var content = await res.Content.ReadAsStringAsync();
        var jObj = JsonConvert.DeserializeObject<JObject>(content);
        if (jObj == null)
        {
            throw new Exception("Failed to deserialize");
        }

        bool isError = jObj.ContainsKey("error");
        OsuApiV2ErrorInfo? errorInfo = null;
        TApiV2DifficultyAttributes? apiV2DifficultyAttributes = null;
        if (isError)
        {
            errorInfo = jObj.ToObject<OsuApiV2ErrorInfo>();
        }
        else
        {
            apiV2DifficultyAttributes = jObj["attributes"]?.ToObject<TApiV2DifficultyAttributes>();
        }

        var difficultyAttribute = apiV2DifficultyAttributes;

        return new OsuApiV2Response<TApiV2DifficultyAttributes>(errorInfo, difficultyAttribute, res);
    }
}