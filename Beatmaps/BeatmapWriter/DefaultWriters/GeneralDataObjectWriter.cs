using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class GeneralDataObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public GeneralDataObjectWriter(IObjectWriter<TWriterType> objectWriter)
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
        if (obj is not Beatmap beatmap)
        {
            throw new InvalidCastException();
        }
        
        Write(beatmap);
        
    }
    public bool NeedClose => Writer.NeedClose;
    public void Close()
    {
        if (NeedClose)
        {
            Writer.Close();
        }
    }
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
    
    public void Write(Beatmap obj)
    {
        IsWriting = true;
        Writer.Write($"[General]{Environment.NewLine}");
        WriteKeyValuePair("AudioFilename", obj.Metadata.AudioFileName);
        WriteKeyValuePair("AudioLeadIn", obj.AudioLeadIn);
        WriteKeyValuePair("PreviewTime", obj.PreviewTime.TotalMilliseconds);
        
        if (obj.CountdownType != CountdownType.None)
        {
            WriteKeyValuePair("Countdown", obj.CountdownType);
        }

        if (obj.CountdownOffset != null)
        {
            WriteKeyValuePair("CountdownOffset", obj.CountdownOffset);
        }
        
        WriteKeyValuePair("SampleSet", obj.SampleSet);
        WriteKeyValuePair("StackLeniency", obj.StackLeniency);
        WriteKeyValuePair("Mode", (int)obj.Ruleset.LegacyRuleset!);
        WriteKeyValuePair("SpecialStyle", (int)obj.SpecialStyle);
        WriteKeyValuePair("WidescreenStoryboard", obj.WidescreenStoryboard ? 1 : 0);
        

        if (obj.EpilepsyWarning != null)
        {
            bool epilepsyWarning = obj.EpilepsyWarning.Value;
            Writer.Write($"EpilepsyWarning: {(epilepsyWarning ? 1 : 0)}");
            Writer.Write(Environment.NewLine);
        }
        
        
        if (obj.LetterboxInBreaks != null)
        {
            bool letterboxInBreaks = obj.LetterboxInBreaks.Value;
            WriteKeyValuePair("LetterboxInBreaks", obj.LetterboxInBreaks.Value ? 1 : 0);
            Writer.Write(Environment.NewLine);
        }
        
        if (obj.UseSkinSprites != null)
        {
            bool useSkinSprites = obj.UseSkinSprites.Value;
            WriteKeyValuePair("UseSkinSprites", useSkinSprites ? 1 : 0);
            Writer.Write(Environment.NewLine);
        }
        
        if (obj.SamplesMatchPlaybackRate != null)
        {
            bool samplesMatchPlaybackRate = obj.SamplesMatchPlaybackRate.Value;
            WriteKeyValuePair("UseSkinSprites", samplesMatchPlaybackRate ? 1 : 0);
            Writer.Write(Environment.NewLine);
        }
        WriteKeyValuePairIfNotNull("OverlayPosition", obj.OverlayPosition);
        WriteKeyValuePairIfNotNull("SkinPreference", obj.SkinPreference);
        
        Writer.Write(Environment.NewLine);
        IsWriting = false;
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