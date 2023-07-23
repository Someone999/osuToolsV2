namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class TaikoLargeRedHit : HitObject, ITaikoHit
{
    public override HitObjectType HitObjectType => HitObjectType.TaikoLargeRedHit;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }

    public TaikoLargeRedHit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}