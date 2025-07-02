using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2MatchEventDetail
{
    [JsonProperty("type")]
    public string MatchEventType { get; set; } = "";
    
    [JsonProperty]
    public string? Text { get; set; }
}