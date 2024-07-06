using Newtonsoft.Json;
using osuToolsV2.Game.Legacy;
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