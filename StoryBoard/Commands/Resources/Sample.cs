namespace osuToolsV2.StoryBoard.Commands.Resources;

public class Sample : MainStoryBoardCommand
{
    public double Volume { get; set; }
    public double StartTime { get; set; }
    public override string ToFileContent() => $"Sample,{StartTime},{(int)Layer},\"{FileName}\",{Volume}";
}