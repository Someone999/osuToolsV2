using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Catch;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Catch.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Rulesets.Catch;

public class CatchRuleset : Ruleset
{
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
    public override IScoreInfo CreateScoreInfo() => new CatchScoreInfo();
    public override IHitObject CreateHitObject(IBeatmap beatmap, string[] data)
    {
        OriginalHitObjectType type = (OriginalHitObjectType)int.Parse(data[3]);
        IHitObject? hitObject = null;
        if ((type & OriginalHitObjectType.HitCircle) != 0)
        {
            hitObject = new Fruit();
        }
        else if ((type & OriginalHitObjectType.Slider) != 0)
        {
            hitObject = new JuiceStream();
        }
        else if ((type & OriginalHitObjectType.Spinner) != 0)
        {
            hitObject = new Banana();
        }

        if (hitObject == null)
        {
            throw new InvalidOperationException($"Can not process type {type}.");
        }
        hitObject.Parse(data);
        return hitObject;
    }
}