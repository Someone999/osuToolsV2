using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Osu.Mods;

public class OsuHardRockMod : HardRockMod
{
    public override void Apply(DifficultyAttributes difficulty)
    {
        difficulty.CircleSize *= 1 + 0.3;
        difficulty.ApproachRate *= 1 + 0.4;
        difficulty.OverallDifficulty *= 1 + 0.4;
        difficulty.HpDrain *= 1 + 0.4;
    }
}