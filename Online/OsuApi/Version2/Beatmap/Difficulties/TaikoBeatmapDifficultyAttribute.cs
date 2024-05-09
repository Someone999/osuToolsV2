using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;

public class TaikoBeatmapDifficultyAttribute : ApiV2DifficultyAttributes
{
    [JsonProperty("stamina_difficulty")]
    public double StaminaDifficulty { get; set; }
    
    [JsonProperty("rhythm_difficulty")]
    public double RhythmDifficulty { get; set; }
    
    [JsonProperty("color_difficulty")]
    public double ColorDifficulty { get; set; }
    
    [JsonProperty("approach_rate")]
    public double ApproachRate { get; set; }
    
    [JsonProperty("great_hit_window")]
    public double GreatHitWindow { get; set; }
}