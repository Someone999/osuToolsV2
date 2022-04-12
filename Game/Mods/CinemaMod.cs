using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 没有Note的AutoPlay
    /// </summary>
    public abstract class CinemaMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => false;
        /// <inheritdoc />
        public override string Name => "Cinema";

        /// <inheritdoc />
        public override string ShortName => "CM";

        /// <inheritdoc />
        public override double ScoreMultiplier => 1.0d;

        /// <inheritdoc />
        public override ModType ModType => ModType.Automation;
        /// <inheritdoc />
        public override string Description =>  "看不到Note的全自动游玩";
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[]
        {
            typeof(RelaxMod), typeof(AutoPilotMod), typeof(SpunOutMod), typeof(AutoPlayMod), typeof(SuddenDeathMod),
            typeof(PerfectMod)
        };
        /// <inheritdoc />
        public LegacyGameMod LegacyMod => LegacyGameMod.Cinema;
    }
}