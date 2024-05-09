using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class ApiV2Language
{
    [JsonProperty]
    public int Id { get; internal set; }
    
    [JsonProperty]
    public string Name { get; internal set; } = "";
}