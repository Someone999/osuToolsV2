﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using osuToolsV2.StoryBoard.Colors;
using osuToolsV2.StoryBoard.Commands;

namespace osuToolsV2.StoryBoard.Transitions.Initialization
{
    internal class ColorTransitionInitializationInfo: ITypeInitializationInfo<ColorTransition>
    {
        public Type TargetType => typeof(ColorTransition);
        public ColorTransition CreateInstance(double[] startTransitions, double[] endTransitions, double startTime, double endTime)
        {
            RgbColor startColor = new RgbColor(startTransitions[0], startTransitions[1], startTransitions[2]);
            RgbColor endColor = new RgbColor(endTransitions[0], endTransitions[1], endTransitions[2]);
            return new ColorTransition(startColor, endColor, startTime, endTime);
        }
    }
}
