using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 所有难度参数都提高一点，std模式会上下翻转谱面
    /// </summary>
    public abstract class HardRockMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "HardRock";
        /// <inheritdoc />
        public override string ShortName => "HR";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.06;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "所有难度参数都提高一点";
        /// <inheritdoc />
        public override Type[] ConflictMods => new[] { typeof(EasyMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.HardRock;
    }
}