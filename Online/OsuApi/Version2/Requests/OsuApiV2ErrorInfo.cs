using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class OsuApiV2ErrorInfo
{
    [JsonProperty("error")]
    public string Error { get; internal set; } = "";

    [JsonProperty("error_description")]
    public string ErrorDescription { get; internal set; } = "";

    [JsonProperty("hint")]
    public string Hint { get; internal set; } = "";

    [JsonProperty("message")]
    public string Message { get; internal set; } = "";
}