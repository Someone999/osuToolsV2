namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class TaikoRedHit :HitObject, ITaikoHit
{ 
    public override HitObjectType HitObjectType => HitObjectType.TaikoRedHit;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }

    public TaikoRedHit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}