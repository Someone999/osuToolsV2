namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class FadeTransitionGenerator : ITransitionGenerator
{
    private FadeTransitionGenerator()
    {
    }
    
    public List<ITransition> Create(double startTime, double endTime, string[] data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 1;
        double? lastStart = null;
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
                throw new ArgumentException("Data length is insufficient for the required transitions.");
            }
            
            var start = lastStart ?? 0;
            var end = double.Parse(data[i]);
            
            transitions.Add(new FadeTransition(start, end, realStartTime, realEndTime));
            realStartTime += duration;
            realEndTime += duration;

            lastStart = end;
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new FadeTransitionGenerator();
}