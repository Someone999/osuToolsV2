using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class EditorDataObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public EditorDataObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    
    public IObjectWriter<TWriterType> ObjectWriter { get; }
    void WriteKeyValuePair(string key, object val)
    {
        ObjectWriter.Write($"{key}:{val}{Environment.NewLine}");
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
        ObjectWriter.Write($"[Editor]{Environment.NewLine}");
        if (obj.Bookmarks.Count != 0)
        {
            ObjectWriter.Write("Bookmarks: ");
            ObjectWriter.Write(string.Join(",", obj.Bookmarks.Select(i => i.ToString())));
            ObjectWriter.Write(Environment.NewLine);
        }

        WriteKeyValuePair("DistanceSpacing", obj.DistanceSpacing);
        WriteKeyValuePair("BeatDivisor", obj.BeatDivisor);
        WriteKeyValuePair("GridSize", obj.GridSize);
        WriteKeyValuePair("TimelineZoom", obj.TimelineZoom);
        ObjectWriter.Write(Environment.NewLine);
    }
    
    public bool NeedClose => ObjectWriter.NeedClose;
    public void Close()
    {
        if (NeedClose)
        {
            ObjectWriter.Close();
        }
    }
    
    private bool _disposed;
    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }
        
        ObjectWriter.Dispose();
        _disposed = true;
    }
}