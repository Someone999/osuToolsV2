using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 1.5倍速
    /// </summary>
    public abstract class DoubleTimeMod : Mod, ILegacyMod, IChangeTimeRateMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "DoubleTime";
        /// <inheritdoc />
        public override string ShortName => "DT";

        /// <inheritdoc />
        public override double ScoreMultiplier => 1.12;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "1.5倍速";
        /// <inheritdoc />
        public double TimeRate => 1.5d;
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[] { typeof(NightCoreMod), typeof(HalfTimeMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.DoubleTime;
    }
}