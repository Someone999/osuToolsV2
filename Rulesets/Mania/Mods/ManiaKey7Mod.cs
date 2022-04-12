using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到7k
    /// </summary>
    public class ManiaKey7Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key7";
        /// <inheritdoc />
        public override string ShortName => "Key7";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key7;
    }
}