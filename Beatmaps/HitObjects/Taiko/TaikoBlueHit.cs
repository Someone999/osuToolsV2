namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class TaikoBlueHit : HitObject, ITaikoHit
{
    public override HitObjectType HitObjectType => HitObjectType.TaikoBlueHit;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }

    public TaikoBlueHit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}