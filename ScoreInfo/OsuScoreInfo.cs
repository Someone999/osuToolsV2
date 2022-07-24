using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;

namespace osuToolsV2.ScoreInfo;

public sealed class OsuScoreInfo : IScoreInfo
{

    public int CountGeki { get; set; }
    public int CountKatu { get; set; }
    public int Count300 { get; set; }
    public int Count100 { get; set; }
    public int Count50 { get; set; }
    public int CountMiss { get; set; }
    public int? Score { get; set; }
    public int MaxCombo { get; set; }

    private bool? _perfect;
    
    public bool Perfect
    {
        get => _perfect ?? false;
        set => _perfect = value;
    }
    public int Combo { get; set; }
    public IBeatmap? Beatmap { get; set; }
    public ModList? Mods { get; set; }
    public double GetAccuracy()
    {
        int all = Count300 + Count100 + Count50 + CountMiss;
        double ratio = Count300 + Count100 * (1.0 / 3) + Count50 * (1.0 / 6);
        return ratio / all;

    }
    public double GetCountGekiRate()
    {
        double all = CountGeki + CountKatu;
        return CountGeki / all;
    }
    public double GetCount300Rate()
    {
        double all = Count300 + Count100 + Count50;
        return Count300 / all;
    }
    public int GetHitObjectCount()
    {
        return Beatmap?.HitObjects?.Count ?? 0;
    }
    public void Clear()
    {
        CountGeki = 0;
        Count300 = 0;
        CountKatu = 0;
        Count100 = 0;
        Count50 = 0;
        CountMiss = 0;
        Combo = 0;
        MaxCombo = 0;
        if (Score != null)
        {
            Score = 0;
        }
    }

}