namespace osuToolsV2.StoryBoard.Colors
{
    public class RgbColor
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public override string ToString() => $"{Red},{Green},{Blue}";

        public RgbColor(double red, double green, double blue)
        {
            Red = red;
            Green = green;
            Blue = blue;
        }
    }
}
