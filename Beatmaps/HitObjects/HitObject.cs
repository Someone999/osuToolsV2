using HsManCommonLibrary.Attributes;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects;

public class HitObject : IHitObject
{
    public HitObject(OriginalHitObjectType originalHitObjectType)
    {
        OriginalHitObjectType = originalHitObjectType;
    }

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public virtual HitObjectType HitObjectType => HitObjectType.None;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;

    public OriginalHitObjectType OriginalHitObjectType { get; }
    [Deprecated("No implement here because child classes have implemented")]
    public virtual string ToFileFormat()
    {
        throw new NotImplementedException();
    }
}