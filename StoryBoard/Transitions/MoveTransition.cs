namespace osuToolsV2.StoryBoard.Transitions;

public class MoveTransition : ITransition
{
    public double StartX { get; set; }
    public double StartY { get; set; }
    public double EndX { get; set; }
    public double EndY { get; set; }
    public double StartTime { get; set; }
    public double EndTime { get; set; }

    public MoveTransition(double startX, double startY, double endX, double endY, double startTime, double endTime)
    {
        StartX = startX;
        StartY = startY;
        EndX = endX;
        EndY = endY;
        StartTime = startTime;
        EndTime = endTime;
    }
}