using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class HitObjectsObjectWriter<TWriterType> : IObjectWriter<List<IHitObject>, IObjectWriter<TWriterType>>
{

    public HitObjectsObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    public IObjectWriter<TWriterType> ObjectWriter { get; }
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
        ObjectWriter.Write($"[HitObjects]{Environment.NewLine}");
        foreach (var hitObject in obj)
        {
            ObjectWriter.Write(hitObject.ToFileFormat() + Environment.NewLine);
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