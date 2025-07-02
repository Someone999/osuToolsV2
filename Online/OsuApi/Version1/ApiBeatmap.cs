using HsManCommonLibrary.NestedValues.Attributes;
using Newtonsoft.Json;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version1;

public class ApiBeatmap
{
    [AutoAssign("approved")]
    [JsonProperty("approved")]
    public ApproveStatus ApproveStatus { get; private set; }
    
    [AutoAssign("submit_date")]
    [JsonProperty("submit_date")]
    public DateTime SubmitDate { get; private set; }
    
    [AutoAssign("approved_date")]
    [JsonProperty("approved_date")]
    public DateTime? ApprovedDate { get; private set; }
    
    [AutoAssign("last_update")]
    [JsonProperty("last_update")]
    public DateTime LastUpdate { get; private set; }
    
    [AutoAssign("artist")]
    [JsonProperty("artist")] 
    public string Artist { get; private set; } = "";
    
    [AutoAssign("beatmap_id")]
    [JsonProperty("beatmap_id")]
    public long BeatmapId { get; set; }
    
    [AutoAssign("beatmapset_id")]
    [JsonProperty("beatmapset_id")]
    public long BeatmapSetId { get; set; }
    
    [AutoAssign("bpm")]
    [JsonProperty("bpm")]
    public double Bpm { get; set; }

    [AutoAssign("creator")]
    [JsonProperty("creator")]
    public string Creator { get; set; } = "";
    
    [AutoAssign("creator_id")]
    [JsonProperty("creator_id")]
    public string CreatorId { get; set; } = "";

    [AutoAssign("difficultyrating")]
    [JsonProperty("difficultyrating")]
    public double StarRating { get; set; }

    [AutoAssign("diff_aim")]
    [JsonProperty("diff_aim")]
    public double? AimDifficulty { get; set; }

    [AutoAssign("diff_speed")]
    [JsonProperty("diff_speed")]
    public double? SpeedDifficulty { get; set; }

    [AutoAssign("diff_size")]
    [JsonProperty("diff_size")]
    public double CircleSize { get; set; }

    [AutoAssign("diff_overall")]
    [JsonProperty("diff_overall")]
    public double OverallDifficulty { get; set; }

    [AutoAssign("diff_approach")]
    [JsonProperty("diff_approach")]
    public double ApproachRate { get; set; }

    [AutoAssign("diff_drain")]
    [JsonProperty("diff_drain")]
    public double HpDrainRate { get; set; }

    [AutoAssign("hit_length")]
    [JsonProperty("hit_length")]
    public int HitLength { get; set; }

    [AutoAssign("source")]
    [JsonProperty("source")]
    public string Source { get; set; } = "";

    [AutoAssign("genre_id")]
    [JsonProperty("genre_id")]
    public int GenreId { get; set; }

    [AutoAssign("language_id")]
    [JsonProperty("language_id")]
    public string LanguageId { get; set; } = "";
    
    [AutoAssign("title")]
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [AutoAssign("total_length")]
    [JsonProperty("total_length")]
    public int TotalLength { get; set; }

    [AutoAssign("version")]
    [JsonProperty("version")]
    public string Version { get; set; } = "";

    [AutoAssign("file_md5")]
    [JsonProperty("file_md5")]
    public string FileMd5 { get; set; } = "";

    [AutoAssign("mode")]
    [JsonProperty("mode")]
    public LegacyRuleset Mode { get; set; }

    [AutoAssign("tags")]
    [JsonProperty("tags")]
    public string Tags { get; set; } = "";

    [AutoAssign("favourite_count")]
    [JsonProperty("favourite_count")]
    public int FavouriteCount { get; set; }

    [AutoAssign("rating")]
    [JsonProperty("rating")]
    public double Rating { get; set; }

    [AutoAssign("playcount")]
    [JsonProperty("playcount")]
    public int PlayCount { get; set; }

    [AutoAssign("passcount")]
    [JsonProperty("passcount")]
    public int PassCount { get; set; }

    [AutoAssign("count_normal")]
    [JsonProperty("count_normal")]
    public int CountNormal { get; set; }

    [AutoAssign("count_slider")]
    [JsonProperty("count_slider")]
    public int CountSlider { get; set; }

    [AutoAssign("count_spinner")]
    [JsonProperty("count_spinner")]
    public int CountSpinner { get; set; }

    [AutoAssign("max_combo")]
    [JsonProperty("max_combo", NullValueHandling = NullValueHandling.Ignore)]
    public int MaxCombo { get; set; }

    [AutoAssign("approved")]
    [JsonProperty("storyboard")]
    public int Storyboard { get; set; }

    [AutoAssign("video")]
    [JsonProperty("video")]
    public int Video { get; set; }

    [AutoAssign("download_unavailable")]
    [JsonProperty("download_unavailable")]
    public int DownloadUnavailable { get; set; }

    [AutoAssign("download_unavailable")]
    [JsonProperty("audio_unavailable")]
    public int AudioUnavailable { get; set; }
}
