namespace osuToolsV2.Graphic;

public struct OsuPixel : ICloneable
{
    public OsuPixel(double x, double y)
    {
        X = x;
        Y = y;
    }
    
    public double X { get; set; }
    public double Y { get; set; }

    public object Clone()
    {
        return new OsuPixel(X, Y);
    }

    public static OsuPixel Empty { get; } = new OsuPixel();
}