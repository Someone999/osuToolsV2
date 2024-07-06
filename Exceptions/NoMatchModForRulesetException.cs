using osuToolsV2.Game.Legacy;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Exceptions;

public class NoMatchModForRulesetException : osuToolsException
{
    public NoMatchModForRulesetException(Ruleset ruleset, LegacyGameMod mod) 
        : base($"No mod {mod} for ruleset {ruleset.Name}")

    {
    }
}