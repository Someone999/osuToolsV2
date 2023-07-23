using System.Text;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Fade : SubStoryBoardCommand
    {
        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"F,{Easing},{StartTime},{EndTime},{((FadeTransition) Transitions[0]).StartOpacity},");
            sb.Append(((FadeTransition) Transitions[0]).EndOpacity);
            for (var i = 1; i < Transitions.Count; i++)
            {
                if (i <  Transitions.Count)
                {
                    sb.Append(',');
                }
                sb.Append(((FadeTransition)Transitions[i]).EndOpacity);
            }
            return sb.ToString();
        }
    }
}
