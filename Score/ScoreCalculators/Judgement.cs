using osuToolsV2.Rulesets;

namespace osuToolsV2.Score.ScoreCalculators;

public class Judgement
{
    protected readonly ScoreInfo ScoreInfo;
    public HitResult HitResult { get; }

    public Judgement(ScoreInfo scoreInfo, HitResult hitResult)
    {
        ScoreInfo = scoreInfo;
        HitResult = hitResult;
    }
}