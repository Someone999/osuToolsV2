using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class BeatmapObjectWriter<TWriterType> : IBeatmapObjectWriter<IObjectWriter<TWriterType>>
{
    public BeatmapObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
        GeneralDataObjectWriter = new GeneralDataObjectWriter<TWriterType>(ObjectWriter);
        MetadataObjectWriter = new BeatmapMetadataObjectWriter<TWriterType>(ObjectWriter);
        DifficultyObjectWriter = new DifficultyObjectWriter<TWriterType>(ObjectWriter);
        EditorDataObjectWriter = new EditorDataObjectWriter<TWriterType>(ObjectWriter);
        HitObjectsObjectWriter = new HitObjectsObjectWriter<TWriterType>(ObjectWriter);
        TimingPointObjectWriter = new TimingPointsObjectWriter<TWriterType>(ObjectWriter);
        EventsObjectWriter = new EventsObjectWriter<TWriterType>(ObjectWriter);
    }

    
    public IObjectWriter<TWriterType> ObjectWriter { get; }
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
        ObjectWriter.Write($"osu file format v{obj.Metadata.BeatmapFileVersion}{Environment.NewLine}");
        GeneralDataObjectWriter.Write(obj);
        EditorDataObjectWriter.Write(obj);
        MetadataObjectWriter.Write(obj.Metadata);
        DifficultyObjectWriter.Write(obj.DifficultyAttributes);
        EventsObjectWriter.Write(obj);
        TimingPointObjectWriter.Write(obj.TimingPoints);
        HitObjectsObjectWriter.Write(obj.HitObjects ?? new List<IHitObject>());
        
    }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> GeneralDataObjectWriter { get; }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> EditorDataObjectWriter { get; }
    public IObjectWriter<BeatmapMetadata, IObjectWriter<TWriterType>> MetadataObjectWriter { get; }
    public IObjectWriter<DifficultyAttributes, IObjectWriter<TWriterType>> DifficultyObjectWriter { get; }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> EventsObjectWriter { get; }
    public IObjectWriter<List<TimingPoint>, IObjectWriter<TWriterType>> TimingPointObjectWriter { get; }
    public IObjectWriter<List<IHitObject>, IObjectWriter<TWriterType>> HitObjectsObjectWriter { get; }
    public bool NeedClose => ObjectWriter.NeedClose;
    public void Close()
    {
        if (GeneralDataObjectWriter.NeedClose)
        {
            GeneralDataObjectWriter.Close();
        }
        if (EditorDataObjectWriter.NeedClose)
        {
            EditorDataObjectWriter.Close();
        }
        if (MetadataObjectWriter.NeedClose)
        {
            MetadataObjectWriter.Close();
        }
        if (DifficultyObjectWriter.NeedClose)
        {
            DifficultyObjectWriter.Close();
        }
        if (EventsObjectWriter.NeedClose)
        {
            EventsObjectWriter.Close();
        }
        if (TimingPointObjectWriter.NeedClose)
        {
            TimingPointObjectWriter.Close();
        }
        if (HitObjectsObjectWriter.NeedClose)
        {
            HitObjectsObjectWriter.Close();
        }
        
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
        GeneralDataObjectWriter.Dispose();
        EditorDataObjectWriter.Dispose();
        MetadataObjectWriter.Dispose();
        DifficultyObjectWriter.Dispose();
        EventsObjectWriter.Dispose();
        TimingPointObjectWriter.Dispose();
        HitObjectsObjectWriter.Dispose();
        _disposed = true;
    }
}