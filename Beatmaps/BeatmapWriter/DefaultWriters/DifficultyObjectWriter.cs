using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class DifficultyObjectWriter<TWriterType> : 
    IObjectWriter<DifficultyAttributes, IObjectWriter<TWriterType>>
{

    public DifficultyObjectWriter(IObjectWriter<TWriterType> objectWriter)
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
    
    void WriteKeyValuePairIfNotNull(string? key, object? val)
    {
        if (key == null || val == null)
        {
            return;
        }
        WriteKeyValuePair(key, val);
    }

    void WriteKeyValuePair(string key, object? val)
    {
        Writer.Write($"{key}:{val ?? default}{Environment.NewLine}");
    }
    
    void WriteKeyValuePairNonNull(string key, object? val)
    {
        if (val == null)
        {
            return;
        }
        
        Writer.Write($"{key}:{val}{Environment.NewLine}");
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
        IsWriting = true;
        Writer.Write($"[Difficulty]{Environment.NewLine}");
        WriteKeyValuePair("HPDrainRate", obj.HpDrain);
        WriteKeyValuePair("CircleSize", obj.CircleSize);
        WriteKeyValuePair("OverallDifficulty", obj.OverallDifficulty);
        WriteKeyValuePair("ApproachRate", obj.ApproachRate);
        WriteKeyValuePair("SliderMultiplier", obj.SliderMultiplier);
        WriteKeyValuePair("SliderTickRate", obj.SliderTickRate);
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