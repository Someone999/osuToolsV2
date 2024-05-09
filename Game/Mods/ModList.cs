using System.Collections;
using System.Text;
using osuToolsV2.Exceptions;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Game.Mods
{
    public class ModList : IEnumerable<Mod>
    {
        private int _version;
        List<Mod> _mods = new List<Mod>();
        public double ScoreMultiplier { get; private set; } = 1.0;
        public double TimeRate { get; private set; } = 1.0;
        public bool IsRanked { get; private set; } = true;
        public bool AllowsFail { get; private set; } = true;
        public bool IsHiddenMods
        {
            get
            {
                return _mods.Any(m => m is FadeInMod or FlashlightMod or HiddenMod);
            }
        }
        void CalcScoreMul()
        {
            if (_mods.Count == 0)
            {
                ScoreMultiplier = 1;
                return;
            }
            
            double multiplier = 1;
            foreach (var mod in _mods)
            {
                multiplier *= mod.ScoreMultiplier;
            }
            
            ScoreMultiplier = Math.Round(multiplier, 2);
        }

        void CalcTimeRate()
        {
            TimeRate = 1;
            foreach (var m in _mods)
            {
                if (m is IChangeTimeRateMod changeTimeRateMod)
                {
                    TimeRate *= changeTimeRateMod.TimeRate;
                }
            }
        }

        void IsModsRanked() => IsRanked = _mods.All(m => m.IsRanked);
        void GetAllowsFail() => AllowsFail = _mods.Aggregate(true, (current, mod) => current && mod.AllowsFail());
        void RecalculateProperties()
        {
            CalcScoreMul();
            CalcTimeRate();
            GetAllowsFail();
            IsModsRanked();
            Interlocked.Increment(ref _version);
        }

        public void SetForSpecifiedRuleset(Ruleset ruleset)
        {
            var originalMods = _mods;
            _mods = new List<Mod>();
            Dictionary<string, Mod> avaMods =
                ruleset.AvailableMods.ToDictionary(key => key.ShortName, _ => _);
            var matched = 
                from originalMod in originalMods 
                where avaMods.ContainsKey(originalMod.Name) 
                select originalMod;
            
            foreach (var mod in matched)
            {
                Add(mod, false);
            }
        }
        
        public void Add(Mod m, bool throwWhenError)
        {
            foreach (var mod in _mods)
            {
                foreach (var conflictMod in mod.ConflictMods)
                {
                    if (!conflictMod.IsInstanceOfType(m)) 
                        continue;
                    if (throwWhenError)
                    {
                        throw new ModConflictedException(mod.GetType(), m);
                    }
                    return;
                }
            }
            _mods.Add(m);
            RecalculateProperties();
        }

        public void Remove(Mod m)
        {
            _mods.Remove(m);
            RecalculateProperties();
        }

        public void Clear()
        {
            _mods.Clear();
            RecalculateProperties();
        }

        public static ModList FromModArray(Mod[] modarr)
        {
            ModList list = new ModList
            {
                _mods = new List<Mod>(modarr)
            };
            list.RecalculateProperties();
            return list;
        }

        public static ModList FromLegacyMods(LegacyGameMod legacyMods, Ruleset? ruleset, bool throwWhenError = true) =>
            FromInteger((int) legacyMods, ruleset, throwWhenError);
        public static ModList FromInteger(int mods, Ruleset? ruleset = null,bool throwWhenError = true)
        {
            ruleset ??= Ruleset.FromLegacyRuleset(LegacyRuleset.Osu);
            ModList list = new ModList();
            string bits = new string(Convert.ToString(mods, 2).ToArray());
            for (int i = 0; i < bits.Length; i++)
            {
                if (bits[i] != '1')
                {
                    continue;
                }
                LegacyGameMod legacy = (LegacyGameMod) (1 << (bits.Length - 1 - i));

                foreach (var modeAvailableMod in ruleset.AvailableMods)
                {
                    if (modeAvailableMod is not ILegacyMod legacyMod)
                    {
                        continue;
                    }
                    if (legacyMod.LegacyMod == legacy)
                    {
                        list.Add(modeAvailableMod, throwWhenError);
                    }
                }
            }

            return list;
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
        

        private string _cachedShortNames = "", _cachedNames = "";
        private int _shortNameLastVersion = -1, _nameLastVersion = -1;
        public string ShortNames
        {
            get
            {
                if (_shortNameLastVersion == _version)
                {
                    return _cachedShortNames;
                }
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < _mods.Count; i++)
                {
                    builder.Append(_mods[i].ShortName);
                    if (i < _mods.Count - 1)
                    {
                        builder.Append(',');
                    }
                }

                _shortNameLastVersion = _version;
                return _cachedShortNames = builder.ToString();
            }
        }
        
        public string Names
        {
            get
            {
                if (_nameLastVersion == _version)
                {
                    return _cachedNames;
                }
                
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < _mods.Count; i++)
                {
                    builder.Append(_mods[i].Name);
                    if (i < _mods.Count - 1)
                    {
                        builder.Append(',');
                    }
                }

                _nameLastVersion = _version;
                return _cachedNames = builder.ToString();
            }
        }

        public IEnumerator<Mod> GetEnumerator()
        {
            return _mods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
