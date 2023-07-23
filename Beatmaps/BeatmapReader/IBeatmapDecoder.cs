namespace osuToolsV2.Beatmaps.BeatmapReader;

public interface IDecoder<TOutput>
{
    TOutput Decode(StringReader stringReader);
}

public interface IBeatmapDecoder : IDecoder<Beatmap>
{
}