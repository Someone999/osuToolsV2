using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Color: SubStoryBoardCommand
    {
        /*public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"C,{Easing},{StartTime},{EndTime},");
            sb.Append(Transitions[0] as ColorTransition);
            for (var i = 1; i < Transitions.Count; i++)
            {
                sb.Append(((ColorTransition)Transitions[i]).EndColor);
            }
            return sb.ToString();
        }*/
    }
}
