using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Score.ScoreCalculators;

public class ManiaScoreCalculator : IScoreCalculator
{
    public int? TotalHits { get; set; }
    public const int MaxScore = 1000000;
    
    double GetHitBonusValue(HitResult hitResult) => hitResult switch
    {
        HitResult.HitGeki => 32,
        HitResult.Hit300 => 32,
        HitResult.HitKatu => 16,
        HitResult.Hit100 => 8,
        HitResult.Hit50 => 4,
        HitResult.HitMiss => 0,
        HitResult.None => 0,
        _ => throw new ArgumentOutOfRangeException(nameof(hitResult), hitResult, null)
    };
    
    double GetHitBonus(HitResult hitResult) => hitResult switch
    {
        HitResult.HitGeki => 2,
        HitResult.Hit300 => 1,
        _ => 0
    };
    
    double GetHitValue(HitResult hitResult) => hitResult switch
    {
        HitResult.HitGeki => 320,
        HitResult.Hit300 => 300,
        HitResult.HitKatu => 200,
        HitResult.Hit100 => 100,
        HitResult.Hit50 => 50,
        HitResult.HitMiss => 0,
        HitResult.None => 0,
        _ => throw new ArgumentOutOfRangeException(nameof(hitResult), hitResult, null)
    };
    
    double GetPunishment(HitResult hitResult) => hitResult switch
    {
        HitResult.HitGeki => 0,
        HitResult.Hit300 => 0,
        HitResult.HitKatu => 8,
        HitResult.Hit100 => 24,
        HitResult.Hit50 => 44,
        HitResult.HitMiss => 0,
        _ => throw new ArgumentOutOfRangeException(nameof(hitResult), hitResult, null)
    };

    double GetScaledScore(ScoreInfo scoreInfo)
    {
        double modMul = GetModMultiplier(scoreInfo);
        int totalNotes = TotalHits ?? -1;

        if (totalNotes != -1)
        {
            return MaxScore * modMul * 0.5 / totalNotes;
        }
        
        if (scoreInfo.Beatmap != null)
        {
            var scoreProcessor = scoreInfo.Beatmap.Ruleset.CreateScoreProcessor();
            totalNotes = scoreProcessor.GetHitObjectCount(scoreInfo);
        }
        else
        {
            totalNotes = scoreInfo.CountGeki + scoreInfo.CountKatu + scoreInfo.Count300 + scoreInfo.Count100 +
                         scoreInfo.Count50 + scoreInfo.CountMiss;
        }

        return MaxScore * modMul * 0.5 / totalNotes;
    }

    
    double GetBonus(ManiaJudgement currentJudgement, ScoreInfo scoreInfo, double lastBonus)
    {
        if (currentJudgement.HitResult == HitResult.HitMiss)
        {
            return 0;
        }
        
        var scaledPunishment = GetPunishment(currentJudgement.HitResult) / GetModDivider(scoreInfo);
        var bonus = Math.Max(0, lastBonus - scaledPunishment + GetHitBonus(currentJudgement.HitResult));
        return Math.Min(bonus, 100);
    }
    
    double GetBaseScore(ScoreInfo scoreInfo, HitResult hitResult)
    {
        return GetScaledScore(scoreInfo) * (GetHitValue(hitResult) / 320);
    }

    double GetBonusScore(ScoreInfo scoreInfo, ManiaJudgement judgement, double lastBonus)
    {
        return GetScaledScore(scoreInfo) * 
               (GetHitBonusValue(judgement.HitResult) * Math.Sqrt(GetBonus(judgement, scoreInfo, lastBonus)) / 320d);
    }

    double GetModDivider(ScoreInfo scoreInfo)
    {
        var mul = 1d;
        if (scoreInfo.Mods == null)
        {
            return 1;
        }
        
        if (scoreInfo.Mods.Any(m => m is HardRockMod))
        {
            mul /= 1.08;
        }

        if (scoreInfo.Mods.Any(m => m is DoubleTimeMod))
        {
            mul /= 1.1;
        }

        if (scoreInfo.Mods.Any(m => m is NightCoreMod))
        {
            mul /= 1.1;
        }

        if (scoreInfo.Mods.Any(m => m is FadeInMod))
        {
            mul /= 1.06;
        }

        if (scoreInfo.Mods.Any(m => m is HiddenMod))
        {
            mul /= 1.06;
        }

        if (scoreInfo.Mods.Any(m => m is FlashlightMod))
        {
            mul /= 1.06;
        }

        return mul;
    }

    double GetModMultiplier(ScoreInfo scoreInfo)
    {
        if (scoreInfo.Mods == null)
        {
            return 1;
        }

        var mul = 1.0d;
        if (scoreInfo.Mods.Any(m => m is EasyMod))
        {
            mul *= 0.5;
        }
        
        if (scoreInfo.Mods.Any(m => m is NoFailMod))
        {
            mul *= 0.5;
        }
        
        if (scoreInfo.Mods.Any(m => m is HalfTimeMod))
        {
            mul *= 0.5;
        }

        return mul;
    }

    
    public double CalculateScore(ManiaJudgement judgement, ScoreInfo scoreInfo, double lastBonus) 
        => GetBaseScore(scoreInfo, judgement.HitResult) + GetBonusScore(scoreInfo, judgement, lastBonus);
    public List<ManiaJudgement> ManiaJudgements { get; } = new List<ManiaJudgement>();

    public double CalculateScore(ScoreInfo scoreInfo)
    {
        ManiaJudgement? lastJudgement = null;
        double lastBonus = 0;
        double totalScore = 0;
        foreach (var maniaJudgement in ManiaJudgements)
        {
            lastBonus = lastJudgement == null ? 100 : GetBonus(lastJudgement, scoreInfo, lastBonus);
            var currentScore = CalculateScore(maniaJudgement, scoreInfo, lastBonus);
            lastJudgement = maniaJudgement;
            totalScore += currentScore;
        }

        return totalScore;
    }
}