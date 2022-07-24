namespace osuToolsV2.Beatmaps.BreakTimes
{

    public class BreakTime
    {
        public double StartTime { get; set; }
        public double EndTime { get; set; }
        public double Duration => EndTime - StartTime;
        public BreakTime(double startTime, double endTime)
        {
            StartTime = startTime;
            EndTime = endTime;
        }

        public string ToFileFormat()
        {
            return $"2,{StartTime},{EndTime}";
        }
    }
}
