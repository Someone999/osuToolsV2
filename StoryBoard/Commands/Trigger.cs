using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Trigger : SubStoryBoardCommand
    {
        public string TriggerType { get; set; } = string.Empty;
    }
}
