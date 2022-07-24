using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class Spinner : IHitObject
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.Spinner;
    public HitSound HitSound { get; set; } = HitSound.Normal;
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public double EndTime { get; set; }
    public void Parse(string[] data)
    {
        HitObjectTools.GenericParse(this, data,out var oriType);
        if ((oriType & OriginalHitObjectType.Spinner) == 0)
        {
            Position = OsuPixel.Empty;
            StartTime = 0;
            HitSound = HitSound.Normal;
            throw new InvalidOperationException($"Can not process type {oriType}");
        }
        OriginalHitObjectType = oriType;
        EndTime = double.Parse(data[5]);
        if (data.Length > 6)
        {
            HitSample = HitSample.Parse(data[6]);
        }

    }
    public OriginalHitObjectType OriginalHitObjectType { get; private set; }
    public string ToFileFormat()
    {
        return $"{Position.X},{Position.Y},{StartTime},{(int)HitSound},{EndTime},{HitSample.ToFileFormat()}";
    }
}