using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2;

public class ApiV2MultiplayerScores
{
    [JsonProperty("cursor_string")]
    public string? CursorString { get; set; }

    [JsonProperty("params")]
    public object? Params { get; set; }

    [JsonProperty("scores")]
    public List<ApiV2Score> Scores { get; set; } = new List<ApiV2Score>();

    [JsonProperty("total")]
    public int? Total { get; set; }

    [JsonProperty("user_score")]
    public ApiV2Score UserScore { get; set; } = new ApiV2Score();
}