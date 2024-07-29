using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets;

public class EmptyRuleset : Ruleset
{

    public override string Name => "None";

    public override IScoreProcessor CreateScoreProcessor()
    {
        return EmptyScoreProcessor.Instance;
    }

    public override Mod[] AvailableMods => Array.Empty<Mod>();
    public static EmptyRuleset Instance { get; } = new EmptyRuleset();
}