using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

public class EventsObjectWriter<TWriterType> : IObjectWriter<Beatmap, IObjectWriter<TWriterType>>
{

    public EventsObjectWriter(IObjectWriter<TWriterType> objectWriter)
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
    public void Write(Beatmap obj)
    {
        ObjectWriter.Write($"[Events]{Environment.NewLine}");
        if (obj.BreakTimes.Count > 0)
        {
            foreach (var breakTime in obj.BreakTimes)
            {
                ObjectWriter.Write(breakTime.ToFileFormat() + Environment.NewLine);
            }
        }

        if (obj.Metadata.BackgroundInfo.HasBackground)
        {
            var bgInfo = obj.Metadata.BackgroundInfo;
            ObjectWriter.Write($"0,0,\"{bgInfo.FileName}\",{bgInfo.X},{bgInfo.Y}{Environment.NewLine}");
        }
        if (obj.Metadata.VideoInfo.HasVideo)
        {
            var viInfo = obj.Metadata.VideoInfo;
            ObjectWriter.Write($"1,0,\"{viInfo.FileName},{viInfo.X},{viInfo.Y}\"{Environment.NewLine}");
        }
        if(obj.InlineStoryBoardCommand != null)
        {
            foreach (var command in obj.InlineStoryBoardCommand)
            {
                ObjectWriter.Write(command.ToFileContent() + Environment.NewLine);
            }
        }
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