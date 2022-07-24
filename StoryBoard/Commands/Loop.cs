using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Loop : SubStoryBoardCommand
    {
        public int LoopCount { get; set; }
        public override string ToFileContent()
        {
            throw new NotImplementedException();
        }
    }
}
