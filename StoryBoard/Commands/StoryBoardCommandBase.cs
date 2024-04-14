namespace osuToolsV2.StoryBoard.Commands
{
    public abstract class StoryBoardCommandBase//: IOsuFileContent
    {
        public List<SubStoryBoardCommand> SubCommands { get;  set; } = new ();
        public SubStoryBoardCommand AsSubStoryBoardCommand() => (SubStoryBoardCommand) this;
        public MainStoryBoardCommand AsMainStoryBoardCommand() => (MainStoryBoardCommand) this;
        public abstract string ToFileContent();
    }
}
