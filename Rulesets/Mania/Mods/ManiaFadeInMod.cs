using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    public class ManiaFadeInMod: FadeInMod
    {
        public override Type[] ConflictMods => new Type[] { typeof(ManiaHiddenMod), typeof(ManiaFlashlightMod) };
    }
}
