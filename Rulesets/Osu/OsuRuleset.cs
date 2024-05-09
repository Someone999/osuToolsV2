using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.Rulesets.Osu.ScoreProcessor;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Osu;

public class OsuRuleset : Ruleset
{
    private static readonly OsuScoreProcessor OsuScoreProcessor = new OsuScoreProcessor();
    public override string Name => "Osu";
    public override bool IsLegacyRuleset => true;
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Osu;
    public override IScoreProcessor CreateScoreProcessor() => OsuScoreProcessor;
    
    public override Mod[] AvailableMods =>
        new Mod[]
        {
            new OsuEasyMod(),new OsuNoFailMod(),new OsuHalfTimeMod(),
            new OsuHardRockMod(),new OsuSuddenDeathMod(),new OsuPerfectMod(),
            new OsuDoubleTimeMod(),new OsuNightCoreMod(),new OsuHiddenMod(),
            new OsuFlashlightMod(),new OsuRelaxMod(),new OsuAutoPilotMod(),
            new OsuSpunOutMod(),new OsuAutoPlayMod(),new OsuCinemaMod(),
            new OsuScoreV2Mod(), new OsuTargetPracticeMod()
        };

}