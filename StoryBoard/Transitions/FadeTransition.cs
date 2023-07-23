namespace osuToolsV2.StoryBoard.Transitions
{
    public class FadeTransition : ITransition
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; } 
        public double StartOpacity { get; set; }
        public double EndOpacity { get; set; }

        public FadeTransition( double startOpacity, double endOpacity, double startTime, double endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
            StartOpacity = startOpacity;
            EndOpacity = endOpacity;
        }
    }
}
