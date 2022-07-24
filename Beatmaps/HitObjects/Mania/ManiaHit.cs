using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public class ManiaHit : IManiaHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.ManiaHit;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public void Parse(string[] data)
    {
        HitCircle hitCircle = new HitCircle();
        hitCircle.Parse(data);
        Position = hitCircle.Position;
        StartTime = hitCircle.StartTime;
        HitSound = hitCircle.HitSound;
        HitSample = hitCircle.HitSample;
        Column = (int) Math.Floor(Position.X * BeatmapColumn / 512d);
        OriginalHitObjectType = hitCircle.OriginalHitObjectType;
    }
    public OriginalHitObjectType OriginalHitObjectType { get; private set; }
    public string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)OriginalHitObjectType},{(int)HitSound},{HitSample.ToFileFormat()}";
    }
    public int Column { get; set; }
    public int BeatmapColumn { get; set; }
}