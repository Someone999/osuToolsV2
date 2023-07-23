namespace osuToolsV2.StoryBoard.Commands
{
    public class Trigger : SubStoryBoardCommand
    {
        public string TriggerType { get; set; } = string.Empty;
        public override string ToFileContent()
        {
            return "NotImplementedException";
        }
    }
}
