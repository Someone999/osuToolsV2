using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;

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
    bool Perfect { get; set; }
    int Combo { get; set; }
    IBeatmap? Beatmap { get; set; }
    ModList? Mods { get; set; }
    double GetAccuracy();
    double GetCountGekiRate();
    double GetCount300Rate();
    int GetHitObjectCount();
    void Clear();
}