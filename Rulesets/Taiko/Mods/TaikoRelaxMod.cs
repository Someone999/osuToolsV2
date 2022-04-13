using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Taiko.Mods
{
    public class TaikoRelaxMod : RelaxMod
    {
        public override string Name => "Relax";
        public override string Description => "判定不分红蓝";
        public override string ShortName => "Relax";
        public override Type[] ConflictMods => new[] {typeof(AutoPlayMod), typeof(CinemaMod),typeof(SuddenDeathMod),typeof(PerfectMod)};
        public override bool AllowsFail() => false;
        public override bool IsRanked => false;
        public override double ScoreMultiplier => 0;
        public override ModType ModType => ModType.DifficultyReduction;
    }
}