using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2MatchData
{
    public long Id { get; set; }
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime StartTime { get; set; }
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime? EndTime { get; set; }
    public string Name { get; set; } = "";
}