

using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到4k
    /// </summary>
    public class ManiaKey4Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key4";
        /// <inheritdoc />
        public override string ShortName => "Key4";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key4;
    }
}