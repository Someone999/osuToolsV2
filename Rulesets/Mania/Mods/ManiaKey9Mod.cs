using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到9k
    /// </summary>
    public class ManiaKey9Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key9";
        /// <inheritdoc />
        public override string ShortName => "Key9";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key9;
    }
}