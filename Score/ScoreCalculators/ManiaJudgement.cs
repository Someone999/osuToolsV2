using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Score.ScoreCalculators;

public class ManiaJudgement : Judgement
{

    public ManiaJudgement(ScoreInfo scoreInfo, HitResult hitResult) : 
        base(scoreInfo, hitResult)
    {
    }
}