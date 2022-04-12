using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;

namespace osuToolsV2.ScoreInfo;

public class TaikoScoreInfo : IScoreInfo
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
        int all = Count300 + Count100 + CountMiss;
        double ratio = Count300 + Count100 * 0.5;
        return ratio / all;
    }
    public double GetCountGekiRate()
    {
        return 0;
    }
    public double GetCount300Rate()
    {
        double all = Count300 + Count100;
        return Count300 / all;
    }
    
    public int GetHitObjectCount()
    {
        var hitObjects = Beatmap?.HitObjects;
        if (hitObjects == null)
        {
            return 0;
        }
        return (from 
            hitObj
            in hitObjects 
            where hitObj.HitObjectType != HitObjectType.Drumroll 
            select hitObj).Count();
    }
}