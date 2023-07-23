using osuToolsV2.Game;

namespace osuToolsV2.Score.ScoreProcessor;

public interface IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo);
    public int GetPassedHitObject(ScoreInfo scoreInfo);
    public int GetHitObjectCount(ScoreInfo scoreInfo);
    public double GetCount300Rate(ScoreInfo scoreInfo);
    public double GetCountGekiRate(ScoreInfo scoreInfo);
    public GameRanking GetRanking(ScoreInfo scoreInfo);
    public bool IsPerfect(ScoreInfo scoreInfo);
}