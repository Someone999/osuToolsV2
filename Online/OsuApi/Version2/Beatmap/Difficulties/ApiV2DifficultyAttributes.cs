using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;

public class ApiV2DifficultyAttributes
{
    [JsonProperty("max_combo")]
    public int MaxCombo { get; set; }
    
    [JsonProperty("star_rating")]
    public double StarRating { get; set; }
}