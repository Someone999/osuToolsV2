using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class GeneralDataObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public GeneralDataObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
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
    public bool NeedClose => ObjectWriter.NeedClose;
    public void Close()
    {
        if (NeedClose)
        {
            ObjectWriter.Close();
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

    void WriteKeyValuePair(string key, object val)
    {
        ObjectWriter.Write($"{key}:{val}{Environment.NewLine}");
    }
    
    public void Write(Beatmap obj)
    {
        ObjectWriter.Write($"[General]{Environment.NewLine}");
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
            ObjectWriter.Write($"EpilepsyWarning: {(epilepsyWarning ? 1 : 0)}");
            ObjectWriter.Write(Environment.NewLine);
        }
        
        
        if (obj.LetterboxInBreaks != null)
        {
            bool letterboxInBreaks = obj.LetterboxInBreaks.Value;
            WriteKeyValuePair("LetterboxInBreaks", obj.LetterboxInBreaks.Value ? 1 : 0);
            ObjectWriter.Write(Environment.NewLine);
        }
        
        if (obj.UseSkinSprites != null)
        {
            bool useSkinSprites = obj.UseSkinSprites.Value;
            WriteKeyValuePair("UseSkinSprites", useSkinSprites ? 1 : 0);
            ObjectWriter.Write(Environment.NewLine);
        }
        
        if (obj.SamplesMatchPlaybackRate != null)
        {
            bool samplesMatchPlaybackRate = obj.SamplesMatchPlaybackRate.Value;
            WriteKeyValuePair("UseSkinSprites", samplesMatchPlaybackRate ? 1 : 0);
            ObjectWriter.Write(Environment.NewLine);
        }
        WriteKeyValuePairIfNotNull("OverlayPosition", obj.OverlayPosition);
        WriteKeyValuePairIfNotNull("SkinPreference", obj.SkinPreference);
        
        ObjectWriter.Write(Environment.NewLine);
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