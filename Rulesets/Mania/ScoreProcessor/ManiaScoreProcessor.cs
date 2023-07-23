using osuToolsV2.Game;
using osuToolsV2.Rulesets.Mania.Mods;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Mania.ScoreProcessor;

public class ManiaScoreProcessor : IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo)
    {
        int all = scoreInfo.CountGeki + scoreInfo.CountKatu + scoreInfo.Count300 + 
                  scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
        double ratio = scoreInfo.CountGeki + scoreInfo.Count300 + scoreInfo.CountKatu * (2 / 3.0) + 
                       scoreInfo.Count100 * (1 / 3.0) + scoreInfo.Count50 * (1 / 6.0);
        return ratio / all;
    }

    public int GetPassedHitObject(ScoreInfo scoreInfo)
    {
        return scoreInfo.CountGeki + scoreInfo.Count300 + scoreInfo.CountKatu + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
    }

    public int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        var tmpHitObjectCount = scoreInfo.Beatmap?.HitObjects?.Count ?? 0;
        var modList = scoreInfo.Mods;
        if (modList == null)
        {
            return tmpHitObjectCount;
        }

        return modList.Contains(new ManiaScoreV2Mod()) ? tmpHitObjectCount * 2 : tmpHitObjectCount;
    }

    public double GetCount300Rate(ScoreInfo scoreInfo)
    {
        int all = scoreInfo.CountGeki + scoreInfo.CountKatu + scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
        double count300 = scoreInfo.CountGeki + scoreInfo.Count300;
        return count300 / all;
    }

    public double GetCountGekiRate(ScoreInfo scoreInfo)
    {
        double all = scoreInfo.CountGeki + scoreInfo.Count300;
        return scoreInfo.CountGeki / all;
    }

    public GameRanking GetRanking(ScoreInfo scoreInfo)
    {
        var acc = GetAccuracy(scoreInfo);
        if (Math.Abs(acc - 1) < 1e-5)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameRanking.XH : GameRanking.X;
        }

        return acc switch
        {
            > 0.95 => scoreInfo.Mods?.IsHiddenMods ?? false ? GameRanking.SH : GameRanking.S,
            > 0.9 => GameRanking.A,
            > 0.8 => GameRanking.B,
            _ => GetAccuracy(scoreInfo) > 0.7 ? GameRanking.C : GameRanking.D
        };
    }

    public bool IsPerfect(ScoreInfo scoreInfo)
    {
        return scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss == 0;
    }
}