using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Game.Mods;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Rulesets;

public class EmptyRuleset : Ruleset
{

    public override string Name => "None";
    public override IScoreInfo CreateScoreInfo()
    {
        throw new NotSupportedException();
    }
    public override IHitObject CreateHitObject(IBeatmap beatmap, string[] data)
    {
        throw new NotSupportedException();
    }
    
    public override Mod[] AvailableMods => new Mod[0];
}