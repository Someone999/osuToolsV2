using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 加重节奏的DoubleTime
    /// </summary>
    public abstract class NightCoreMod : Mod, ILegacyMod, IChangeTimeRateMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "NightCore";
        /// <inheritdoc />
        public override string ShortName => "NC";
        /// <inheritdoc />
        public override double ScoreMultiplier => _scoreMultiplier;
        private double _scoreMultiplier = 1.12;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "在DoubleTime的基础上加重节奏";
        /// <inheritdoc />
        public double TimeRate => 1.5d;
        /// <inheritdoc />
        public override Type[] ConflictMods => new[] { typeof(DoubleTimeMod), typeof(HalfTimeMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.NightCore;
    }
}