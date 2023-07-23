namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class HitCircle : HitObject
{
    public override HitObjectType HitObjectType => HitObjectType.HitCircle;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }


    public HitCircle(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}