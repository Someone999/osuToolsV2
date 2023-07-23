using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 渐隐
    /// </summary>
    public abstract class HiddenMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "Hidden";
        /// <inheritdoc />
        public override string ShortName => "HD";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.06;

        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "渐隐";
        /// <inheritdoc />
        public override Type[] ConflictMods => new[] { typeof(FadeInMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.Hidden;
    }
}