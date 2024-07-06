using osuToolsV2.Game.Legacy;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Game.Mods.ModCaches;

public class RulesetModCache
{
    private Dictionary<Ruleset, Dictionary<LegacyGameMod, Mod>> _cachedMods = new();

    public void CacheRulesetMod(Ruleset ruleset, LegacyGameMod legacyGameMod, Mod mod)
    {
        if (!_cachedMods.TryGetValue(ruleset, out var candidateMods))
        {
            candidateMods = new Dictionary<LegacyGameMod, Mod>();
            _cachedMods.Add(ruleset, candidateMods);
        }

        candidateMods.TryAdd(legacyGameMod, mod);
    }

    public Mod? GetCachedMod(Ruleset ruleset, LegacyGameMod legacyGameMod)
    {
        return !_cachedMods.TryGetValue(ruleset, out var candidateMods)
            ? null
            : candidateMods.GetValueOrDefault(legacyGameMod);

    }
}