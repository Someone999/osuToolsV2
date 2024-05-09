using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;

namespace osuToolsV2.Rulesets.Osu.Mods;

public class OsuTargetPracticeMod : Mod, ILegacyMod
{
    public override string Name => "TargetPractice";
    public override string Description => "";
    public override string ShortName => "TP";
    public LegacyGameMod LegacyMod => LegacyGameMod.Target;
}