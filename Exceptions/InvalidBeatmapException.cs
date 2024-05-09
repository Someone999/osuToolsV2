namespace osuToolsV2.Exceptions;

public class InvalidBeatmapException : osuToolsException
{
    public InvalidBeatmapException()
    {
    }
    public InvalidBeatmapException(string msg, Exception? innerException = null) : base(msg, innerException)
    {
    }
}