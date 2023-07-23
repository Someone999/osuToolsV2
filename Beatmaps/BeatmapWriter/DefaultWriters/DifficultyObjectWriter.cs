using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class DifficultyObjectWriter<TWriterType> : 
    IObjectWriter<DifficultyAttributes, IObjectWriter<TWriterType>>
{

    public DifficultyObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    
    public IObjectWriter<TWriterType> ObjectWriter { get; }
    
    void WriteKeyValuePairIfNotNull(string? key, object? val)
    {
        if (key == null || val == null)
        {
            return;
        }
        WriteKeyValuePair(key, val);
    }

    void WriteKeyValuePair(string key, object val)
    {
        ObjectWriter.Write($"{key}:{val}{Environment.NewLine}");
    }
    public void Write(object obj)
    {
        if (obj is not DifficultyAttributes difficultyAttributes)
        {
            throw new InvalidCastException();
        }
        Write(difficultyAttributes);
    }
    
    public void Write(DifficultyAttributes obj)
    {
        ObjectWriter.Write($"[Difficulty]{Environment.NewLine}");
        WriteKeyValuePair("HPDrainRate", obj.HpDrain);
        WriteKeyValuePair("CircleSize", obj.CircleSize);
        WriteKeyValuePair("OverallDifficulty", obj.OverallDifficulty);
        WriteKeyValuePair("ApproachRate", obj.ApproachRate);
        WriteKeyValuePair("SliderMultiplier", obj.SliderMultiplier);
        WriteKeyValuePair("SliderTickRate", obj.SliderTickRate);
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