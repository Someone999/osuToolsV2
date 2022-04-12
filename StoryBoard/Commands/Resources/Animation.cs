
using osuToolsV2.Graphic;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.StoryBoard.Commands.Resources;

public class Animation : MainStoryBoardCommand
{
    public OsuPixel Position { get; set; } = new();
    public StoryBoardOrigin Origin { get; set; }
    public int FrameCount { get; set; }
    public double FrameDelay { get; set; }
    public StoryBoardAnimationLoopType LoopType { get; set; }

    /*public override string ToFileContent() =>
        $"Animation,{Layer},{Origin},\"{FileName}\",{Position.X},{Position.Y},{FrameCount},{FrameDelay},{LoopType}";*/
}