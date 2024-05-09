using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;

public class CatchBeatmapDifficultyAttribute : ApiV2DifficultyAttributes
{
    [JsonProperty("approach_rate")]
    public double ApproachRate { get; set; }
}