using System.Text;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public class MoveY: SubStoryBoardCommand
    {
        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"MY,{Easing},{StartTime},{EndTime},");
            sb.Append($"{((MoveTransition) Transitions[0]).StartX},{((MoveTransition) Transitions[0]).StartY}");
            sb.Append($"{((MoveTransition) Transitions[0]).EndX},{((MoveTransition) Transitions[0]).EndY}");
            for (var i = 1; i < Transitions.Count; i++)
            {
                if (i <  Transitions.Count - 1)
                {
                    sb.Append(',');
                }
                sb.Append($"{((MoveTransition) Transitions[i]).EndX},{((MoveTransition) Transitions[i]).EndY}");
            }
            return sb.ToString();
        }
    }
}
