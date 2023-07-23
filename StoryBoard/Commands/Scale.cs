using System.Text;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Scale : SubStoryBoardCommand
    {

        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"MY,{Easing},{StartTime},{EndTime},");
            return "NotImplementedException";
        }
    }
}
