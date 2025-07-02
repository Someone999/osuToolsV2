using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class BeatmapDiscussion
{
    [JsonProperty("beatmap")]
    public ApiV2Beatmap? Beatmap { get; internal set; }

    [JsonProperty("beatmap_id")]
    public int? BeatmapId { get; internal set; }

    [JsonProperty("beatmapset")]
    public ApiV2BeatmapSet? Beatmapset { get; internal set; }

    [JsonProperty("beatmapset_id")]
    public int BeatmapsetId { get; internal set; }

    [JsonProperty("can_be_resolved")]
    public bool CanBeResolved { get; internal set; }

    [JsonProperty("can_grant_kudosu")]
    public bool CanGrantKudosu { get; internal set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; internal set; }

    [JsonProperty("current_user_attributes")]
    public JObject CurrentUserAttributes { get; internal set; } = new JObject();

    [JsonProperty("deleted_at")]
    public DateTime? DeletedAt { get; internal set; }

    [JsonProperty("deleted_by_id")]
    public int? DeletedById { get; internal set; }

    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("kudosu_denied")]
    public bool KudosuDenied { get; internal set; }

    [JsonProperty("last_post_at")]
    public DateTime LastPostAt { get; internal set; }

    /// <summary>
    /// Definitions are at <see cref="osuToolsV2.Online.OsuApi.Version2.Beatmap.MessageType"/>
    /// </summary>
    [JsonProperty("message_type")]
    public string MessageType { get; internal set; } = "";

    [JsonProperty("parent_id")]
    public int? ParentId { get; internal set; }

    [JsonProperty("posts")]
    public BeatmapsetDiscussionPost[]? Posts { get; internal set; }

    [JsonProperty("resolved")]
    public bool Resolved { get; internal set; }

    [JsonProperty("starting_post")]
    public BeatmapsetDiscussionPost? StartingPost { get; internal set; }

    [JsonProperty("timestamp")]
    public int? Timestamp { get; internal set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; internal set; }

    [JsonProperty("user_id")]
    public int UserId { get; internal set; }
}