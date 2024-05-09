namespace osuToolsV2.Exceptions;

public class InvalidScoreDbFileException : Exception
{
    public InvalidScoreDbFileException()
    {
    }

    public InvalidScoreDbFileException(string? message) : base(message)
    {
    }

    public InvalidScoreDbFileException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}