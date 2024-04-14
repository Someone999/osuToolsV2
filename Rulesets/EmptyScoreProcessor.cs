using osuToolsV2.Game;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets;

public class EmptyScoreProcessor : IScoreProcessor
{
    public double GetAccuracy(ScoreInfo scoreInfo) => 0;
    public int GetPassedHitObjectCount(ScoreInfo scoreInfo) => 0;
    public int GetHitObjectCount(ScoreInfo scoreInfo) => 0;
    public double GetCount300Rate(ScoreInfo scoreInfo) => 0;
    public double GetCountGekiRate(ScoreInfo scoreInfo) => 0;
    public GameGrade GetGrade(ScoreInfo scoreInfo) => GameGrade.D;
    public bool IsPerfect(ScoreInfo scoreInfo) => false;
    

    public static EmptyScoreProcessor Instance { get; } = new();
}