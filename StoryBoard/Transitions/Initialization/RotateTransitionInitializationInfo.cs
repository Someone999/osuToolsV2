using osuToolsV2.StoryBoard.Math;

namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class RotateTransitionInitializationInfo : ITypeInitializationInfo<RotateTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public RotateTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new RotateTransition(new Degrees(startTransitions[0], true), new Degrees(endTransitions[0], true),
            startTime, endTime);
    }
}