namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class DenDen : HitObject
{
    public override HitObjectType HitObjectType => HitObjectType.DenDen;
   
    public double EndTime { get; set; }

    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)HitSound},{EndTime},{HitSample.ToFileFormat()}";
    }

    public DenDen(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}