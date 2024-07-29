using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Score.ScoreCalculators;

public class ManiaJudgement : Judgement
{
    private const int MaxScore = 1000000;
    private readonly ManiaJudgement? _lastJudgement;
    public int? TotalHits { get; set; }

    public ManiaJudgement(ScoreInfo scoreInfo, HitResult hitResult, ManiaJudgement? lastJudgement) : 
        base(scoreInfo, hitResult)
    {
        _lastJudgement = lastJudgement;
    }

    double GetHitBonusValue(HitResult hitResult) => hitResult switch
    {
        HitResult.HitGeki => 32,
        HitResult.Hit300 => 32,
        HitResult.HitKatu => 16,
        HitResult.Hit100 => 8,
        HitResult.Hit50 => 4,
        HitResult.HitMiss => 0,
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

    double GetScaledScore()
    {
        double modMul = GetModMultiplier();
        int totalNotes = TotalHits ?? -1;

        if (totalNotes != -1)
        {
            return MaxScore * modMul * 0.5 / totalNotes;
        }
        
        if (ScoreInfo.Beatmap != null)
        {
            var scoreProcessor = ScoreInfo.Beatmap.Ruleset.CreateScoreProcessor();
            totalNotes = scoreProcessor.GetHitObjectCount(ScoreInfo);
        }
        else
        {
            totalNotes = ScoreInfo.CountGeki + ScoreInfo.CountKatu + ScoreInfo.Count300 + ScoreInfo.Count100 +
                         ScoreInfo.Count50 + ScoreInfo.CountMiss;
        }

        return MaxScore * modMul * 0.5 / totalNotes;
    }
    
    double GetBonus()
    {
        var lastBonus = _lastJudgement?.GetBonus() ?? 100;
        var scaledPunishment = GetPunishment(HitResult) / GetModDivider();
        var bonus = Math.Max(0, lastBonus - scaledPunishment + GetHitBonus(HitResult));
        return Math.Min(bonus, 100);
    }
    
    double GetBaseScore()
    {
        return GetScaledScore() * (GetHitValue(HitResult) / 320);
    }

    double GetBonusScore()
    {
        return GetScaledScore() * (GetHitBonusValue(HitResult) * Math.Sqrt(GetBonus()) / 320d);
    }

    double GetModDivider()
    {
        var mul = 1d;
        if (ScoreInfo.Mods == null)
        {
            return 1;
        }
        
        if (ScoreInfo.Mods.Any(m => m is HardRockMod))
        {
            mul /= 1.08;
        }

        if (ScoreInfo.Mods.Any(m => m is DoubleTimeMod))
        {
            mul /= 1.1;
        }

        if (ScoreInfo.Mods.Any(m => m is NightCoreMod))
        {
            mul /= 1.1;
        }

        if (ScoreInfo.Mods.Any(m => m is FadeInMod))
        {
            mul /= 1.06;
        }

        if (ScoreInfo.Mods.Any(m => m is HiddenMod))
        {
            mul /= 1.06;
        }

        if (ScoreInfo.Mods.Any(m => m is FlashlightMod))
        {
            mul /= 1.06;
        }

        return mul;
    }

    double GetModMultiplier()
    {
        if (ScoreInfo.Mods == null)
        {
            return 1;
        }

        var mul = 1.0d;
        if (ScoreInfo.Mods.Any(m => m is EasyMod))
        {
            mul *= 0.5;
        }
        
        if (ScoreInfo.Mods.Any(m => m is NoFailMod))
        {
            mul *= 0.5;
        }
        
        if (ScoreInfo.Mods.Any(m => m is HalfTimeMod))
        {
            mul *= 0.5;
        }

        return mul;
    }



    public double CalculateScore() => GetBaseScore() + GetBonusScore();
}