using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class Availability
{
    [JsonProperty("download_disabled")]
    public bool DownloadDisabled { get; set; }

    [JsonProperty("more_information")]
    public string MoreInformation { get; set; } = "";

    [JsonProperty("discussion_enabled")]
    [Obsolete("Deprecated, all beatmapsets now have discussion enabled.")]
    public bool DiscussionEnabled { get; set; }

    [JsonProperty("discussion_locked")]
    public bool DiscussionLocked { get; set; }

    [JsonProperty("is_scoreable")]
    public bool IsScoreable { get; set; }

    [JsonProperty("storyboard")]
    public bool Storyboard { get; set; }
}