using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 0.75倍速
    /// </summary>
    public abstract class HalfTimeMod : Mod, ILegacyMod, IChangeTimeRateMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "HalfTime";
        /// <inheritdoc />
        public override string ShortName => "HT";

        /// <inheritdoc />
        public override double ScoreMultiplier => 0.3;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "0.75倍速";
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[] { typeof(DoubleTimeMod), typeof(NightCoreMod) };
        /// <inheritdoc />
        public double TimeRate => 0.75d;
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.HalfTime;
    }
}