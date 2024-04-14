using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class BeatmapsetDiscussionPost
{
    [JsonProperty("beatmapset_discussion_id")]
    public int BeatmapSetDiscussionId { get; internal set; }

    [JsonProperty("created_at")]
    public DateTime CreatedAt { get; internal set; }

    [JsonProperty("deleted_at")]
    public DateTime? DeletedAt { get; internal set; }

    [JsonProperty("deleted_by_id")]
    public int? DeletedById { get; internal set; }

    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("last_editor_id")]
    public int? LastEditorId { get; internal set; }

    [JsonProperty("message")]
    public string Message { get; internal set; } = "";

    [JsonProperty("system")]
    public bool System { get; internal set; }

    [JsonProperty("updated_at")]
    public DateTime UpdatedAt { get; internal set; }

    [JsonProperty("user_id")]
    public int UserId { get; internal set; }
}