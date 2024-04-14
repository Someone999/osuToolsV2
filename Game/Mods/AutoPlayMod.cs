using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 自动
    /// </summary>
    public abstract class AutoPlayMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => false;

        /// <inheritdoc />
        public override string Name => "AutoPlay";

        /// <inheritdoc />
        public override string ShortName => "Auto";

        /// <inheritdoc />
        public override double ScoreMultiplier => 1.0d;
        /// <inheritdoc />
        public override ModType ModType => ModType.Automation;
        /// <inheritdoc />
        public override string Description => "全自动游玩";
        /// <inheritdoc />

        public override Type[] ConflictMods => new[]
        {
            typeof(RelaxMod), typeof(AutoPilotMod), typeof(SpunOutMod), typeof(CinemaMod), typeof(SuddenDeathMod),
            typeof(PerfectMod)
        };
        /// <inheritdoc />

        public LegacyGameMod LegacyMod => LegacyGameMod.AutoPlay;
    }
}