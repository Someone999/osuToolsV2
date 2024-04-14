using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.GameInfo;

namespace osuToolsV2.Beatmaps;

public class BeatmapMetadata
{
    public string Artist { get; set; } = "";
    public string ArtistUnicode { get; set; } = "";
    public string Title { get; set; } = "";
    public string TitleUnicode { get; set; } = "";
    public string Creator { get; set; } = "";
    public string Version { get; set; } = "";
    public string Source { get; set; } = "";
    public string Tags { get; set; } = "";
    public int? BeatmapFileVersion { get; set; }
    public string? AudioFileName { get; set; }
    public BackgroundInfo? BackgroundInfo { get; set; }
    public VideoInfo? VideoInfo { get; set; }

    public string? BeatmapFullPath { get; set; }
    public string? BeatmapFileName { get; set; }
    
    public string? Md5Hash { get; set; }
    public int? BeatmapId { get; set; }
    public int? BeatmapSetId { get; set; }
}