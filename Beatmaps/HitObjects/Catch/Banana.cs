namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class Banana : HitObject, IHasEndTime
{

   
    public override HitObjectType HitObjectType => HitObjectType.Banana;
  
    public double EndTime { get; set; }

    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)HitSound},{EndTime},{HitSample.ToFileFormat()}";
    }

    public Banana(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}