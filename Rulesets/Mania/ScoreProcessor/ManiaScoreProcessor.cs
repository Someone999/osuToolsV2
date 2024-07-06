using osuToolsV2.Beatmaps.HitObjects.Mania;
using osuToolsV2.Game;
using osuToolsV2.Game.Mods;
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

        double decay;
        if (scoreInfo.Mods == null)
        {
            decay = 1.0;
        }
        else
        {
            decay = scoreInfo.Mods.Any(m => m is ScoreV2Mod) ? 0.9836 : 1.0;
        }

        const double countGekiRatio = 1.0;
        double count300Ratio = 1.0 * decay;
        double countKatuRatio = 2.0 / 3 * decay;
        double count100Ratio = 1.0 / 3 * decay;
        double count50Ratio = 1.0 / 6 * decay;

        double countGekiFactor = scoreInfo.CountGeki * countGekiRatio;
        double count300Factor = scoreInfo.Count300 * count300Ratio;
        double countKatuFactor = scoreInfo.CountKatu * countKatuRatio;
        double count100Factor = scoreInfo.Count100 * count100Ratio;
        double count50Factor = scoreInfo.Count50 * count50Ratio;
        double totalFactor = countGekiFactor + count300Factor + countKatuFactor + count100Factor + count50Factor;
        
        return totalFactor / all;
    }

    public int GetPassedHitObjectCount(ScoreInfo scoreInfo)
    {
        return scoreInfo.CountGeki + scoreInfo.Count300 + scoreInfo.CountKatu + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
    }

    public int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        var hitObjects = scoreInfo.Beatmap?.HitObjects;
        if (hitObjects == null)
        {
            return 0;
        }
        var tmpHitObjectCount = hitObjects.Count;
        
        var modList = scoreInfo.Mods;
        if (modList == null)
        {
            return tmpHitObjectCount;
        }
        
        var hasScoreV2Mod = modList.Any(m => m is ManiaScoreV2Mod);
        if (!hasScoreV2Mod)
        {
            return tmpHitObjectCount;
        }
        
        var holdCount = hitObjects.Count(h => h is ManiaHold);
        return tmpHitObjectCount + holdCount;
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

    public GameGrade GetGrade(ScoreInfo scoreInfo)
    {
        var acc = GetAccuracy(scoreInfo);
        if (Math.Abs(acc - 1) < 1e-5)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.XH : GameGrade.X;
        }

        return acc switch
        {
            > 0.95 => scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.SH : GameGrade.S,
            > 0.9 => GameGrade.A,
            > 0.8 => GameGrade.B,
            _ => GetAccuracy(scoreInfo) > 0.7 ? GameGrade.C : GameGrade.D
        };
    }

    public bool IsPerfect(ScoreInfo scoreInfo)
    {
        return scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss == 0;
    }
}