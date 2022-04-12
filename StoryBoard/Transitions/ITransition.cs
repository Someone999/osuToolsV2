namespace osuToolsV2.StoryBoard.Transitions
{
    public interface ITransition
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
    }
}
