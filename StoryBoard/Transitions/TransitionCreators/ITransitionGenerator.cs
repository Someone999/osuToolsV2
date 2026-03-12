namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public interface ITransitionGenerator
{
    List<ITransition> Create(double startTime, double endTime, IReadOnlyList<string> data);
}