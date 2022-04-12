using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 上隐
    /// </summary>
    public abstract class FadeInMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "FadeIn";
        /// <inheritdoc />
        public override string ShortName => "FI";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1d;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "上隐";
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[] {typeof(HiddenMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.FadeIn;

    }
}