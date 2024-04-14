namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class MoveXTransitionInitializationInfo : ITypeInitializationInfo<MoveTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public MoveTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new MoveTransition(startTransitions[0], 0, endTransitions[0], 0,
            startTime, endTime);
    }
}