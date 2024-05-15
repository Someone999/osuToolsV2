using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Score;

public class ScoreInfo
{
    public int CountGeki { get; set; }
    public int CountKatu { get; set; }
    public int Count300 { get; set; }
    public int Count100 { get; set; }
    public int Count50 { get; set; }
    public int CountMiss { get; set; }
    public int? Score { get; set; }
    public int MaxCombo { get; set; }
    public bool Perfect { get; set; }
    public int Combo { get; set; }
    public IBeatmap? Beatmap { get; set; }
    public ModList? Mods { get; set; }
    public List<double>? HitErrors { get; set; }
    public double? UnstableRate { get; set; }
    public int? SliderBreaks { get; set; }
    public int? PlayerMaxCombo { get; set; }
    
    public DateTime PlayTime { get; set; }
    
    public string? PlayerName { get; set; }
    
    public Ruleset? Ruleset { get; set; }

    public void Clear()
    {
        CountGeki = 0;
        CountKatu = 0;
        Count300 = 0;
        Count100 = 0;
        Count50 = 0;
        CountMiss = 0;
        Score = null;
        MaxCombo = 0;
        Perfect = false;
        Combo = 0;
        Beatmap = null;
        Mods = null;
        HitErrors = null;
        UnstableRate = null;
        SliderBreaks = null;
        PlayerMaxCombo = null;
        PlayerName = null;
        PlayTime = new DateTime();
    }
}