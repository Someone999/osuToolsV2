namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class Drumroll : HitObject, ITaikoHit
{
    public override HitObjectType HitObjectType => HitObjectType.Drumroll;
    
    public double Length { get; set; }
    public override string ToFileFormat()
    {
        throw new NotSupportedException();
    }

    public Drumroll(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}