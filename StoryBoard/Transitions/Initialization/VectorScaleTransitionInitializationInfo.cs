namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class VectorScaleTransitionInitializationInfo : ITypeInitializationInfo<VectorScaleTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public VectorScaleTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new VectorScaleTransition(startTransitions[0], startTransitions[1], endTransitions[0], endTransitions[1],
            startTime, endTime);
    }
}