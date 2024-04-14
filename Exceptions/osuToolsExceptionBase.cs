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