using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;

namespace osuToolsV2.ScoreInfo;

public class CatchScoreInfo : IScoreInfo
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
        int all = CountKatu + Count300 + Count100 + Count50 + CountMiss;
        double ratio = Count300 + Count100 + Count50;
        return ratio / all;
    }
    public double GetCountGekiRate()
    {
        return 0;
    }
    public double GetCount300Rate()
    {
        return GetAccuracy();
    }
    public int GetHitObjectCount()
    {
        var hitObjs = Beatmap?.HitObjects;
        if (hitObjs == null)
        {
            return 0;
        }

        int total = hitObjs.Count;
        int juiceStreamCount = (from i in hitObjs where i.HitObjectType == HitObjectType.JuiceStream select i).Count();
        int bananaCount = (from i in hitObjs where i.HitObjectType == HitObjectType.Banana select i).Count();
        return total + juiceStreamCount - bananaCount;
    }
}