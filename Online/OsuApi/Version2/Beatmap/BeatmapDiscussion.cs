using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class BeatmapDiscussion
{
    [JsonProperty("beatmap")]
    public ApiV2BeatmapExtended? Beatmap { get; internal set; }

    [JsonProperty("beatmap_id")]
    public int? BeatmapId { get; internal set; }

    [JsonProperty("beatmapset")]
    public ApiV2BeatmapSetExtened? Beatmapset { get; internal set; }

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

public class User
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("username")]
    public string Username { get; set; } = "";

    [JsonProperty("avatar_url")]
    public string AvatarUrl { get; set; } = "";

    [JsonProperty("country_code")]
    public string CountryCode { get; set; } = "";

    [JsonProperty("is_active")]
    public bool IsActive { get; set; }

    [JsonProperty("is_bot")]
    public bool IsBot { get; set; }

    [JsonProperty("is_deleted")]
    public bool IsDeleted { get; set; }

    [JsonProperty("is_online")]
    public bool IsOnline { get; set; }

    [JsonProperty("is_supporter")]
    public bool IsSupporter { get; set; }

    [JsonProperty("last_visit")]
    public DateTime LastVisit { get; set; }

    [JsonProperty("pm_friends_only")]
    public bool PmFriendsOnly { get; set; }

    [JsonProperty("profile_colour")]
    public string? ProfileColour { get; set; } 

    [JsonProperty("account_history")]
    public List<UserAccountHistory>? AccountHistory { get; set; }

    [JsonProperty("active_tournament_banner")]
    public ProfileBanner? ActiveTournamentBanner { get; set; }

    [JsonProperty("active_tournament_banners")]
    public List<ProfileBanner>? ActiveTournamentBanners { get; set; }

    [JsonProperty("badges")]
    public List<UserBadge>? Badges { get; set; }

    [JsonProperty("beatmap_playcounts_count")]
    public int BeatmapPlaycountsCount { get; set; }

    [JsonProperty("favourite_beatmapset_count")]
    public int FavouriteBeatmapsetCount { get; set; }

    [JsonProperty("follower_count")]
    public int FollowerCount { get; set; }

    [JsonProperty("graveyard_beatmapset_count")]
    public int GraveyardBeatmapsetCount { get; set; }

    [JsonProperty("guest_beatmapset_count")]
    public int GuestBeatmapsetCount { get; set; }

    [JsonProperty("loved_beatmapset_count")]
    public int LovedBeatmapsetCount { get; set; }

    [JsonProperty("mapping_follower_count")]
    public int MappingFollowerCount { get; set; }

    [JsonProperty("pending_beatmapset_count")]
    public int PendingBeatmapsetCount { get; set; }

    [JsonProperty("ranked_beatmapset_count")]
    public int RankedBeatmapsetCount { get; set; }

    [JsonProperty("scores_best_count")]
    public int ScoresBestCount { get; set; }

    [JsonProperty("scores_first_count")]
    public int ScoresFirstCount { get; set; }

    [JsonProperty("scores_recent_count")]
    public int ScoresRecentCount { get; set; }

    [JsonProperty("session_verified")]
    public bool SessionVerified { get; set; }

    [JsonProperty("support_level")]
    public int SupportLevel { get; set; }

    [JsonProperty("unread_pm_count")]
    public int UnreadPmCount { get; set; }

    [JsonProperty("user_achievements")]
    public List<UserAchievement>? UserAchievements { get; set; }

    /*[JsonProperty("user_preferences")]
    public UserPreferences UserPreferences { get; set; }*/

    [JsonProperty("cover_url")] 
    public string CoverUrl { get; set; } = "";

    [JsonProperty("discord")]
    public string Discord { get; set; } = "";

    [JsonProperty("has_supported")]
    public bool HasSupported { get; set; }

    [JsonProperty("interests")]
    public string Interests { get; set; } = "";

    [JsonProperty("join_date")]
    public DateTime JoinDate { get; set; }

    [JsonProperty("location")]
    public string Location { get; set; } = "";

    [JsonProperty("max_blocks")]
    public int MaxBlocks { get; set; }

    [JsonProperty("max_friends")]
    public int MaxFriends { get; set; }

    [JsonProperty("occupation")]
    public string Occupation { get; set; } = "";

    [JsonProperty("playmode")]
    public string Playmode { get; set; } = "";

    [JsonProperty("playstyle")]
    public List<string> Playstyle { get; set; } = new List<string>();

    [JsonProperty("post_count")]
    public int PostCount { get; set; }

    [JsonProperty("profile_order")]
    public List<string> ProfileOrder { get; set; } = new List<string>();

    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [JsonProperty("title_url")]
    public string TitleUrl { get; set; } = "";

    [JsonProperty("twitter")]
    public string Twitter { get; set; } = "";

    [JsonProperty("website")]
    public string Website { get; set; } = "";
}

public class UserAccountHistory
{
    [JsonProperty("description")]
    public string Description { get; set; } = "";

    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("length")]
    public int Length { get; set; }

    [JsonProperty("permanent")]
    public bool Permanent { get; set; }

    [JsonProperty("timestamp")]
    public DateTime Timestamp { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = "";
}

public class ProfileBanner
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("tournament_id")]
    public int TournamentId { get; set; }

    [JsonProperty("image")]
    public string Image { get; set; } = "";

    [JsonProperty("image@2x")]
    public string Image2x { get; set; } = "";
}

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
public static class ApiV2UserType
{
    public const string Note = "note";
    public const string Restriction = "restriction";
    public const string Silence = "silence";
}

public class UserAchievement
{
    [JsonProperty("achieved_at")]
    public string AchievedAt { get; set; } = "";

    [JsonProperty("achievement_id")]
    public int AchievementId { get; set; }
}