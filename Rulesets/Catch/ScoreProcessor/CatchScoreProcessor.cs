using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Game;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Catch.ScoreProcessor;

public class CatchScoreProcessor : IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo)
    {
         int all = scoreInfo.CountKatu + scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
         double ratio = scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50;
         return ratio / all;
    }

    public int GetPassedHitObject(ScoreInfo scoreInfo)
    {
        return scoreInfo.Count300;
    }

    public int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        var hitObjs = scoreInfo.Beatmap?.HitObjects;
        if (hitObjs == null)
        {
            return 0;
        }

        int total = hitObjs.Count;
        int juiceStreamCount = (from i in hitObjs where i.HitObjectType == HitObjectType.JuiceStream select i).Count();
        int bananaCount = (from i in hitObjs where i.HitObjectType == HitObjectType.Banana select i).Count();
        return total + juiceStreamCount - bananaCount;
    }

    public double GetCount300Rate(ScoreInfo scoreInfo) => GetAccuracy(scoreInfo);
   

    public double GetCountGekiRate(ScoreInfo scoreInfo) => 0;
    

    public GameRanking GetRanking(ScoreInfo scoreInfo)
    {
        var acc = GetAccuracy(scoreInfo);
        if (Math.Abs(acc - 1) < 1e-5)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false? GameRanking.XH : GameRanking.X;
        }

        return acc switch
        {
            > 0.98 => scoreInfo.Mods?.IsHiddenMods ?? false ? GameRanking.SH : GameRanking.S,
            > 0.94 => GameRanking.A,
            > 0.9 => GameRanking.B,
            _ => acc > 0.85 ? GameRanking.C : GameRanking.D
        };
    }

    public bool IsPerfect(ScoreInfo scoreInfo) => scoreInfo is {CountKatu: 0, CountMiss: 0};
    
}