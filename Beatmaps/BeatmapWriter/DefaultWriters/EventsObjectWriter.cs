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
        if (obj.BreakTimes != null)
        {
            foreach (var breakTime in obj.BreakTimes)
            {
                Writer.Write(breakTime.ToFileFormat() + Environment.NewLine);
            }
        }

        var bgHolder = obj.Metadata.BackgroundHolder;
        if (bgHolder.IsInitialized() && bgHolder.Value != null)
        {
            var bgInfo = bgHolder.Value;
            Writer.Write($"0,0,\"{bgInfo.FileName}\",{bgInfo.Position.X},{bgInfo.Position.Y}{Environment.NewLine}");
        }
        var viHolder = obj.Metadata.VideoHolder;
        if (viHolder.IsInitialized() && viHolder.Value != null)
        {
            var viInfo = viHolder.Value;
            Writer.Write($"1,0,\"{viInfo.FileName}\"{Environment.NewLine}");
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