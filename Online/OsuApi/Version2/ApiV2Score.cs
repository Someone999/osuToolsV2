using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Online.OsuApi.Version2.Beatmap;
using osuToolsV2.Online.OsuApi.Version2.Multiplayer;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Online.OsuApi.Version2;

public class ApiV2Score
{
    public int? Id { get; set; }
    public double Accuracy { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int BeatmapId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? BestId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? BuildId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? ClassicTotalScore { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime EndedAt { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public bool HasReplay { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public bool IsPerfectCombo { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public bool LegacyPerfect { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int LegacyScoreId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int LegacyTotalScore { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int MaxCombo { get; set; }
    
    [JsonProperty]
    public string[] Mods { get; set; } = Array.Empty<string>();
    
    [JsonProperty]
    public bool Passed { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? PlaylistItemId { get; set; } 
    
    [JsonProperty]
    public double? Pp { get; set; } 
    
    [JsonProperty]
    public bool? Preserve { get; set; } 
    
    [JsonProperty]
    public bool? Processed { get; set; } 
    
    [JsonProperty]
    public string Rank { get; set; } = "";
    
    [JsonProperty]
    public bool? Ranked { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int? RoomId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int RulesetId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public DateTime? StartedAt { get; set; } 
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public ApiV2ScoreStatistics MaximumStatistics { get; set; } = new ApiV2ScoreStatistics();
    
    [JsonProperty]
    public ApiV2ScoreStatistics Statistics { get; set; } = new ApiV2ScoreStatistics();
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int TotalScore { get; set; }
    [JsonProperty("type")]
    public string ScoreType { get; set; } = "";
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public int UserId { get; set; }
}

public class ApiV2MatchScore : ApiV2Score
{
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public JToken CurrentUserAttributes { get; set; } = JValue.CreateNull();
    
    public int? Position { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public ApiV2MultiplayerScoresAround? ScoresAround { get; set; }

    [JsonProperty("match")]
    public ApiV2MatchSlot? MatchSlot { get; set; }
    
    public ApiV2Beatmap? Beatmap { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
    public ApiV2BeatmapSetExtended? BeatmapSet { get; set; }
}