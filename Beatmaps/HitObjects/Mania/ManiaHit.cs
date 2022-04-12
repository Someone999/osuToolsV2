using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public class ManiaHit : IManiaNote
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
    }
    public int Column { get; set; }
    public int BeatmapColumn { get; set; }
}