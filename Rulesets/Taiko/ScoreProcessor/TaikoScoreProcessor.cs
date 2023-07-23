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

    public int GetPassedHitObject(ScoreInfo scoreInfo)
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
        double all = scoreInfo.Count300 + scoreInfo.Count100;
        return scoreInfo.Count300 / all;
    }

    public double GetCountGekiRate(ScoreInfo scoreInfo) => 0;
   
    public GameRanking GetRanking(ScoreInfo scoreInfo)
    {
        bool all300 = scoreInfo is {Count100: 0, Count50: 0, CountMiss: 0};
        double rate300 = (double) scoreInfo.Count300 /
                         (scoreInfo.Count300 + scoreInfo.Count100 +scoreInfo.Count50 + scoreInfo.CountMiss);
        

        bool noMiss = scoreInfo.CountMiss == 0;
        if (Math.Abs(GetAccuracy(scoreInfo) - 1) < 1e-5 && all300)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameRanking.XH : GameRanking.X;
        }

        if (rate300 > 0.9)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameRanking.SH : GameRanking.S;
        }

        switch (rate300)
        {
            case > 0.9:
            case > 0.8 when noMiss:
                return GameRanking.A;
            case > 0.8:
            case > 0.7 when noMiss:
                return GameRanking.B;
            case > 0.6 when noMiss:
                return GameRanking.C;
            default:
                return GameRanking.D;
        }
    }

    public bool IsPerfect(ScoreInfo scoreInfo) => scoreInfo.Count100 == 0;

}