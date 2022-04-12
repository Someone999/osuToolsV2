using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到5k
    /// </summary>
    public class ManiaKey5Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key5";
        /// <inheritdoc />
        public override string ShortName => "Key5";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key5;
    }
}