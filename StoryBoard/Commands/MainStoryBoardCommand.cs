using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.StoryBoard.Commands;

public abstract class MainStoryBoardCommand : StoryBoardCommandBase
{
    public StoryBoardLayer Layer { get; set; }
    public string FileName { get; set; } = string.Empty;
}