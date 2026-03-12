﻿using System.Collections.Generic;
using osuToolsV2.StoryBoard.Math;

namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class RotateTransitionGenerator : ITransitionGenerator
{
    private RotateTransitionGenerator()
    {
    }
    
    
    public List<ITransition> Create(double startTime, double endTime, IReadOnlyList<string> data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 1;
        Degrees? lastDegree = null;
        var realStartTime = startTime;
        var realEndTime = endTime;
        
        if (data.Count < transitionIndex + dataLength)
        {
            throw new ArgumentException("Data length is insufficient for the required transitions.");
        }
        
        for (int i = transitionIndex; i < data.Count; i++)
        {
            if (data.Count < i + dataLength)
            {
                break;
            }
            
            var startDegree = lastDegree ?? new Degrees(0, true);
            var endDegree = new Degrees(double.Parse(data[i]), true);

            var rotateTransition = new RotateTransition(startDegree, endDegree, realStartTime, realEndTime);
            realStartTime += duration;
            realEndTime += duration;
            
            lastDegree = endDegree;
            transitions.Add(rotateTransition);
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new RotateTransitionGenerator();
}