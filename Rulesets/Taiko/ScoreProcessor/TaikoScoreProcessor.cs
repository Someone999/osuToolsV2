using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Game;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Taiko.ScoreProcessor;

public class TaikoScoreProcessor : IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo)
    {
        int all = scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.CountMiss;
        double ratio = scoreInfo.Count300 + scoreInfo.Count100 * 0.5;
        return ratio / all;
    }

    public int GetPassedHitObjectCount(ScoreInfo scoreInfo)
    {
        return scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
    }

    public int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        var hitObjects = scoreInfo.Beatmap?.HitObjects;
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

    public double GetCount300Rate(ScoreInfo scoreInfo)
    {
        double all = scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.CountMiss;
        return scoreInfo.Count300 / all;
    }

    public double GetCountGekiRate(ScoreInfo scoreInfo) => 0;
   
    public GameGrade GetGrade(ScoreInfo scoreInfo)
    {
        bool all300 = scoreInfo is {Count100: 0, Count50: 0, CountMiss: 0};
        double rate300 = (double) scoreInfo.Count300 /
                         (scoreInfo.Count300 + scoreInfo.Count100 +scoreInfo.Count50 + scoreInfo.CountMiss);
        

        bool noMiss = scoreInfo.CountMiss == 0;
        if (Math.Abs(GetAccuracy(scoreInfo) - 1) < 1e-5 && all300)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.XH : GameGrade.X;
        }

        if (rate300 > 0.9 && noMiss)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.SH : GameGrade.S;
        }

        switch (rate300)
        {
            case > 0.9:
            case > 0.8 when noMiss:
                return GameGrade.A;
            case > 0.8:
            case > 0.7 when noMiss:
                return GameGrade.B;
            case > 0.6 when noMiss:
                return GameGrade.C;
            default:
                return GameGrade.D;
        }
    }

    public bool IsPerfect(ScoreInfo scoreInfo) => scoreInfo.Count100 == 0;

}