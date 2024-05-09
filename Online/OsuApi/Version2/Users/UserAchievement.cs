using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Users;

public class UserAchievement
{
    [JsonProperty("achieved_at")]
    public string AchievedAt { get; set; } = "";

    [JsonProperty("achievement_id")]
    public int AchievementId { get; set; }
}