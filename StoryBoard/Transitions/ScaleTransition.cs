namespace osuToolsV2.StoryBoard.Transitions;

public class ScaleTransition : ITransition
{
    public double StartRatio { get; set; }
    public double EndRatio { get; set; }
    public double StartTime { get; set; }
    public double EndTime { get; set; }

    public ScaleTransition(double startRatio, double endRatio, double startTime, double endTime)
    {
        StartRatio = startRatio;
        EndRatio = endRatio;
        StartTime = startTime;
        EndTime = endTime;
    }
}