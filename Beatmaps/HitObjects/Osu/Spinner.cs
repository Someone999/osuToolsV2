namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class Spinner : HitObject
{

   
    public override HitObjectType HitObjectType => HitObjectType.Spinner;
  
    public double EndTime { get; set; }

    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)HitSound},{EndTime},{HitSample.ToFileFormat()}";
    }

    public Spinner(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}