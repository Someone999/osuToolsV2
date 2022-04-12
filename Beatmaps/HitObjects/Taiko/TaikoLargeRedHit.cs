using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Taiko;

public class TaikoLargeRedHit : ITaikoHit
{

    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.TaikoLargeRedHit;
    public HitSound HitSound { get; set; } = HitSound.Normal;
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public void Parse(string[] data)
    {
        throw new NotSupportedException();
    }
}