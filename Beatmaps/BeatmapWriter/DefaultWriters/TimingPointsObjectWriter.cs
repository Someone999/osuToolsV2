using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class TimingPointsObjectWriter<TWriterType> : IObjectWriter<List<TimingPoint>, IObjectWriter<TWriterType>>
{

    public TimingPointsObjectWriter(IObjectWriter<TWriterType> objectWriter)
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
        if (obj is not List<TimingPoint> timingPoints)
        {
            throw new InvalidCastException();
        }
        Write(timingPoints);
    }
    public void Write(List<TimingPoint> obj)
    {
        IsWriting = true;
        Writer.Write($"[TimingPoints]{Environment.NewLine}");
        foreach (var timingPoint in obj)
        {
            Writer.Write(timingPoint.ToFileFormat() + Environment.NewLine);
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