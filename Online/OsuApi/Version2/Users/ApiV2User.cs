using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace osuToolsV2.Online.OsuApi.Version2.Users;

public class ApiV2User
{
    [JsonProperty("id")]
    public int Id { get; set; }
    
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

    [JsonProperty("username")]
    public string UserName { get; set; } = "";

    [JsonProperty("account_history")]
    public List<UserAccountHistory>? AccountHistory { get; set; }

    [JsonProperty("active_tournament_banner")]
    public ProfileBanner? ActiveTournamentBanner { get; set; }

    [JsonProperty("active_tournament_banners")]
    public List<ProfileBanner>? ActiveTournamentBanners { get; set; }

    [JsonProperty("badges")]
    public List<UserBadge>? Badges { get; set; }

    [JsonProperty("beatmap_playcounts_count")]
    public int? BeatmapPlaycountsCount { get; set; }
    
    [JsonProperty]
    public JToken? Blocks { get; set; }
    
    [JsonProperty]
    public JToken? Country { get; set; }
    
    [JsonProperty]
    public JToken? Cover { get; set; }

    [JsonProperty("favourite_beatmapset_count")]
    public int? FavouriteBeatmapsetCount { get; set; }

    [JsonProperty("follower_count")]
    public int? FollowerCount { get; set; }

    [JsonProperty("graveyard_beatmapset_count")]
    public int? GraveyardBeatmapsetCount { get; set; }

    [JsonProperty("guest_beatmapset_count")]
    public int? GuestBeatmapsetCount { get; set; }

    [JsonProperty("loved_beatmapset_count")]
    public int? LovedBeatmapsetCount { get; set; }

    [JsonProperty("mapping_follower_count")]
    public int? MappingFollowerCount { get; set; }

    [JsonProperty("pending_beatmapset_count")]
    public int? PendingBeatmapsetCount { get; set; }

    [JsonProperty("ranked_beatmapset_count")]
    public int? RankedBeatmapsetCount { get; set; }

    [JsonProperty("scores_best_count")]
    public int? ScoresBestCount { get; set; }

    [JsonProperty("scores_first_count")]
    public int? ScoresFirstCount { get; set; }

    [JsonProperty("scores_recent_count")]
    public int? ScoresRecentCount { get; set; }

    [JsonProperty("session_verified")]
    public bool? SessionVerified { get; set; }

    [JsonProperty("support_level")]
    public int? SupportLevel { get; set; }

    [JsonProperty("unread_pm_count")]
    public int? UnreadPmCount { get; set; }

    [JsonProperty("user_achievements")]
    public List<UserAchievement>? UserAchievements { get; set; }

    [JsonProperty("user_preferences")]
    public JObject? UserPreferences { get; set; }
}

public class ApiV2UserExtended : ApiV2User
{
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