using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Taiko.Mods;
using osuToolsV2.Rulesets.Taiko.ScoreProcessor;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Taiko;

public class TaikoRuleset : Ruleset
{
    private static readonly TaikoScoreProcessor TaikoScoreProcessor = new TaikoScoreProcessor();
    public override string Name => "Taiko";
    public override bool IsLegacyRuleset => true;
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Taiko;
    public override IScoreProcessor CreateScoreProcessor() => TaikoScoreProcessor;
    
    public override Mod[] AvailableMods =>
        new Mod[]
        {
            new TaikoEasyMod(),new TaikoNoFailMod(),new TaikoHalfTimeMod(),
            new TaikoHardRockMod(),new TaikoSuddenDeathMod(),new TaikoPerfectMod(),
            new TaikoDoubleTimeMod(),new TaikoNightCoreTimeMod(),new TaikoHiddenMod(),
            new TaikoFlashlightMod(),new TaikoRelaxMod(),new TaikoAutoPlayMod(),
            new TaikoCinemaMod(),new TaikoScoreV2Mod()
        };
    
    
}