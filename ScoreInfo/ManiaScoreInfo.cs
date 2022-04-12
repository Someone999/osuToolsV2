using osuToolsV2.Beatmaps;

namespace osuToolsV2.ScoreInfo;

public class ManiaScoreInfo : IScoreInfo
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
    public double GetAccuracy()
    {
        int all = CountGeki + CountKatu + Count300 + Count100 + Count50 + CountMiss;
        double ratio = CountGeki + Count300 + CountKatu * (2 / 3.0) + Count100 * (1 / 3.0) + Count50 * (1 / 6.0);
        return ratio / all;
    }
    public double GetCountGekiRate()
    {
        double all = CountGeki + Count300;
        return CountGeki / all;
    }
    public double GetCount300Rate()
    {
        int all = CountGeki + CountKatu + Count300 + Count100 + Count50 + CountMiss;
        double count300 = CountGeki + Count300;
        return count300 / all;
    }
    public int GetHitObjectCount()
    {
        return Beatmap?.HitObjects?.Count ?? 0;
    }
}