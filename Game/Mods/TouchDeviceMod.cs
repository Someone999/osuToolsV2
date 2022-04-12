using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{

    /// <summary>
    /// 触屏
    /// </summary>
    public abstract class TouchDeviceMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name { get; } = "TouchDevice";
        /// <inheritdoc />
        public override string ShortName { get; } = "TD";
        /// <inheritdoc />
        public override double ScoreMultiplier => 1.0d;
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyReduction;
        /// <inheritdoc />
        public override string Description => "在触屏设备上游玩的时候会自动打开的Mod";
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.TouchDevice;


    }
}