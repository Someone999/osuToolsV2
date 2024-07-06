namespace osuToolsV2.Beatmaps.BreakTimes;

public class VideoInfo
{
    public bool HasVideo => FileName != null;
    public string? FileName { get; set; } 
    public double StartTime { get; set; }
}