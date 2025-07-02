using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2;

public class ApiV2MultiplayerScoresAround
{
    [JsonProperty("higher")]
    public ApiV2MultiplayerScores Higher { get; set; } = new ApiV2MultiplayerScores();
    [JsonProperty("lower")]
    public ApiV2MultiplayerScores Lower { get; set; } = new ApiV2MultiplayerScores();
}