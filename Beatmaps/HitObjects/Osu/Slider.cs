using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.Osu;

public class Slider : IHitObject
{
    public enum CurveTypes
    {
        Bezire,
        Catmull,
        Linear,
        PerfectCircle
    }
    public OsuPixel Position { get; set; }
    public double StartTime { get; set; }
    public HitObjectType HitObjectType => HitObjectType.Slider;
    public HitSound HitSound { get; set; } = HitSound.Normal;
    public HitSample HitSample { get; set; } = HitSample.Empty;
    public int SlideTimes { get; set; }
    public double Length { get; set; }
    public List<OsuPixel> CurvePoints { get; internal set; } = new List<OsuPixel>();
    public CurveTypes CurveType { get; set; }
    
    public void Parse(string[] data)
    {
        HitObjectTools.GenericParse(this, data, out var oriType);
        if ((oriType & OriginalHitObjectType.Slider) == 0)
        {
            Position = OsuPixel.Empty;
            StartTime = 0;
            HitSound = HitSound.Normal;
            throw new InvalidOperationException($"Can not process type {oriType}");
        }

        string[] sliderBasicInfo = data[5].Split('|');
        CurveType = sliderBasicInfo[0] switch
        {
            "B" => CurveTypes.Bezire,
            "C" => CurveTypes.Catmull,
            "L" => CurveTypes.Linear,
            "P" => CurveTypes.PerfectCircle,
            _ => throw new InvalidOperationException("Unknown curve type.")
        };

        string[] points = sliderBasicInfo.Skip(1).ToArray();
        foreach (var point in points)
        {
            string[] coordinate = point.Split(':');
            if (coordinate.Length != 2)
            {
                continue;
            }
            var pixel = new OsuPixel(double.Parse(coordinate[0]), double.Parse(coordinate[1]));
            CurvePoints.Add(pixel);
        }
        SlideTimes = int.Parse(data[6]);
        Length = int.Parse(data[7]);
        
       // EdgeSounds are ignored.
       if (data.Length <= 8)
       {
           return;
       }
       foreach (var s in data)
       {
           if (!s.Contains('|'))
           {
               HitSample = HitSample.Parse(s);
           }
       }
    }
}