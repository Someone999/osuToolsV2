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
        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"C,{Easing},{StartTime},{EndTime},");
            var firstTransition = (ColorTransition)Transitions[0];
            sb.Append(firstTransition.StartColor);
            sb.Append(',');
            sb.Append(firstTransition.EndColor);
            for (var i = 1; i < Transitions.Count; i++)
            {
                if (i < Transitions.Count - 1)
                {
                    sb.Append(',');
                }
                sb.Append(((ColorTransition)Transitions[i]).EndColor);
            }
            return sb.ToString();
        }
    }
}
