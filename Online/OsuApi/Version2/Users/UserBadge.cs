using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Users;

public class UserBadge
{
    [JsonProperty("awarded_at")]
    public DateTime AwardedAt { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; } = "";

    [JsonProperty("image@2x_url")]
    public string Image2XUrl { get; set; } = "";

    [JsonProperty("image_url")]
    public string ImageUrl { get; set; } = "";

    [JsonProperty("url")]
    public string Url { get; set; } = "";
}