using osuToolsV2.Beatmaps;

namespace osuToolsV2.ScoreInfo;

public interface IScoreInfo
{
    int CountGeki { get; set; }
    int CountKatu { get; set; }
    int Count300 { get; set; }
    int Count100 { get; set; }
    int Count50 { get; set; }
    int CountMiss { get; set; }
    int? Score { get; set; }
    int MaxCombo { get; set; }
    IBeatmap? Beatmap { get; set; }
    double GetAccuracy();
    double GetCountGekiRate();
    double GetCount300Rate();
    int GetHitObjectCount();
}