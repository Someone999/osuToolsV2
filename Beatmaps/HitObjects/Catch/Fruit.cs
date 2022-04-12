using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class Fruit : IHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.Fruit;
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
    }
}