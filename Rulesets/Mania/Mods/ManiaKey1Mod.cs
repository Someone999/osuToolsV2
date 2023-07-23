using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到1k
    /// </summary>
    public class ManiaKey1Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key1";
        /// <inheritdoc />
        public override string ShortName => "Key1";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key1;

    }
}