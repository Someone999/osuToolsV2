using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class ApiV2Beatmap
{
    [JsonProperty("beatmapset_id")]
    public int BeatmapSetId { get; internal set; }

    [JsonProperty("difficulty_rating")]
    public float DifficultyRating { get; internal set; }

    [JsonProperty("id")]
    public int Id { get; internal set; }

    [JsonProperty("mode")]
    public string Mode { get; internal set; } = "";

    [JsonProperty("status")]
    public string Status { get; internal set; } = "";

    [JsonProperty("total_length")]
    public int TotalLength { get; internal set; }

    [JsonProperty("user_id")]
    public int UserId { get; internal set; }

    [JsonProperty("version")]
    public string Version { get; internal set; } = "";
}


public class ApiV2BeatmapExtended : ApiV2Beatmap
{
    [JsonProperty]
    public float Accuracy { get; internal set; }
    
    [JsonProperty("ar")]
    public float ApproachingRate { get; internal set; }
    
    [JsonProperty("cs")]
    public float CircleSize { get; internal set; }
    
    [JsonProperty("drain")]
    public float HpDrain { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int HitLength { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime? DeletedAt { get; internal set; } 
    
    [JsonProperty]
    public float? Bpm { get; internal set; }
    
    [JsonProperty]
    public bool Converted { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int CountCircles { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int CountSliders { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int CountSpinners { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public bool IsScoreable { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime LastUpdated { get; internal set; }
    
    [JsonProperty("mode_int")]
    public int ModeInt { get; internal set; }
    
    [JsonProperty("passcount")]
    public int PassCount { get; internal set; }
    
    [JsonProperty("playcount")]
    public int PlayCount { get; internal set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public ApiV2BeatmapRankStatus Ranked { get; internal set; }

    [JsonProperty]
    public string Url { get; internal set; } = "";
    
    [JsonProperty]
    public string? Checksum { get; set; }
    
    [JsonProperty("beatmapset")]
    public ApiV2BeatmapSetExtended? BeatmapSet { get; internal set; } 
    
    [JsonProperty("failtimes")]
    public FailTimes FailTimes { get; internal set; } = new FailTimes();
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int MaxCombo { get; internal set; }
    
    

    public ApiV2ConvertBeatmap ToApiV2ConvertBeatmap()
    {
        ApiV2ConvertBeatmap convertBeatmap = new ApiV2ConvertBeatmap
        {
            DifficultyAttributes =
            {
                OverallDifficulty = Accuracy,
                HpDrain = HpDrain,
                CircleSize = CircleSize,
                ApproachRate = ApproachingRate,
                Stars = DifficultyRating
            },
            Bpm = Bpm ?? 0,
            Metadata =
            {
                Artist = BeatmapSet?.Artist ?? "",
                ArtistUnicode = BeatmapSet?.ArtistUnicode ?? "",
                Title = BeatmapSet?.Title ?? "",
                TitleUnicode = BeatmapSet?.TitleUnicode ?? "",
                Creator = BeatmapSet?.Creator ?? "",
                Tags = BeatmapSet?.Tags ?? "",
                Source = BeatmapSet?.Source ?? "",
                Version = Version,
                BeatmapSetId = BeatmapSetId,
                BeatmapId = Id,
                Md5Hash = Checksum
            },
            Ruleset = Ruleset.FromLegacyRuleset((LegacyRuleset) ModeInt)
        };
        
        return convertBeatmap;
    }
}