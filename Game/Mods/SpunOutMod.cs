using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 自动转转盘
    /// </summary>
    public abstract class SpunOutMod : Mod, ILegacyMod
    {
        ///<inheritdoc/>
        public override string Description => "可以自动转转盘的Mod";

        /// <inheritdoc />
        public override bool IsRanked => true;
        /// <inheritdoc />
        public override string Name => "SpunOut";
        /// <inheritdoc />
        public override string ShortName => "SP";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.9;
        /// <inheritdoc />
        public override ModType ModType => ModType.Automation;
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[]
        {
            typeof(AutoPilotMod), typeof(AutoPlayMod), typeof(CinemaMod), typeof(SuddenDeathMod),
            typeof(PerfectMod), typeof(NoFailMod)
        };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.SpunOut;
    }
}