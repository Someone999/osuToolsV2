using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 只有很小的视野，且视野在50，100连击时会再缩小一次
    /// </summary>
    public abstract class FlashlightMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "Flashlight";
        /// <inheritdoc />
        public override string ShortName => "FL";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.12;

        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />

        public override string Description => "极限视野";
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.Flashlight;
    }
}