namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class Fruit : HitObject
{
    
    public override HitObjectType HitObjectType => HitObjectType.Fruit;
    

    public override string ToFileFormat()
    {
        return string.Join(",", Position.X, Position.Y, StartTime, (int)OriginalHitObjectType, (int)HitSound, HitSample.ToFileFormat());;
    }

    public Fruit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}