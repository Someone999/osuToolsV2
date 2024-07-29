using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Catch.Mods;
using osuToolsV2.Rulesets.Catch.ScoreProcessor;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Catch;

public class CatchRuleset : Ruleset
{
    private static readonly CatchScoreProcessor CatchScoreProcessor = new CatchScoreProcessor();
    public override string Name => "Catch";
    public override Mod[] AvailableMods => new Mod[]
    {
        new CatchEasyMod(), new CatchNoFailMod(), new CatchHalfTimeMod(),
        new CatchHardRockMod(), new CatchSuddenDeathMod(), new CatchPerfectMod(),
        new CatchDoubleTimeMod(), new CatchNightCoreMod(), new CatchHiddenMod(),
        new CatchFlashlightMod(), new CatchRelaxMod(), new CatchAutoPlayMod(),
        new CatchCinemaMod(), new CatchScoreV2Mod()
    };
    public override bool IsLegacyRuleset => true;
    
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Catch;
    public override IScoreProcessor CreateScoreProcessor()
    {
        return CatchScoreProcessor;
    }


}