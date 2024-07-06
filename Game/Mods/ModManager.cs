using System.Collections;
using osuToolsV2.Exceptions;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods.ModCaches;
using osuToolsV2.Game.Mods.ModPropertyEvaluators;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Utils;

namespace osuToolsV2.Game.Mods;

public class ModManager : IEnumerable<Mod>
{
    private readonly ModCache _modCache = new();
    private List<Mod> _mods = new();
    private readonly ModPropertyEvaluator _modPropertyEvaluator = new();

   
    public double ScoreMultiplier { get; private set; } = 1.0;
    public double TimeRate { get; private set; } = 1.0;
    public bool IsRanked { get; private set; } = true;
    public bool AllowsFail { get; private set; } = true;
    public bool IsHiddenMods { get; private set; }

    
    public string ShortNames => string.Join(",", _mods.Select(m => m.ShortName));
    public string Names => string.Join(",", _mods.Select(m => m.Name));
    private void RecalculateProperties()
    {
        ScoreMultiplier = _modPropertyEvaluator.ScoreMultiplierModPropertyEvaluator.Evaluate(_mods);
        TimeRate = _modPropertyEvaluator.TimeRateModPropertyEvaluator.Evaluate(_mods);
        IsRanked = _modPropertyEvaluator.IsRankedModPropertyEvaluator.Evaluate(_mods);
        AllowsFail = _modPropertyEvaluator.AllowsFailModPropertyEvaluator.Evaluate(_mods);
        IsHiddenMods = _modPropertyEvaluator.IsHiddenModPropertyEvaluator.Evaluate(_mods);
    }

    public void Add(Mod mod, bool throwWhenError)
    {
        if (_mods.Any(m => m.GetType() == mod.GetType()))
        {
            return;
        }
        
        List<Mod> removeMods = new List<Mod>();
        foreach (var m in _mods)
        {
            bool isConflict = _modCache.ConflictModCache.IsConflict(mod, m.GetType());

            if (!isConflict)
            {
                continue;
            }

            if (throwWhenError)
            {
                throw new ModConflictedException(mod.GetType(), mod);
            }

            removeMods.Add(mod);
        }

        if (removeMods.Count > 0)
        {
            foreach (var removeMod in removeMods)
            {
                _mods.Remove(removeMod);
            }
        }

        _mods.Add(mod);
        RecalculateProperties();
    }
    
    public void Add<TMod>(bool throwWhenError = false) where TMod : Mod, new()
    {
        Add(new TMod(), throwWhenError);
    }
    
    public void AddLegacyMod(LegacyGameMod legacyGameMod, Ruleset ruleset, bool throwWhenError)
    {
        var cachedMod = _modCache.RulesetModCache.GetCachedMod(ruleset, legacyGameMod);
        if (cachedMod != null)
        {
            Add(cachedMod, throwWhenError);
        }

        var matchedMod =
            ruleset.AvailableMods.FirstOrDefault(m =>
                m is ILegacyMod legacyMod && legacyMod.LegacyMod == legacyGameMod);

        if (matchedMod != null)
        {
            _modCache.RulesetModCache.CacheRulesetMod(ruleset, legacyGameMod, matchedMod);
            Add(matchedMod, throwWhenError);
            return;
        }

        if (!throwWhenError)
        {
            return;
        }

        throw new NoMatchModForRulesetException(ruleset, legacyGameMod);
    }
    
    
   
    
    public void Remove<TMod>() where TMod : Mod
    {
        _mods.RemoveAll(m => m is TMod);
        RecalculateProperties();
    }
    
    public void Remove(Mod mod)
    {
        _mods.Remove(mod);
        RecalculateProperties();
    }

    public void Clear()
    {
        _mods.Clear();
        RecalculateProperties();
    }

    public Mod[] ToArray() => _mods.ToArray();
    
    public LegacyGameMod ToLegacyMod()
    {
        LegacyGameMod legacyGameMod = LegacyGameMod.None;
        foreach (var mod in _mods)
        {
            if (mod is ILegacyMod legacyMod)
            {
                legacyGameMod |= legacyMod.LegacyMod;
            }
        }

        return legacyGameMod;
    }
    
    public IEnumerator<Mod> GetEnumerator() => _mods.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    
    public static ModManager FromModEnumerable(IEnumerable<Mod> modArr)
    {
        ModManager manager = new ModManager
        {
            _mods = new List<Mod>(modArr)
        };
        
        manager.RecalculateProperties();
        return manager;
    }
    
    public static ModManager FromLegacyMods(LegacyGameMod legacyMods, Ruleset? ruleset, bool throwWhenError = true) =>
        FromInteger((int)legacyMods, ruleset, throwWhenError);

    public static ModManager FromInteger(int mods, Ruleset? ruleset, bool throwWhenError)
    {
        ruleset ??= Ruleset.FromLegacyRuleset(LegacyRuleset.Osu);
        ModManager manager = new ModManager();
        var legacyMods = EnumUtil.IntToEnum<LegacyGameMod>(mods);
        foreach (var legacyMod in legacyMods)
        {
            manager.AddLegacyMod(legacyMod, ruleset, throwWhenError);
        }
            
        return manager;
    }
}
