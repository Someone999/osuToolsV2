namespace osuToolsV2.StoryBoard.Commands.Resources;

public class Video : MainStoryBoardCommand
{
    public double StartTime { get; set; }
    public override string ToFileContent()
    {
        return $"Video,{StartTime},\"{FileName}\"";
    }
}