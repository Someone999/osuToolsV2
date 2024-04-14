using osuToolsV2.Game.Mods;

namespace osuToolsV2.Exceptions;

public class ModConflictedException : Exception
{
    public ModConflictedException(Type modType, Mod mod):base($"Mod {modType} conflicted with mod {mod.GetType()} ")
    {
    }
}