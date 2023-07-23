using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class Slider : HitObject
{
    public enum CurveTypes
    {
        Bezire,
        Catmull,
        Linear,
        PerfectCircle
    }
    
    public override HitObjectType HitObjectType => HitObjectType.Slider;
   
    public int SlideTimes { get; set; }
    public double Length { get; set; }
    public List<OsuPixel> CurvePoints { get; internal set; } = new List<OsuPixel>();
    public CurveTypes CurveType { get; set; }
    

    public override string ToFileFormat()
    {
        throw new NotSupportedException();
    }

    public Slider(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}