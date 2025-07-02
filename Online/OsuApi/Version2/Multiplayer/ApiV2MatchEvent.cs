using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2MatchEvent
{
    [JsonProperty]
    public long Id { get; set; }
    
    [JsonProperty]
    public DateTime Timestamp { get; set; }

    [JsonProperty("detail")]
    public ApiV2MatchEventDetail EventDetail { get; set; } = new ApiV2MatchEventDetail();
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? UserId { get; set; }
    
    [JsonProperty("game")]
    public ApiV2MatchGame? MatchGame { get; set; }
}