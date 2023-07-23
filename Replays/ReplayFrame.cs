namespace osuToolsV2.Replays;

public class ReplayFrame
{
    public ReplayFrame(long timeFromLastAction, double x, double y, ReplayButtonState buttonState)
    {
        TimeFromLastAction = timeFromLastAction;
        X = x;
        Y = y;
        ButtonState = buttonState;
    }

    public long Offset { get; init; }
    public long TimeFromLastAction { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public ReplayButtonState ButtonState { get; set; }
}