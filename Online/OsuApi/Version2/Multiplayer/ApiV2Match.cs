using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Online.OsuApi.Version2.Users;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2Match
{
    [JsonProperty("match")]
    public ApiV2MatchData MatchData { get; set; } = new ApiV2MatchData();

    [JsonProperty("events")]
    public List<ApiV2MatchEvent> MatchEvents { get; set; } = new List<ApiV2MatchEvent>();

    [JsonProperty("users")]
    public List<ApiV2User> Users { get; set; } = new List<ApiV2User>();
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public long FirstEventId { get; set; }
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public long LatestEventId { get; set; }
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public long? CurrentGameId { get; set; }
}