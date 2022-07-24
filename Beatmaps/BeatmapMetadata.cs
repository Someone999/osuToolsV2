using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.GameInfo;

namespace osuToolsV2.Beatmaps;

public class BeatmapMetadata
{
    private OsuInfo _gameInfo = new();
    public string Artist { get; set; } = "";
    public string ArtistUnicode { get; set; } = "";
    public string Title { get; set; } = "";
    public string TitleUnicode { get; set; } = "";
    public string Creator { get; set; } = "";
    public string Version { get; set; } = "";
    public string Source { get; set; } = "";
    public string Tags { get; set; } = "";
    public int BeatmapFileVersion { get; set; }
    public BackgroundInfo BackgroundInfo { get; set; } = new BackgroundInfo();

    public string? BackgroundFullPath => BackgroundInfo.FileName != null
        ? Path.Combine(_gameInfo.BeatmapDirectory, BackgroundInfo.FileName)
        : null;
    public string AudioFileName { get; set; } = "";
    public string AudioFileFullPath => Path.Combine(_gameInfo.BeatmapDirectory, AudioFileName);
    public VideoInfo VideoInfo { get; set; } = new VideoInfo();
    public string? VideoFileFullPath => VideoInfo.FileName != null
        ? Path.Combine(Path.GetDirectoryName(BeatmapFullPath) ?? "", VideoInfo.FileName)
        : null;


    public string BeatmapFullPath { get; set; } = "";
    public string BeatmapFileName { get; set; } = "";
    
    public string? Md5Hash { get; set; }
    public int? BeatmapId { get; set; }
    public int? BeatmapSetId { get; set; }
}