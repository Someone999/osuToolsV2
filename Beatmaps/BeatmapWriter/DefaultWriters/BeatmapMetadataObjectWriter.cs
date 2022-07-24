using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class BeatmapMetadataObjectWriter<TWriterType> : 
    IObjectWriter<BeatmapMetadata, IObjectWriter<TWriterType>>
{
    public BeatmapMetadataObjectWriter(IObjectWriter<TWriterType> objectWriter)
    {
        ObjectWriter = objectWriter;
    }
    public IObjectWriter<TWriterType> ObjectWriter { get; }
    public void Write(BeatmapMetadata obj)
    {
        ObjectWriter.Write($"[Metadata]{Environment.NewLine}");
        ObjectWriter.Write($"Title:{obj.Title}{Environment.NewLine}");
        ObjectWriter.Write($"TitleUnicode:{obj.TitleUnicode}{Environment.NewLine}");
        ObjectWriter.Write($"Artist:{obj.Artist}{Environment.NewLine}");
        ObjectWriter.Write($"ArtistUnicode:{obj.ArtistUnicode}{Environment.NewLine}");
        ObjectWriter.Write($"Creator:{obj.Creator}{Environment.NewLine}");
        ObjectWriter.Write($"Version:{obj.Version}{Environment.NewLine}");
        ObjectWriter.Write($"Source:{obj.Source}{Environment.NewLine}");
        ObjectWriter.Write($"Tags:{obj.Tags}{Environment.NewLine}");
        if (obj.BeatmapId != null)
        {
            ObjectWriter.Write($"BeatmapID:{obj.BeatmapId}{Environment.NewLine}");
        }
        
        if (obj.BeatmapSetId != null)
        {
            ObjectWriter.Write($"BeatmapSetID:{obj.BeatmapSetId}{Environment.NewLine}");
        }
        ObjectWriter.Write(Environment.NewLine);
    }

    
    public void Write(object obj)
    {
        Write((BeatmapMetadata)obj);
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