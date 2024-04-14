using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class Covers
{
    [JsonProperty("cover")]
    public string Cover { get; set; } = "";

    [JsonProperty("cover@2x")]
    public string Cover2X { get; set; }= "";

    [JsonProperty("card")]
    public string Card { get; set; }= "";

    [JsonProperty("card@2x")]
    public string Card2X { get; set; }= "";

    [JsonProperty("list")]
    public string List { get; set; }= "";

    [JsonProperty("list@2x")]
    public string List2X { get; set; }= "";

    [JsonProperty("slimcover")]
    public string SlimCover { get; set; }= "";

    [JsonProperty("slimcover@2x")]
    public string SlimCover2X { get; set; }= "";
}