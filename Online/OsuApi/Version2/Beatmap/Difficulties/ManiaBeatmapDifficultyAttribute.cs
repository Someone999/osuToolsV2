using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;

public class ManiaBeatmapDifficultyAttribute : ApiV2DifficultyAttributes
{
    [JsonProperty("great_hit_window")]
    public double GreatHitWindow { get; set; }
    
    [JsonProperty("score_multiplier")]
    public double? ScoreMultiplier { get; set; }
}