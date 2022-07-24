using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class TimingPointsObjectWriter<TWriterType> : IObjectWriter<List<TimingPoint>, IObjectWriter<TWriterType>>
{

    public TimingPointsObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    public IObjectWriter<TWriterType> ObjectWriter { get; }
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
        ObjectWriter.Write($"[TimingPoints]{Environment.NewLine}");
        foreach (var timingPoint in obj)
        {
            ObjectWriter.Write(timingPoint.ToFileFormat() + Environment.NewLine);
        }
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
}