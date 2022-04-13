using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 怎样都失败不了
    /// </summary>
    public abstract class NoFailMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool AllowsFail() => false;
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "NoFail";
        /// <inheritdoc />
        public override string ShortName => "NF";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.5;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "无论如何都不会失败";
        /// <inheritdoc />
        public override Type[] ConflictMods => new[] { typeof(SuddenDeathMod), typeof(PerfectMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.NoFail;
    }
}