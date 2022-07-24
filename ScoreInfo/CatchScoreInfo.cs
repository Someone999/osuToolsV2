using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.ScoreInfo;

public class CatchScoreInfo : IScoreInfo
{

    public int CountGeki { get; set; }
    public int CountKatu { get; set; }
    public int Count300 { get; set; }
    public int Count100 { get; set; }
    public int Count50 { get; set; }
    public int CountMiss { get; set; }
    public int? Score { get; set; }
    public int MaxCombo { get; set; }
    private bool? _perfect;

    public bool Perfect
    {
        get => _perfect ??= CountKatu == 0 && CountMiss == 0;
        set => _perfect = value;
    }
    public int Combo { get; set; }
    public IBeatmap? Beatmap { get; set; }
    public ModList? Mods { get; set; }
    public double GetAccuracy()
    {
        int all = CountKatu + Count300 + Count100 + Count50 + CountMiss;
        double ratio = Count300 + Count100 + Count50;
        return ratio / all;
    }
    public double GetCountGekiRate()
    {
        return 0;
    }
    public double GetCount300Rate()
    {
        return GetAccuracy();
    }
    public int GetHitObjectCount()
    {
        var hitObjs = Beatmap?.HitObjects;
        if (hitObjs == null)
        {
            return 0;
        }

        int total = hitObjs.Count;
        int juiceStreamCount = (from i in hitObjs where i.HitObjectType == HitObjectType.JuiceStream select i).Count();
        int bananaCount = (from i in hitObjs where i.HitObjectType == HitObjectType.Banana select i).Count();
        return total + juiceStreamCount - bananaCount;
    }
    public void Clear()
    {
        CountGeki = 0;
        Count300 = 0;
        CountKatu = 0;
        Count100 = 0;
        Count50 = 0;
        CountMiss = 0;
        Combo = 0;
        MaxCombo = 0;
        if (Score != null)
        {
            Score = 0;
        }
    }
}