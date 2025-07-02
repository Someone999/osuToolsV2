using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version1.Multiplayer.Matches;

public class MatchMetadata
{
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int MatchId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public string Name { get; set; } = "";
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public DateTime StartTime { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public DateTime? EndTime { get; set; }
}