using osuToolsV2.StoryBoard.Enums;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public abstract class StoryBoardCommandBase//: IOsuFileContent
    {
        public List<SubStoryBoardCommand> SubCommands { get;  set; } = new ();
        public SubStoryBoardCommand AsSubStoryBoardCommand() => (SubStoryBoardCommand) this;
        public MainStoryBoardCommand AsMainStoryBoardCommand() => (MainStoryBoardCommand) this;
        //public abstract string ToFileContent();
    }

    public abstract class MainStoryBoardCommand : StoryBoardCommandBase
    {
        public StoryBoardLayer Layer { get; set; }
        public string FileName { get; set; } = string.Empty;
    }

    public abstract class SubStoryBoardCommand : StoryBoardCommandBase
    {
        public double StartTime { get; set; }
        public double? EndTime { get; set; }
        public List<ITransition> Transitions { get; set; } = new();
        public StoryBoardCommandBase? Parent { get; set; }
        public StoryBoardEasing? Easing { get; set; }
    }
}
