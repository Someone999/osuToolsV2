namespace osuToolsV2.Score.ScoreCalculators;

public interface IScoreCalculator
{
    int CalculateScore(ScoreInfo scoreInfo, Judgement judgement);
}