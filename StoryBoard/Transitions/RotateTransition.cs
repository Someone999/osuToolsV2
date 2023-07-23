using osuToolsV2.StoryBoard.Math;

namespace osuToolsV2.StoryBoard.Transitions;

public class RotateTransition : ITransition
{
    public RotateTransition(Degrees startDegrees, Degrees endDegrees, double startTime, double endTime)
    {
        StartDegrees = startDegrees;
        EndDegrees = endDegrees;
        StartTime = startTime;
        EndTime = endTime;
    }

    public Degrees StartDegrees { get; set; }
    public Degrees EndDegrees { get; set; }
    public double StartTime { get; set; }
    public double EndTime { get; set; }
}