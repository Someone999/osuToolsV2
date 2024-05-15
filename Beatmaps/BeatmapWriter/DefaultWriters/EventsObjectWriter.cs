using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class EventsObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public EventsObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        _writer = objectWriter;
    }
    
    public IObjectWriter<TWriterType> Writer{
        get => _writer;
        set
        {
            if (IsWriting)
            {
                return;
            }

            _writer = value;
        }
    }
    
    public bool IsWriting { get; private set; }
    public void Write(object obj)
    {
        if (obj is not Beatmap beatmap)
        {
            throw new InvalidCastException();
        }
        
        Write(beatmap);
    }
    public void Write(Beatmap obj)
    {
        IsWriting = true;
        Writer.Write($"[Events]{Environment.NewLine}");
        if (obj.BreakTimes.Count > 0)
        {
            foreach (var breakTime in obj.BreakTimes)
            {
                Writer.Write(breakTime.ToFileFormat() + Environment.NewLine);
            }
        }

        if (obj.Metadata.BackgroundInfo is { HasBackground: true })
        {
            var bgInfo = obj.Metadata.BackgroundInfo;
            Writer.Write($"0,0,\"{bgInfo.FileName}\",{bgInfo.X},{bgInfo.Y}{Environment.NewLine}");
        }
        if (obj.Metadata.VideoInfo is { HasVideo: true })
        {
            var viInfo = obj.Metadata.VideoInfo;
            Writer.Write($"1,0,\"{viInfo.FileName},{viInfo.X},{viInfo.Y}\"{Environment.NewLine}");
        }

        if (obj.InlineStoryBoardCommand == null)
        {
            return;
        }
        
        foreach (var command in obj.InlineStoryBoardCommand)
        {
            Writer.Write(command.ToFileContent() + Environment.NewLine);
        }

        IsWriting = false;
    }
    
    public bool NeedClose => Writer.NeedClose;
    public void Close()
    {
        if (NeedClose)
        {
            Writer.Close();
        }
    }
    
    private bool _disposed;
    private IObjectWriter<TWriterType> _writer;

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        Writer.Dispose();
        _disposed = true;
    }
}