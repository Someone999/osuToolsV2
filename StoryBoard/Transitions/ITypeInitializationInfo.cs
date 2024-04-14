namespace osuToolsV2.StoryBoard.Transitions;

public interface ITypeInitializationInfo<out T>
{
    Type TargetType { get; }
    T CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime);
}