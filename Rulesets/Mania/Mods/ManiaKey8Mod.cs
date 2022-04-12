using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到8k
    /// </summary>
    public class ManiaKey8Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key8";
        /// <inheritdoc />
        public override string ShortName => "Key8";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key8;
    }
}