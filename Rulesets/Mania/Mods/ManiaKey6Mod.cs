

using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换std谱面到6k
    /// </summary>
    public class ManiaKey6Mod : ManiaKeyMod
    {
        /// <inheritdoc />
        public override string Name => "Key6";
        /// <inheritdoc />
        public override string ShortName => "Key6";
        /// <inheritdoc />
        public override LegacyGameMod LegacyMod => LegacyGameMod.Key6;
    }
}