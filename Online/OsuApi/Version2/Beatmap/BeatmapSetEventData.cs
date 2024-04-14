using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class BeatmapSetEventData
{
    [JsonProperty]
    public string Title { get; internal set; } = "";
    
    [JsonProperty]
    public string Url { get; internal set; } = "";
}

public class ApiV2Genre
{
    [JsonProperty]
    public int Id { get; internal set; }
    
    [JsonProperty]
    public string Name { get; internal set; } = "";
}

public class ApiV2Language
{
    [JsonProperty]
    public int Id { get; internal set; }
    
    [JsonProperty]
    public string Name { get; internal set; } = "";
}