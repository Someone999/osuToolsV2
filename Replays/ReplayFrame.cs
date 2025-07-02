using osuToolsV2.Utils;

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

    public long Offset { get; internal init; }
    public long TimeFromLastAction { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public ReplayButtonState ButtonState { get; set; }

    public bool IsSameFrame(ReplayFrame frame)
    {
        var sameOffset = Offset == frame.Offset;
        if (!sameOffset)
        {
            return false;
        }
        
        var sameDeltaTime = TimeFromLastAction == frame.TimeFromLastAction;
        if (!sameDeltaTime)
        {
            return false;
        }
        
        var sameX = Math.Abs(X - frame.X) < 1e-5;
        if (!sameX)
        {
            return false;
        }
        
        var sameY = Math.Abs(Y - frame.Y) < 1e-5;
        if (!sameY)
        {
            return false;
        }
        
        var sameButtonState = ButtonState == frame.ButtonState;
        return sameButtonState;
    }
}