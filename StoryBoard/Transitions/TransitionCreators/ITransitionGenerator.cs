using HsManCommonLibrary.CommandLine.Matchers;
using osuToolsV2.StoryBoard.Commands;

namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public interface ITransitionGenerator
{
    List<ITransition> Create(double startTime, double endTime, string[] data);
}