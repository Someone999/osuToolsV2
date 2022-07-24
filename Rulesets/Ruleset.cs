
using System.Reflection;
using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Logger;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.ScoreInfo;
using osuToolsV2.Game.Mods;
using System;

namespace osuToolsV2.Rulesets;

public abstract class Ruleset
{
    
    private static Dictionary<string, Ruleset>? _rulesets;
    public static readonly object StaticLock = new object();
    private static void InitRulesets()
    {
        lock (StaticLock)
        {
            _rulesets = new Dictionary<string, Ruleset>();
            Assembly asm = typeof(Ruleset).Assembly;
            Type[] types = asm.GetTypes();
            foreach (var type in types)
            {
                if (type.BaseType != typeof(Ruleset))
                {
                    continue;
                }
                try
                {
                    Ruleset? ruleset = (Ruleset?)Activator.CreateInstance(type);
                    if (ruleset == null)
                    {
                        continue;
                    }
                    _rulesets.Add(ruleset.Name, ruleset);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error when creating ruleset. Please read log file to get more information.");
                    FileLogger.GlobalFileLogger.LogException("Ruleset::InitRulesets",
                        $"Error occured when creating ruleset {type.FullName} {e.Message}");
                }
            }
        }
    }

    public static Ruleset FromRulesetName(string name)
    {
        if (_rulesets != null)
            return _rulesets[name];
        _rulesets = new Dictionary<string, Ruleset>();
        InitRulesets();
        return _rulesets[name];
    }
    public static Ruleset FromLegacyRuleset(LegacyRuleset ruleset) => FromRulesetName(ruleset.ToString());
    
    public abstract string Name { get; }
    public abstract Mod[] AvailableMods {get;}
    public virtual LegacyRuleset? LegacyRuleset => null;
    public virtual bool IsLegacyRuleset => false;
    public static bool operator == (Ruleset? a, Ruleset? b)
    {
        if (a is null && b is null)
        {
            return true;
        }
        if (a is null || b is null)
        {
            return false;
        }
        if (a.IsLegacyRuleset && b.IsLegacyRuleset)
        {
            return a.LegacyRuleset == b.LegacyRuleset;
        }
        if(a.IsLegacyRuleset || b.IsLegacyRuleset)
        {
            return false;
        }
        return a.Name == b.Name;
    }
    
    public static bool operator !=(Ruleset a, Ruleset b)
    {
        return !(a == b);
    }

    private bool Equals(Ruleset other)
    {
        return this == other;
    }
    
    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj))
            return false;
        if (ReferenceEquals(this, obj))
            return true;
        return obj.GetType() == GetType() && Equals((Ruleset)obj);
    }
    
    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public abstract IScoreInfo CreateScoreInfo();
    public abstract IHitObject CreateHitObject(IBeatmap beatmap, string[] data);
}