using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class FailTimes
{
    [JsonProperty("exit")]
    public List<int>? ExitTimes { get; internal set; }
    
    [JsonProperty("fail")]
    public List<int>? FailedTimes { get; internal set; }
}