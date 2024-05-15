using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class BeatmapMetadataObjectWriter<TWriterType> : 
    IObjectWriter<BeatmapMetadata, IObjectWriter<TWriterType>>
{
    public BeatmapMetadataObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        _writer = objectWriter;
    }
    
    public IObjectWriter<TWriterType> Writer 
    {
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
    public void Write(BeatmapMetadata obj)
    {
        IsWriting = true;
        Writer.Write($"[Metadata]{Environment.NewLine}");
        Writer.Write($"Title:{obj.Title}{Environment.NewLine}");
        Writer.Write($"TitleUnicode:{obj.TitleUnicode}{Environment.NewLine}");
        Writer.Write($"Artist:{obj.Artist}{Environment.NewLine}");
        Writer.Write($"ArtistUnicode:{obj.ArtistUnicode}{Environment.NewLine}");
        Writer.Write($"Creator:{obj.Creator}{Environment.NewLine}");
        Writer.Write($"Version:{obj.Version}{Environment.NewLine}");
        Writer.Write($"Source:{obj.Source}{Environment.NewLine}");
        Writer.Write($"Tags:{obj.Tags}{Environment.NewLine}");
        if (obj.BeatmapId != null)
        {
            Writer.Write($"BeatmapID:{obj.BeatmapId}{Environment.NewLine}");
        }
        
        if (obj.BeatmapSetId != null)
        {
            Writer.Write($"BeatmapSetID:{obj.BeatmapSetId}{Environment.NewLine}");
        }
        Writer.Write(Environment.NewLine);
        IsWriting = false;
    }

    
    public void Write(object obj)
    {
        Write((BeatmapMetadata)obj);
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