using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class DenDen : IHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.DenDen;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public double EndTime { get; set; }
    public void Parse(string[] data)
    {
        throw new NotSupportedException("You need to use GenericTaikoHitParser.Parse");
    }
}