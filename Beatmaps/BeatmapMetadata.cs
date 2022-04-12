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
    public int BeatmapFileVersion { get; set; }
    public string? BackgroundFileName { get; set; } 
    public string? BackgroundFullPath { get; set; }
    public string? AudioFileName { get; set; } 
    public string? AudioFileFullPath { get; set; }
    public string? VideoFileName { get; set; } 
    public string? VideoFileFullPath { get; set; }
    
    public string? BeatmapFullPath { get; set; }
    public string? BeatmapFileName { get; set; }
    
    public string? Md5Hash { get; set; }
}