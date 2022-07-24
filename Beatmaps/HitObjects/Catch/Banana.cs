using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class Banana : IHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.Banana;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public double EndTime { get; set; }
    public void Parse(string[] data)
    {
        Spinner spinner = new Spinner();
        spinner.Parse(data);
        Position = spinner.Position;
        StartTime = spinner.StartTime;
        HitSound = spinner.HitSound;
        HitSample = spinner.HitSample;
        EndTime = spinner.EndTime;
        OriginalHitObjectType = spinner.OriginalHitObjectType;
    }
    public OriginalHitObjectType OriginalHitObjectType { get; private set; }
    public string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)HitSound},{EndTime},{HitSample.ToFileFormat()}";
    }
}