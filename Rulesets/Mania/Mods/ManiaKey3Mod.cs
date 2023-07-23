using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到3k
    /// </summary>
    public class ManiaKey3Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key3";
        /// <inheritdoc />
        public override string ShortName => "Key3";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key3;
    }
}