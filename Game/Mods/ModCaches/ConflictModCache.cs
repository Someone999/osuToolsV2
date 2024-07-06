namespace osuToolsV2.Game.Mods.ModCaches;

public class ConflictModCache
{
    private Dictionary<Mod, HashSet<Type>> _cachedConflictMods = new Dictionary<Mod, HashSet<Type>>();

    public void CacheConflictMods(Mod mod)
    {
        if (_cachedConflictMods.ContainsKey(mod))
        {
            return;
        }

        var types = mod.ConflictMods;
        _cachedConflictMods.Add(mod, new HashSet<Type>(types));
    }

    public bool IsConflict(Mod mod, Type modType)
    {
        CacheConflictMods(mod);
        var modSet = _cachedConflictMods[mod];
        var isEmpty = modSet.Count == 0;
        var isConflict = !isEmpty && modSet.Any(t => t.IsAssignableFrom(modType));
        return isConflict;
    }
}