using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class BeatmapObjectWriter<TWriterType> : IBeatmapObjectWriter<IObjectWriter<TWriterType>>
{
    public BeatmapObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        _writer = objectWriter;
        GeneralDataObjectWriter = new GeneralDataObjectWriter<TWriterType>(Writer);
        MetadataObjectWriter = new BeatmapMetadataObjectWriter<TWriterType>(Writer);
        DifficultyObjectWriter = new DifficultyObjectWriter<TWriterType>(Writer);
        EditorDataObjectWriter = new EditorDataObjectWriter<TWriterType>(Writer);
        HitObjectsObjectWriter = new HitObjectsObjectWriter<TWriterType>(Writer);
        TimingPointObjectWriter = new TimingPointsObjectWriter<TWriterType>(Writer);
        EventsObjectWriter = new EventsObjectWriter<TWriterType>(Writer);
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
        if (obj is not Beatmap beatmap)
        {
            throw new InvalidCastException();
        }
        
        Write(beatmap);
    }
    public void Write(Beatmap obj)
    {
        IsWriting = true;
        Writer.Write($"osu file format v{obj.Metadata.BeatmapFileVersion}{Environment.NewLine}");
        GeneralDataObjectWriter.Write(obj);
        EditorDataObjectWriter.Write(obj);
        MetadataObjectWriter.Write(obj.Metadata);
        DifficultyObjectWriter.Write(obj.DifficultyAttributes);
        EventsObjectWriter.Write(obj);
        TimingPointObjectWriter.Write(obj.TimingPointCollection);
        HitObjectsObjectWriter.Write(obj.HitObjects ?? new List<IHitObject>());
        IsWriting = false;
    }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> GeneralDataObjectWriter { get; }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> EditorDataObjectWriter { get; }
    public IObjectWriter<BeatmapMetadata, IObjectWriter<TWriterType>> MetadataObjectWriter { get; }
    public IObjectWriter<DifficultyAttributes, IObjectWriter<TWriterType>> DifficultyObjectWriter { get; }
    public IObjectWriter<Beatmap, IObjectWriter<TWriterType>> EventsObjectWriter { get; }
    public IObjectWriter<List<TimingPoint>, IObjectWriter<TWriterType>> TimingPointObjectWriter { get; }
    public IObjectWriter<List<IHitObject>, IObjectWriter<TWriterType>> HitObjectsObjectWriter { get; }
    public bool NeedClose => Writer.NeedClose;
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