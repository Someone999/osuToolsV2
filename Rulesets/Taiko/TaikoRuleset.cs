using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Taiko;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Taiko.Mods;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Rulesets.Taiko;

public class TaikoRuleset : Ruleset
{

    public override string Name => "Taiko";
    public override bool IsLegacyRuleset => true;
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Taiko;
    public override IScoreInfo CreateScoreInfo() => new TaikoScoreInfo();
    public override IHitObject CreateHitObject(IBeatmap beatmap, string[] data)
    {
        return GenericTaikoHitParser.Parse(data);
    }
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