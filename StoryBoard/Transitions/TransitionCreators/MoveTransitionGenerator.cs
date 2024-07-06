namespace osuToolsV2.StoryBoard.Transitions.TransitionCreators;

public class MoveTransitionGenerator : ITransitionGenerator
{
    private MoveTransitionGenerator()
    {
    }
    
    
    public List<ITransition> Create(double startTime, double endTime, string[] data)
    {
        List<ITransition> transitions = new List<ITransition>();
        var duration = endTime - startTime;
        const int transitionIndex = 4;
        const int dataLength = 2;
        double? lastStartX = null, lastStartY = null;
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

            var startX = lastStartX ?? 0;
            var startY = lastStartY ?? 0;

            var endX = double.Parse(data[i]);
            var endY = double.Parse(data[i + 1]);
            i++;
            
            var moveTransition = new MoveTransition(startX, startY, endX, endY, realStartTime, realEndTime);
            realStartTime += duration;
            realEndTime += duration;

            lastStartX = endX;
            lastStartY = endY;
            transitions.Add(moveTransition);
        }

        return transitions;
    }

    public static ITransitionGenerator Instance { get; } = new MoveTransitionGenerator();
}