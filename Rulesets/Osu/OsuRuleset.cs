using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Rulesets.Osu;

public class OsuRuleset : Ruleset
{
    public override string Name => "Osu";
    public override bool IsLegacyRuleset => true;
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Osu;
    public override IScoreInfo CreateScoreInfo() => new OsuScoreInfo();
    public override IHitObject CreateHitObject(IBeatmap beatmap, string[] data)
    {
        OriginalHitObjectType type = (OriginalHitObjectType)int.Parse(data[3]);
        IHitObject? hitObject = null;
        if ((type & OriginalHitObjectType.HitCircle) != 0)
        {
            hitObject = new HitCircle();
        }
        else if ((type & OriginalHitObjectType.Slider) != 0)
        {
            hitObject = new Slider();
        }
        else if ((type & OriginalHitObjectType.Spinner) != 0)
        {
            hitObject = new Spinner();
        }

        if (hitObject == null)
        {
            throw new InvalidOperationException($"Can not process type {type}.");
        }
        hitObject.Parse(data);
        return hitObject;
    }
    public override Mod[] AvailableMods =>
        new Mod[]
        {
            new OsuEasyMod(),new OsuNoFailMod(),new OsuHalfTimeMod(),
            new OsuHardRockMod(),new OsuSuddenDeathMod(),new OsuPerfectMod(),
            new OsuDoubleTimeMod(),new OsuNightCoreMod(),new OsuHiddenMod(),
            new OsuFlashlightMod(),new OsuRelaxMod(),new OsuAutoPilotMod(),
            new OsuSpunOutMod(),new OsuAutoPlayMod(),new OsuCinemaMod(),
            new OsuScoreV2Mod()
        };

}