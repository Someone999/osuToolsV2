using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class JuiceStream : HitObject
{
    public override HitObjectType HitObjectType => HitObjectType.JuiceStream;
    public List<OsuPixel> CurvePoints { get; internal set; } = new List<OsuPixel>();
    public Slider.CurveTypes CurveType { get; internal set; }
    public double Lenght { get; set; } 
    public int SlideTimes { get; set; }
    
    
    public override string ToFileFormat()
    {
        throw new NotSupportedException();
    }

    public JuiceStream(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}