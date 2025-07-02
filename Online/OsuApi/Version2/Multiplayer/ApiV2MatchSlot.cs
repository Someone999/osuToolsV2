using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Multiplayer;

public class ApiV2MatchSlot
{
    [JsonProperty("slot")]
    public int Position { get; set; }

    public string Team { get; set; } = "";
    
    public bool Pass { get; set; }
}