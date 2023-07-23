using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    public class ManiaHardRockMod : HardRockMod
    {
        public override bool IsRanked => false;
        public override double ScoreMultiplier => 1.0;
    }
}
