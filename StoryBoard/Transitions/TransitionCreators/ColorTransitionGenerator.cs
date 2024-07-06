using osuToolsV2.StoryBoard.Colors;

namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class ColorTransitionGenerator : ITransitionGenerator
{
    private ColorTransitionGenerator()
    {
    }

    private RgbColor ReadNext(string[] data, ref int i)
    {
        var r = double.Parse(data[i]);
        var g = double.Parse(data[i + 1]);
        var b = double.Parse(data[i + 2]);
        i += 2;
        return new RgbColor(r, g, b);
    }
    
    public List<ITransition> Create(double startTime, double endTime, string[] data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 3;
        RgbColor? lastStart = null;
        var realStartTime = startTime;
        var realEndTime = endTime;

       
        if (data.Length < transitionIndex + dataLength)
        {
            throw new ArgumentException("Data length is insufficient for the required transitions.");
        }
       
        
        for (int i = transitionIndex; i < data.Length; i++)
        {
            if (data.Length < i + dataLength)
            {
                break;
            }

            var start = lastStart ?? new RgbColor(0, 0, 0);
            var end = ReadNext(data, ref i);
            
            var colorTransition = new ColorTransition(start, end, realStartTime, realEndTime);
           
            realStartTime += duration;
            realEndTime += duration;

            lastStart = end;
            transitions.Add(colorTransition);
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new ColorTransitionGenerator();
}