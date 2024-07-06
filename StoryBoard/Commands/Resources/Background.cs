using HsManCommonLibrary.ValueHolders;
using osuToolsV2.Graphic;

namespace osuToolsV2.StoryBoard.Commands.Resources;

public class Background : MainStoryBoardCommand
{
    public OsuPixel Position { get; set; } = new OsuPixel();
    public override string ToFileContent()
    {
        return $"Background,{Layer},\"{FileName}\",{Position.X},{Position.Y}";
    }
}