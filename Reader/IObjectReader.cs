namespace osuToolsV2.Reader;

public interface IObjectReader<TReader>
{
    object? Read();
    TReader Reader { get; set; }
    bool IsReading { get; }
}

public interface IObjectReader<TReader, out TObject> : IObjectReader<TReader>
{
    new TObject? Read();
}