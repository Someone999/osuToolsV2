using osuToolsV2.Game.Mods;

namespace osuToolsV2.Exceptions;

public class osuToolsExceptionBase : Exception
{
    protected osuToolsExceptionBase()
    {
    }
    
    public osuToolsExceptionBase(string msg) : base(msg)
    {
    }
    
    public osuToolsExceptionBase(string msg, Exception? innerException) : base(msg, innerException)
    {
    }
}

public class ModConflictedException : Exception
{
    public ModConflictedException(Type modType, Mod mod):base($"Mod {modType} conflicted with mod {mod.GetType()} ")
    {
    }
}