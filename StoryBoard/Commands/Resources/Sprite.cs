
using osuToolsV2.Graphic;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.StoryBoard.Commands.Resources
{
    

    public class Sprite: MainStoryBoardCommand
    {
        public OsuPixel Position { get; set; } = new();
        public StoryBoardOrigin Origin { get; set; }
        //public override string ToFileContent() => $"Sprite,{Layer},{Origin},\"{FileName}\",{Position.X},{Position.Y}";


    }
}
