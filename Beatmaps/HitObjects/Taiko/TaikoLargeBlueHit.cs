namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class TaikoLargeBlueHit : HitObject, ITaikoHit
{
    public override HitObjectType HitObjectType => HitObjectType.TaikoLargeBlueHit;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }

    public TaikoLargeBlueHit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}