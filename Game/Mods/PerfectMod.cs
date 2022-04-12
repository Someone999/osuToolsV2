using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 要不100%要不重试
    /// </summary>
    public abstract class PerfectMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "Perfect";
        /// <inheritdoc />
        public override string ShortName => "PF";
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <inheritdoc />
        public override string Description => "感受痛苦吧";
        /// <inheritdoc />
        public override Type[] ConflictMods =>new Type[] {typeof(SuddenDeathMod),typeof(NoFailMod) };

        public LegacyGameMod LegacyMod => LegacyGameMod.Perfect;
    }
}