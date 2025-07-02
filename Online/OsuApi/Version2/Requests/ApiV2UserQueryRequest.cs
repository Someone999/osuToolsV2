using Newtonsoft.Json;
using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;
using osuToolsV2.Online.OsuApi.Version2.Users;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;



public class ApiV2UserQueryRequestParameters
{
    public string UserIdOrName { get; set; } = "";
    public bool IsUserName { get; set; }
    public LegacyRuleset? Ruleset { get; set; } = null;
}

public class ApiV2UserQueryRequest : IApiV2Request<ApiV2UserExtended, ApiV2UserQueryRequestParameters>
{
    public async Task<OsuApiV2Response<ApiV2UserExtended>> QueryAsync(OsuOAuthToken token, OsuApiQueryContext<ApiV2UserQueryRequestParameters> context)
    {
        if (context.Context is null)
        {
            throw new InvalidOperationException("context.Context is null");
        }
        
        HttpClient client = new HttpClient();
       
        UriBuilder builder = new UriBuilder();
        var currentContext = context.Context;
        builder.Scheme = "https";
        builder.Host = "osu.ppy.sh";
        builder.Port = 443;
        builder.Path = $"/api/v2/users/{(currentContext.IsUserName ? "@" : "") + currentContext.UserIdOrName}";
        if (context.Context.Ruleset != null)
        {
            var ruleset = context.Context.Ruleset;
            string rulesetStr = ruleset switch
            {
                LegacyRuleset.Osu => "osu",
                LegacyRuleset.Taiko => "taiko",
                LegacyRuleset.Catch => "fruits",
                LegacyRuleset.Mania => "mania",
                LegacyRuleset.None => throw new InvalidOperationException(),
                null => throw new InvalidOperationException(),
                _ => throw new InvalidOperationException()
            };
            
            builder.Path += $"/{rulesetStr}";
        }
        
        HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, builder.ToString());
        requestMessage.Headers.Add("Authorization", $"Bearer {token.AccessToken}");
        var r = await client.SendAsync(requestMessage);
        if (!r.IsSuccessStatusCode)
        {
            return new OsuApiV2Response<ApiV2UserExtended>(null, null, r);
        }
        
        var json = await r.Content.ReadAsStringAsync();
        var user = JsonConvert.DeserializeObject<ApiV2UserExtended>(json);
        return new OsuApiV2Response<ApiV2UserExtended>(null, user, r);
    }
}