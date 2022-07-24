using osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;

namespace osuToolsV2.Writer;



public interface IObjectWriter<out TWriter>
{
    TWriter ObjectWriter { get; }
    void Write(object obj);
    bool NeedClose { get; }
    void Close();
}

public interface IObjectWriter<in TWriteObject, out TWriter> : IObjectWriter<TWriter>
{
    void Write(TWriteObject obj);
}




