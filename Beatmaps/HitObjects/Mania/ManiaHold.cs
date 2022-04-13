using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public class ManiaHold : IManiaHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.ManiaHold;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public double EndTime { get; set; }
    public void Parse(string[] data)
    {
        HitObjectTools.GenericParse(this, data, out var oriType);
        int colonIdx = data[5].IndexOf(':');
        string endTimeStr = data[5].Substring(0, colonIdx);
        string hitSampleStr = data[5].Substring(colonIdx + 1);
        EndTime = double.Parse(endTimeStr);
        HitSample = HitSample.Parse(hitSampleStr);
        Column = (int) Math.Floor(Position.X * BeatmapColumn / 512d);
    }
    public int Column { get; set; }
    public int BeatmapColumn { get; set; }
}