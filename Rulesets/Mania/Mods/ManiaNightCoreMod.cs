using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    public class ManiaNightCoreMod: NightCoreMod
    {
        public override double ScoreMultiplier => 1.0;
        public override Type[] ConflictMods => new[] { typeof(ManiaDoubleTimeMod), typeof(ManiaHalfTimeMod) };
    }
}
