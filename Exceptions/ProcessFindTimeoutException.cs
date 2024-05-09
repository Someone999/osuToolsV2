namespace osuToolsV2.Exceptions;

public class ProcessFindTimeoutException : osuToolsException
{
    protected ProcessFindTimeoutException()
    {
    }

    public ProcessFindTimeoutException(string msg) : base(msg)
    {
    }

    public ProcessFindTimeoutException(string msg, Exception? innerException) : base(msg, innerException)
    {
    }
}