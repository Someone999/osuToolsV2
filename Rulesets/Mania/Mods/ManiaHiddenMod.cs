using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    public class ManiaHiddenMod: HiddenMod
    {
        public override double ScoreMultiplier => 1.0;
        public override Type[] ConflictMods => new Type[] { typeof(ManiaFadeInMod), typeof(ManiaFlashlightMod) };
    }
}
