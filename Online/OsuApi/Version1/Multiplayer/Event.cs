using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version1.Multiplayer;

public class Event
{
    [JsonProperty("display_html")]
    public string DisplayHtml { get; set; } = "";
    
    [JsonProperty("beatmap_id")]
    public int BeatmapId { get; set; }
    
    [JsonProperty("beatmapset_id")]
    public int BeatmapSetId { get; set; }
    
    [JsonProperty("date")]
    public DateTime Date { get; set; }

    [JsonProperty("epicfactor")]
    public int EpicFactor { get; set; }
}