﻿using osuToolsV2.Game;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Osu.ScoreProcessor;

public class OsuScoreProcessor : IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo)
    {
         int all = scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
                    double ratio = scoreInfo.Count300 + scoreInfo.Count100 * (1.0 / 3) + scoreInfo.Count50 * (1.0 / 6);
                    return ratio / all;
    }

    public int GetPassedHitObjectCount(ScoreInfo scoreInfo)
    {
        return scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
    }

    public int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        return scoreInfo.Beatmap?.HitObjects?.Count ?? 0;
    }

    public double GetCount300Rate(ScoreInfo scoreInfo)
    {
        double all = scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss;
        return scoreInfo.Count300 / all;
    }

    public double GetCountGekiRate(ScoreInfo scoreInfo)
    {
        double all = scoreInfo.CountGeki + scoreInfo.CountKatu;
        return scoreInfo.CountGeki / all;
    }

    public GameGrade GetGrade(ScoreInfo scoreInfo)
    {
        bool all300 = scoreInfo.Count100 == 0 && scoreInfo is {Count50: 0, CountMiss: 0};
        double rate300 = (double) scoreInfo.Count300 / (scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss);
        
        double rate50 = (double) scoreInfo.Count50 / (scoreInfo.Count300 + scoreInfo.Count100 + scoreInfo.Count50 + scoreInfo.CountMiss);

        bool noMiss = scoreInfo.CountMiss == 0;
        if (Math.Abs(GetAccuracy(scoreInfo) - 1) < 1e-5 && all300)
        {
            return scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.XH : GameGrade.X;
        }

        switch (rate300)
        {
            case > 0.9 when rate50 < 0.01 && noMiss:
                return scoreInfo.Mods?.IsHiddenMods ?? false ? GameGrade.SH : GameGrade.S;
            case > 0.9:
            case > 0.8 when noMiss:
                return GameGrade.A;
            case > 0.8:
            case > 0.7 when noMiss:
                return GameGrade.B;
            case > 0.6:
                return GameGrade.C;
            default:
                return GameGrade.D;
        }
    }

    public bool IsPerfect(ScoreInfo scoreInfo)
    {
        return scoreInfo is {CountMiss: 0, SliderBreaks: 0};
    }
}