﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.StoryBoard.Commands
{
    public class Parameter : SubStoryBoardCommand
    {
        public ParameterOperation ParameterOperation { get; set; }
        public override string ToFileContent()
        {
            return "NotImplementedException";
        }
    }
}
