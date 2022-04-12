using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 所有难度参数都降低一点
    /// </summary>
    public abstract class EasyMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "Easy";
        /// <inheritdoc />
        public override string ShortName => "EZ";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.5d;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "所有的难度参数都降低一点，并有3次满血复活的机会";
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[] { typeof(HardRockMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.Easy;
    }
}