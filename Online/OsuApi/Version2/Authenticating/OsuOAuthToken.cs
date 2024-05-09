using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OsuOAuthToken
{
    private DateTime? _expireTime;
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public string TokenType { get; internal set; } = "";
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int ExpiresIn { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public string AccessToken { get; internal set; } = "";
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public string? RefreshToken { get; internal set; } = "";
    public DateTime IssueAt { get; } = DateTime.Now;

    public bool Expired
    {
        get
        {
            if (ExpiresIn == 0)
            {
                throw new InvalidOperationException("Expire time is not valid.");
            }

            _expireTime ??= IssueAt.AddSeconds(ExpiresIn);
            return _expireTime < DateTime.Now;
        }
    }
}