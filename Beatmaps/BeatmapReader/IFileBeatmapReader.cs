using osuToolsV2.Reader;

namespace osuToolsV2.Beatmaps.BeatmapReader;

public interface IFileBeatmapReader : IObjectReader<StreamReader, Beatmap>
{
}