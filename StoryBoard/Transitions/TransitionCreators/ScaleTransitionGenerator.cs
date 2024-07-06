namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class ScaleTransitionGenerator : ITransitionGenerator
{
    private ScaleTransitionGenerator()
    {
    }
    
    
    public List<ITransition> Create(double startTime, double endTime, string[] data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 1;
        double? lastStartRatio = null;
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
            
            var startRatio = lastStartRatio ?? 0;
            var endRatio = double.Parse(data[i]);

            var ratioTransition = new ScaleTransition(startRatio, endRatio, realStartTime, realEndTime);
            realStartTime += duration;
            realEndTime += duration;
            
            lastStartRatio = endRatio;
            transitions.Add(ratioTransition);
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new ScaleTransitionGenerator();
}