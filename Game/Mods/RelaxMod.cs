

using osuToolsV2.Game.Legacy;

namespace osuToolsV2.Game.Mods
{
    /// <summary>
    /// 自动按键，只需要定位
    /// </summary>
    public abstract class RelaxMod : Mod, ILegacyMod
    {
        /// <inheritdoc />
        public override bool IsRanked => false;
        /// <inheritdoc />
        public override string Name => "Relax";
        /// <inheritdoc />
        public override string ShortName => "Relax";
        /// <inheritdoc />
        public override double ScoreMultiplier => 0.0;
        /// <inheritdoc />
        public override ModType ModType => ModType.Automation;
        /// <inheritdoc />
        public override string Description => "自动按键，只需要定位";
        /// <inheritdoc />
        public override Type[] ConflictMods => new Type[]
        {
            typeof(AutoPilotMod), typeof(AutoPlayMod), typeof(CinemaMod), typeof(SuddenDeathMod),
            typeof(PerfectMod), typeof(NoFailMod)
        };

        public LegacyGameMod LegacyMod => LegacyGameMod.Relax;

        /// <inheritdoc />
        public override bool AllowsFail() => false;
    }
}