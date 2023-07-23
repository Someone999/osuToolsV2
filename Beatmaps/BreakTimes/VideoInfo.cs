namespace osuToolsV2.Beatmaps.BreakTimes;

public class VideoInfo
{
    public bool HasVideo => FileName != null;
    public string? FileName { get; set; } 
    public double X { get; set; }
    public double Y { get; set; }
    public double StartTime { get; set; }
}