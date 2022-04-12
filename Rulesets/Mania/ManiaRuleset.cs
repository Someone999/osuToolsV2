using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Mania;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Mania.Mods;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Rulesets.Mania;

public class ManiaRuleset : Ruleset
{

    public override string Name => "Mania";
    public override Mod[] AvailableMods => new Mod[]
    { 
        new ManiaKeyMod(), new ManiaEasyMod(), new ManiaNoFailMod(),new ManiaHalfTimeMod(),
        new ManiaHardRockMod(), new ManiaSuddenDeathMod(),new ManiaPerfectMod(),new ManiaDoubleTimeMod(),
        new ManiaNightCoreMod(), new ManiaFadeInMod(), new ManiaHiddenMod(), new ManiaFlashlightMod(),
        new ManiaKey1Mod(),new ManiaKey2Mod(),new ManiaKey3Mod(),new ManiaKey4Mod(),new ManiaKey5Mod(),
        new ManiaKey6Mod(),new ManiaKey7Mod(),new ManiaKey8Mod(),new ManiaKey9Mod(),new ManiaKeyCoopMod(),
        new ManiaMirrorMod(),new ManiaRandomMod(),new ManiaAutoPlayMod(),new ManiaCinemaMod(),new ManiaScoreV2Mod()
    };
    public override bool IsLegacyRuleset => true;
    public override LegacyRuleset? LegacyRuleset => Legacy.LegacyRuleset.Mania;
    public override IScoreInfo CreateScoreInfo() => new ManiaScoreInfo();
    public override IHitObject CreateHitObject(IBeatmap beatmap, string[] data)
    {
        OriginalHitObjectType type = (OriginalHitObjectType)int.Parse(data[2]);
        IManiaNote? maniaNote = null;
        switch (type)
        {
            case OriginalHitObjectType.HitCircle:
                maniaNote = new ManiaHit();
                break;
            case OriginalHitObjectType.ManiaHold:
                maniaNote = new ManiaHold();
                break;
        }

        if (maniaNote == null)
        {
            throw new InvalidOperationException($"Can not process type {type}.");
        }
        maniaNote.BeatmapColumn = (int)beatmap.DifficultyAttributes.CircleSize;
        maniaNote.Column = (int) Math.Floor(maniaNote.Position.X * maniaNote.BeatmapColumn / 512d);
        maniaNote.Parse(data);
        return maniaNote;
    }

}