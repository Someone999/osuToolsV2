namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public class ManiaHold : HitObject, IManiaHitObject, IHasEndTime
{
    
    public override HitObjectType HitObjectType => HitObjectType.ManiaHold;
    public double EndTime { get; set; }
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{EndTime}:{HitSample.ToFileFormat()}";
    }
    public int Column { get; set; }
    public int BeatmapColumn { get; set; }

    public ManiaHold(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}