using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 新版的计分方式
    /// </summary>
    public abstract class ScoreV2Mod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => false;
        /// <inheritdoc />
        public override string Name => "ScoreV2";
        /// <inheritdoc />
        public override string ShortName => "V2";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1;
        /// <inheritdoc />
        public override ModType ModType => ModType.Fun;
        /// <inheritdoc />
        public override string Description => "新版的计分方式";
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.ScoreV2;
    }
}