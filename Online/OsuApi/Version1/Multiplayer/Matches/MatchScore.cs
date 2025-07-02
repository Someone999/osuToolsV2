using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;
using osuToolsV2.Score;

namespace osuToolsV2.Online.OsuApi.Version1.Multiplayer.Matches;

public class MatchScore
{
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int Slot { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public MatchTeam Team { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int UserId { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int Score { get; set; }
    
    [JsonProperty("maxcombo")]
    public int MaxCombo { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int Rank { get; set; }
    
    [JsonProperty("count50")]
    public int Count50 { get; set; }
    
    [JsonProperty("count100")]
    public int Count100 { get; set; }
    
    [JsonProperty("count300")]
    public int Count300 { get; set; }
    
    [JsonProperty("countgeki")]
    public int CountGeki { get; set; }
    
    [JsonProperty("countkatu")]
    public int CountKatu { get; set; }
    
    [JsonProperty("countmiss")]
    public int CountMiss { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int Perfect { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public int Pass { get; set; }
    
    [JsonProperty(NamingStrategyType = typeof (SnakeCaseNamingStrategy))]
    public LegacyGameMod EnabledMod { get; set; }

    public ScoreInfo ConvertToScoreInfo(Ruleset ruleset)
    {
        return new ScoreInfo()
        {
            CountGeki = CountGeki,
            CountKatu = CountKatu,
            Count300 = Count300,
            Count100 = Count100,
            Count50 = Count50,
            CountMiss = CountMiss,
            PlayerMaxCombo = MaxCombo,
            Mods = ModManager.FromLegacyMods(EnabledMod, ruleset),
            Perfect = Perfect == 1
        };
    }
}