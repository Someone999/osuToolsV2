namespace osuToolsV2.StoryBoard.Math
{
    public class Degrees
    {
        public double Degree { get; set; }
        public double Radians { get; set; }
        public Degrees(double val, bool isDegree)
        {
            if (isDegree)
            {
                Degree = val;
                Radians = System.Math.PI / 180 * val;
            }
            else
            {
                Radians = val;
                Degree = 180 / System.Math.PI * val;
            }
        }
    }
}
