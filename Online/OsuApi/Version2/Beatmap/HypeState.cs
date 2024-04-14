using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class HypeState
{
    [JsonProperty]
    public int Current { get; internal set; }
    
    [JsonProperty]
    public int Required { get; internal set; }
}