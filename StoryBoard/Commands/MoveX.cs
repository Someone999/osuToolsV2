using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.StoryBoard.Enums;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public class MoveX:SubStoryBoardCommand
    {
        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"MX,{Easing},{StartTime},{EndTime},");
            sb.Append($"{((MoveTransition) Transitions[0]).StartX}");
            sb.Append($"{((MoveTransition) Transitions[0]).EndX}");
            for (var i = 1; i < Transitions.Count; i++)
            {
                if (i <  Transitions.Count - 1)
                {
                    sb.Append(',');
                }
                sb.Append($"{((MoveTransition) Transitions[i]).EndX}");
            }
            return sb.ToString();
        }
    }
}
