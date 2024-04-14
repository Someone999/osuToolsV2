namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class MoveYTransitionInitializationInfo : ITypeInitializationInfo<MoveTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public MoveTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new MoveTransition(0, startTransitions[0], 0, endTransitions[0],
            startTime, endTime);
    }
}