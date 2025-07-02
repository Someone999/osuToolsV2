using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Users;

public class ProfileBanner
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("tournament_id")]
    public int TournamentId { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; } = "";

    [JsonProperty("image@2x")]
    public string Image2X { get; set; } = "";
}