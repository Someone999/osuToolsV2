using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.StoryBoard.Transitions;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Rotate: SubStoryBoardCommand
    {
        public override string ToFileContent()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"R,{Easing},{StartTime},{EndTime},");
            var firstTransition = (RotateTransition)Transitions[0];
            sb.Append($",{firstTransition.StartDegrees.Radians}");
            for (int i = 1; i < Transitions.Count; i++)
            {
                if (i <  Transitions.Count - 1)
                {
                    sb.Append(',');
                }
                sb.Append($"{((RotateTransition) Transitions[i]).EndDegrees.Radians}");
            }

            return sb.ToString();
        }
    }
}
