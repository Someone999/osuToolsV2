namespace osuToolsV2.Exceptions;

public class InvalidBeatmapException : osuToolsExceptionBase
{
    public InvalidBeatmapException()
    {
    }
    public InvalidBeatmapException(string msg, Exception? innerException = null) : base(msg, innerException)
    {
    }
}