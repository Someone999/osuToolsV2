using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;

public class OsuBeatmapDifficultyAttribute : ApiV2DifficultyAttributes
{
    [JsonProperty("aim_difficulty")]
    public double AimDifficulty { get; set; }
    
    [JsonProperty("approach_rate")]
    public double ApproachRate { get; set; }
    
    [JsonProperty("flashlight_difficulty")]
    public double FlashlightDifficulty{ get; set; }
    
    [JsonProperty("slider_factor")]
    public double SliderFactor { get; set; }
    
    [JsonProperty("overall_difficulty")]
    public double OverallDifficulty { get; set; }
    
    [JsonProperty("speed_difficulty")]
    public double SpeedDifficulty { get; set; }
}