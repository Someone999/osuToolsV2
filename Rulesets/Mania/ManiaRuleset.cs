using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Mania.Mods;
using osuToolsV2.Rulesets.Mania.ScoreProcessor;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Rulesets.Mania;

public class ManiaRuleset : Ruleset
{
    private static readonly ManiaScoreProcessor ManiaScoreProcessor = new ManiaScoreProcessor();
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
    public override IScoreProcessor CreateScoreProcessor()
    {
        return ManiaScoreProcessor;
    }

}