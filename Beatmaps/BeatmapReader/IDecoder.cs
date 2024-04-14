namespace osuToolsV2.Beatmaps.BeatmapReader;

public interface IDecoder<TOutput>
{
    TOutput Decode(StringReader stringReader);
}