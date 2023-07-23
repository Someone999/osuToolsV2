using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 适用于Mania模式，随机排布所有Note
    /// </summary>
    public abstract class RandomMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => false;
        /// <inheritdoc />
        public override string Name => "Random";
        /// <inheritdoc />
        public override string ShortName => "RD";
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "随机排列Mania Note";
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.Random;
    }
}