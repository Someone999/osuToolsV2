using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version1.Multiplayer.Matches;

public class MatchGame
{
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int GameId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public DateTime StartTime { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public DateTime? EndTime { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int BeatmapId { get; set; }
    
    [JsonProperty("play_mod")]
    public LegacyRuleset Ruleset { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public JObject? MatchType { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public MatchScoringType ScoringType { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public MatchTeamType TeamType { get; set; }
    
    [JsonProperty("mods")]
    public LegacyGameMod GlobalMods { get; set; }

    [JsonProperty("scores")]
    public MatchScore[] MatchScores { get; set; } = Array.Empty<MatchScore>();
}