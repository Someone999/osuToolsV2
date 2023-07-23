namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public class ManiaHit : HitObject, IManiaHitObject
{
    public override HitObjectType HitObjectType => HitObjectType.ManiaHit;
    public override string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }
    public int Column { get; set; }
    public int BeatmapColumn { get; set; }

    public ManiaHit(OriginalHitObjectType originalHitObjectType) : base(originalHitObjectType)
    {
    }
}