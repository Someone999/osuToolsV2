using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Catch;

public class JuiceStream : IHitObject
{


    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.JuiceStream;
    public HitSound HitSound { get; set; }
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public List<OsuPixel> CurvePoints { get; internal set; } = new List<OsuPixel>();
    public Slider.CurveTypes CurveType { get; internal set; }
    public double Lenght { get; set; } 
    public int SlideTimes { get; set; }
    
    public void Parse(string[] data)
    {
        Slider slider = new Slider();
        slider.Parse(data);
        Position = slider.Position;
        StartTime = slider.StartTime;
        HitSound = slider.HitSound;
        HitSample = slider.HitSample;
        CurvePoints = slider.CurvePoints;
        CurveType = slider.CurveType;
        Lenght = slider.Length;
        SlideTimes = slider.SlideTimes;
        OriginalHitObjectType = slider.OriginalHitObjectType;
    }
    public OriginalHitObjectType OriginalHitObjectType { get; private set; }
    public string ToFileFormat()
    {
        throw new NotSupportedException();
    }
}