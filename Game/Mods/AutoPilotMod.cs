using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 自动定位
    /// </summary>
    public abstract class AutoPilotMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool AllowsFail() => false;
        /// <inheritdoc />
        public override bool IsRanked => false;
        /// <inheritdoc />
        public override string Name => "AutoPilot";
        /// <inheritdoc />
        public override string ShortName => "AP";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0d;
        /// <inheritdoc />
        public override string Description => "光标会自动移动，只需要按键";
        /// <inheritdoc />
        public override ModType ModType => ModType.Automation;
        public override Type[] ConflictMods => new Type[]
        {
            typeof(RelaxMod), typeof(SpunOutMod), typeof(AutoPlayMod), typeof(CinemaMod), typeof(SuddenDeathMod),
            typeof(PerfectMod), typeof(NoFailMod)
        };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.AutoPilot;


    }
}