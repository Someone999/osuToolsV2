namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class FadeTransitionInitializationInfo : ITypeInitializationInfo<FadeTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public FadeTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new FadeTransition(startTransitions[0], endTransitions[0], startTime, endTime);
    }
}