using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class EditorDataObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public EditorDataObjectWriter(IObjectWriter<TWriterType> objectWriter)
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
    void WriteKeyValuePair(string key, object val)
    {
        Writer.Write($"{key}:{val}{Environment.NewLine}");
    }

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
        Writer.Write($"[Editor]{Environment.NewLine}");
        if (obj.Bookmarks.Count != 0)
        {
            Writer.Write("Bookmarks: ");
            Writer.Write(string.Join(",", obj.Bookmarks.Select(i => i.ToString())));
            Writer.Write(Environment.NewLine);
        }

        WriteKeyValuePair("DistanceSpacing", obj.DistanceSpacing);
        WriteKeyValuePair("BeatDivisor", obj.BeatDivisor);
        WriteKeyValuePair("GridSize", obj.GridSize);
        WriteKeyValuePair("TimelineZoom", obj.TimelineZoom);
        Writer.Write(Environment.NewLine);
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