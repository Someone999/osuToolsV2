using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
