using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class Notation
{
    [JsonProperty("beatmapset_id")]
    public int BeatmapSetId { get; internal set; }
    
    [JsonProperty]
    public string[] Rulesets { get; internal  set; } = Array.Empty<string>();
    
    [JsonProperty]
    public bool Reset { get; internal  set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int UserId { get; internal set; }
}