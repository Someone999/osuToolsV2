using osuToolsV2.StoryBoard.Colors;

namespace osuToolsV2.StoryBoard.Transitions;

public class ColorTransition : ITransition
{
    public RgbColor StartColor { get; set; }
    public RgbColor EndColor { get; set; }
    public double StartTime { get; set; }
    public double EndTime { get; set; }

    public ColorTransition(RgbColor startColor, RgbColor endColor, double startTime, double endTime)
    {
        StartColor = startColor;
        EndColor = endColor;
        StartTime = startTime;
        EndTime = endTime;
    }
}