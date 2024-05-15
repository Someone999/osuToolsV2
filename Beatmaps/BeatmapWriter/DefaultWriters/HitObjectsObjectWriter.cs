using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class HitObjectsObjectWriter<TWriterType> : IObjectWriter<List<IHitObject>, IObjectWriter<TWriterType>>
{

    public HitObjectsObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        _writer = objectWriter;
    }
    
    public IObjectWriter<TWriterType> Writer {
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
        if (obj is not List<IHitObject> hitObjects)
        {
            throw new InvalidCastException();
        }
        Write(hitObjects);
    }
    public void Write(List<IHitObject> obj)
    {
        IsWriting = true;
        Writer.Write($"[HitObjects]{Environment.NewLine}");
        foreach (var hitObject in obj)
        {
            Writer.Write(hitObject.ToFileFormat() + Environment.NewLine);
        }
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