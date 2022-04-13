using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    public class ManiaFlashlightMod: FlashlightMod
    {
        public override double ScoreMultiplier => 1.0;
        public override Type[] ConflictMods => new[] { typeof(ManiaFadeInMod), typeof(ManiaHiddenMod) };
    }
}
