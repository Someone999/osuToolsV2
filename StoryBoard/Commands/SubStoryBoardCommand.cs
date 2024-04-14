using osuToolsV2.StoryBoard.Enums;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands;

public abstract class SubStoryBoardCommand : StoryBoardCommandBase
{
    public double StartTime { get; set; }
    public double? EndTime { get; set; }
    public List<ITransition> Transitions { get; set; } = new();
    public StoryBoardCommandBase? Parent { get; set; }
    public StoryBoardEasing? Easing { get; set; }
}