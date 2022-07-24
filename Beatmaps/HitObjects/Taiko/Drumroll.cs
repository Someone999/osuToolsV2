using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class Drumroll : IHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.Drumroll;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public double Length { get; set; }
    public void Parse(string[] data)
    {
        throw new NotSupportedException("You need to use GenericTaikoHitParser.Parse");
    }
    public OriginalHitObjectType OriginalHitObjectType { get; internal set; }
    public string ToFileFormat()
    {
        throw new NotSupportedException();
    }
}