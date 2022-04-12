using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 掉一个就死的Mod
    /// </summary>
    public abstract class SuddenDeathMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "SuddenDeath";
        /// <inheritdoc />
        public override string Description => "掉一个即死";
        /// <inheritdoc />
        public override string ShortName => "SD";
        /// <inheritdoc />
        public override ModType ModType => ModType.DifficultyIncrease;
        /// <summary>
        ///     与这个Mod相冲突的Mod
        /// </summary>
        public override Type[] ConflictMods => new Type[] { typeof(PerfectMod), typeof(NoFailMod) };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.SuddenDeath;

        
    }
}