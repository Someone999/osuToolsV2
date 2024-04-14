using HsManCommonLibrary.NestedValues.Attributes;
using Newtonsoft.Json;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version1;

public class ApiBeatmap
{
    [JsonProperty("approved")]
    public ApproveStatus ApproveStatus { get; private set; }
    
    [JsonProperty("submit_date")]
    public DateTime SubmitDate { get; private set; }
    
    [JsonProperty("approved_date")]
    public DateTime ApprovedDate { get; private set; }
    
    [JsonProperty("last_update")]
    public DateTime LastUpdate { get; private set; }
    
    [JsonProperty("artist")] 
    public string Artist { get; private set; } = "";
    
    [JsonProperty("beatmap_id")]
    public long BeatmapId { get; set; }
    
    [JsonProperty("beatmapset_id")]
    public long BeatmapSetId { get; set; }
    
    [JsonProperty("bpm")]
    public double Bpm { get; set; }

    [JsonProperty("creator")]
    public string Creator { get; set; } = "";

    [JsonProperty("creator_id")]
    public string CreatorId { get; set; } = "";

    [JsonProperty("difficultyrating")]
    public double StarRating { get; set; }

    [JsonProperty("diff_aim")]
    public double AimDifficulty { get; set; }

    [JsonProperty("diff_speed")]
    public double SpeedDifficulty { get; set; }

    [JsonProperty("diff_size")]
    public double CircleSize { get; set; }

    [JsonProperty("diff_overall")]
    public double OverallDifficulty { get; set; }

    [JsonProperty("diff_approach")]
    public double ApproachRate { get; set; }

    [JsonProperty("diff_drain")]
    public double HpDrainRate { get; set; }

    [JsonProperty("hit_length")]
    public int HitLength { get; set; }

    [JsonProperty("source")]
    public string Source { get; set; } = "";

    [JsonProperty("genre_id")]
    public int GenreId { get; set; }

    [JsonProperty("language_id")]
    public string LanguageId { get; set; } = "";
    
    [JsonProperty("title")]
    public string Title { get; set; } = "";

    [JsonProperty("total_length")]
    public int TotalLength { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; } = "";

    [JsonProperty("file_md5")]
    public string FileMd5 { get; set; } = "";

    [JsonProperty("mode")]
    public LegacyRuleset Mode { get; set; }

    [JsonProperty("tags")]
    public string Tags { get; set; } = "";

    [JsonProperty("favourite_count")]
    public int FavouriteCount { get; set; }

    [JsonProperty("rating")]
    public double Rating { get; set; }

    [JsonProperty("playcount")]
    public int PlayCount { get; set; }

    [JsonProperty("passcount")]
    public int PassCount { get; set; }

    [JsonProperty("count_normal")]
    public int CountNormal { get; set; }

    [JsonProperty("count_slider")]
    public int CountSlider { get; set; }

    [JsonProperty("count_spinner")]
    public int CountSpinner { get; set; }

    [JsonProperty("max_combo")]
    public int MaxCombo { get; set; }

    [JsonProperty("storyboard")]
    public int Storyboard { get; set; }

    [JsonProperty("video")]
    public int Video { get; set; }

    [JsonProperty("download_unavailable")]
    public int DownloadUnavailable { get; set; }

    [JsonProperty("audio_unavailable")]
    public int AudioUnavailable { get; set; }
}
