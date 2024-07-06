namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class MoveYTransitionGenerator : ITransitionGenerator
{
    private MoveYTransitionGenerator()
    {
    }
    
    
    public List<ITransition> Create(double startTime, double endTime, string[] data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 1;
        double? lastStartY = null;
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
            
            var startY = lastStartY ?? 0;
            
            var endY = double.Parse(data[i]);
            i++;
            
            var moveTransition = new MoveTransition(0, startY, 0, endY, realStartTime, realEndTime);
            realStartTime += duration;
            realEndTime += duration;
            
            lastStartY = endY;
            transitions.Add(moveTransition);
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new MoveYTransitionGenerator();
}