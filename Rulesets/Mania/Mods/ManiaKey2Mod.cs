using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到2k
    /// </summary>
    public class ManiaKey2Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key2";
        /// <inheritdoc />
        public override string ShortName => "Key2";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key2;
    }
}