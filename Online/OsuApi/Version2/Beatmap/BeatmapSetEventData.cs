using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class BeatmapSetEventData
{
    [JsonProperty]
    public string Title { get; internal set; } = "";
    
    [JsonProperty]
    public string Url { get; internal set; } = "";
}