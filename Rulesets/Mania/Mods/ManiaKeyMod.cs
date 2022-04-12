using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Mania.Mods
{
    /// <summary>
    /// 转换按键数量的Mod
    /// </summary>
    public class ManiaKeyMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;

        /// <inheritdoc />
        public override string Name => "KeyMod";

        /// <inheritdoc />
        public override string ShortName => "KeyMod";

        /// <inheritdoc />
        public override ModType ModType => ModType.Conversion;

        /// <inheritdoc />
        public override string Description => "将osu!谱转成指定键数的Mania谱";

        /// <inheritdoc />
        public virtual LegacyGameMod LegacyMod => LegacyGameMod.KeyMod;

        public override Type[] ConflictMods => new Type[] { typeof(ManiaKeyMod) };
    }
}