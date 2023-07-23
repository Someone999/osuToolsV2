namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class ScaleTransitionInitializationInfo : ITypeInitializationInfo<ScaleTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public ScaleTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new ScaleTransition(startTransitions[0], endTransitions[0], startTime, endTime);
    }
}