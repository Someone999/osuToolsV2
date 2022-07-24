namespace osuToolsV2.Beatmaps.BreakTimes;

public class BackgroundInfo
{
    public bool HasBackground => FileName != null;
    public string? FileName { get; set; } 
    public double X { get; set; }
    public double Y { get; set; }
}