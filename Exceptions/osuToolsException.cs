namespace osuToolsV2.Exceptions;

public class osuToolsException : Exception
{
    protected osuToolsException()
    {
    }
    
    public osuToolsException(string msg) : base(msg)
    {
    }
    
    public osuToolsException(string msg, Exception? innerException) : base(msg, innerException)
    {
    }
}