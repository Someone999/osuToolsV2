namespace osuToolsV2.StoryBoard.Transitions.Initialization;

internal class MoveTransitionInitializationInfo : ITypeInitializationInfo<MoveTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public MoveTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new MoveTransition(startTransitions[0], startTransitions[1], endTransitions[0], endTransitions[1],
            startTime, endTime);
    }
}

internal class MoveXTransitionInitializationInfo : ITypeInitializationInfo<MoveTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public MoveTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new MoveTransition(startTransitions[0], 0, endTransitions[0], 0,
            startTime, endTime);
    }
}
internal class MoveYTransitionInitializationInfo : ITypeInitializationInfo<MoveTransition>
{
    public Type TargetType => typeof(FadeTransition);
    public MoveTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
    {
        return new MoveTransition(0, startTransitions[0], 0, endTransitions[0],
            startTime, endTime);
    }
}