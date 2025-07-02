using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Online.OsuApi.Version2.Beatmap;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2MatchGame
{
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int BeatmapId { get; set; }
    public int Id { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime StartTime { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime? EndTime { get; set; }

    [JsonProperty("mode")]
    public string Ruleset { get; set; } = "";
    
    [JsonProperty("mode_int")]
    public LegacyRuleset LegacyRuleset { get; set; }

    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public string ScoringType { get; set; } = "";
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public string TeamType { get; set; } = "";
    public string[] Mods { get; set; } = Array.Empty<string>();
    public ApiV2Beatmap Beatmap { get; set; } = new ApiV2Beatmap();
    public ApiV2Score[] Scores { get; set; } = Array.Empty<ApiV2Score>();
    
   
}