namespace osuToolsV2.Score.ScoreCalculators;

public interface IScoreCalculator
{
    double CalculateScore(ScoreInfo scoreInfo);
}