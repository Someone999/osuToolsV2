using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class HitCircle : IHitObject
{

    public OsuPixel Position { get; set; } = new OsuPixel();
    public double StartTime { get; set; } = 0;
    public HitObjectType HitObjectType => HitObjectType.HitCircle;
    public HitSound HitSound { get; set; } = HitSound.Normal;
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public void Parse(string[] data)
    {
       HitObjectTools.GenericParse(this, data, out var oriType);
       if ((oriType & OriginalHitObjectType.HitCircle) == 0)
       {
           Position = OsuPixel.Empty;
           StartTime = 0;
           HitSound = HitSound.Normal;
           throw new InvalidOperationException($"Can not process type {oriType}");
       }
       
       if (data.Length > 5)
       {
           HitSample = HitSample.Parse(data[5]);
       }
       
    }

}