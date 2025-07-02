using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class ApiV2BeatmapSet
{
    [JsonProperty("artist")]
    public string Artist { get; set; }= "";

    [JsonProperty("artist_unicode")]
    public string ArtistUnicode { get; set; }= "";

    [JsonProperty("covers")]
    public Covers Covers { get; internal set; } = new Covers();

    [JsonProperty("creator")]
    public string Creator { get; internal set; } = "";

    [JsonProperty("favourite_count")]
    public int FavouriteCount { get; internal set; }

    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("nsfw")]
    public bool Nsfw { get; internal set; }

    [JsonProperty("offset")]
    public int Offset { get; internal set; }

    [JsonProperty("play_count")]
    public int PlayCount { get; internal set; }

    [JsonProperty("preview_url")]
    public string PreviewUrl { get; internal set; }= "";

    [JsonProperty("source")]
    public string Source { get; internal set; }= "";

    [JsonProperty("status")]
    public string Status { get; internal set; }= "";

    [JsonProperty("spotlight")]
    public bool Spotlight { get; internal set; }

    [JsonProperty("title")]
    public string Title { get; internal set; }= "";

    [JsonProperty("title_unicode")]
    public string TitleUnicode { get; internal set; }= "";

    [JsonProperty("user_id")]
    public int UserId { get; internal set; }

    [JsonProperty("video")]
    public bool Video { get; internal set; }
    
    
}

public class ApiV2BeatmapSetExtended : ApiV2BeatmapSet
{

    [JsonProperty("availability")]
    public Availability Availability { get; internal set; } = new Availability();
    
    [JsonProperty("bpm")]
    public float Bpm { get; internal set; }

    [JsonProperty("can_be_hyped")]
    public bool CanBeHyped { get; internal set; }

    [JsonProperty("deleted_at")]
    public DateTime? DeletedAt { get; internal set; }
    
    [JsonProperty("hype")]
    public HypeState? Hype { get; internal set; }

    [JsonProperty("last_updated")]
    public DateTime LastUpdated { get; internal set; }

    [JsonProperty("legacy_thread_url")]
    public string LegacyThreadUrl { get; internal set; } = "";

    [JsonProperty("nominations_summary")]
    public NominationSummary? NominationsSummary { get; internal set; }

    [JsonProperty("ranked")]
    public ApiV2BeatmapRankStatus Ranked { get; internal set; }

    [JsonProperty("ranked_date")]
    public DateTime? RankedDate { get; internal set; }

    [JsonProperty("submitted_date")]
    public DateTime? SubmittedDate { get; internal set; }

    [JsonProperty("tags")]
    public string Tags { get; internal set; } = "";
    
    [JsonProperty("has_favourited")]
    public bool IsFavourite { get; internal set; }
    
    [JsonProperty]
    public List<ApiV2Beatmap>? Beatmaps { get; internal set; }
    
    [JsonProperty]
    public JArray? Converts { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public Notation[] CurrentNotations { get; internal set; } = Array.Empty<Notation>();
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public JArray? CurrentUserAttributes { get; internal set; }

    [JsonProperty]
    public Dictionary<string, string>? Description { get; internal set; }
    
    [JsonProperty]
    public List<ApiV2Event<BeatmapSetEventData>>? Events { get; internal set; }
}