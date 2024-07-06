using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.LazyLoaders;
using osuToolsV2.StoryBoard.Commands.Resources;

namespace osuToolsV2.Beatmaps;

public class BeatmapMetadata
{
    public BeatmapMetadata()
    {
    }
    public BeatmapMetadata(StoryBoardCommandLazyLoader storyBoardCommandLazyLoader)
    {
        storyBoardCommandLazyLoader.LoadObject();
    }
    

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
    public BeatmapObjectHolder<Background> BackgroundHolder { get; set; } = new BeatmapObjectHolder<Background>();
    public BeatmapObjectHolder<Video> VideoHolder { get; set; } = new BeatmapObjectHolder<Video>();

    public string? BeatmapFullPath { get; set; }
    public string? BeatmapFileName { get; set; }
    
    public string? Md5Hash { get; set; }
    public int? BeatmapId { get; set; }
    public int? BeatmapSetId { get; set; }
}