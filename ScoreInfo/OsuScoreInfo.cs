using osuToolsV2.Beatmaps;
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
    public IBeatmap? Beatmap { get; set; }
    public Ruleset? Ruleset { get; set; }
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
   
}