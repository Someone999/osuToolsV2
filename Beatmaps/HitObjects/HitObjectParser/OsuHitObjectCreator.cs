using System.Text.RegularExpressions;
using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Exceptions;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects.HitObjectParser;

public class OsuHitObjectCreator : IHitObjectCreator
{
    public IHitObject CreateHitObject(string[] data, IBeatmap beatmap)
    {
        string xStr = data[0], yStr = data[1],startTimeStr = data[2], typeStr = data[3], hitSoundStr = data[4];
        IHitObject? hitObject;
        string objType = new string(Convert.ToString(int.Parse(typeStr), 2).Reverse().ToArray());
        OriginalHitObjectType oriHitObjType = 0;
        for (int i = 0; i < objType.Length; i++)
        {
            if (objType[i] == '0')
            {
                continue;
            }
            oriHitObjType |= (OriginalHitObjectType)(1 << i);
        }

        if (oriHitObjType.HasFlag(OriginalHitObjectType.HitCircle))
        {
            hitObject = new HitCircle(oriHitObjType);

            if (data.Length > 5)
            {
                hitObject.HitSample = HitSample.Parse(data[5]);
            }
        }
        else if (oriHitObjType.HasFlag(OriginalHitObjectType.Slider))
        {
            hitObject = new Slider(oriHitObjType);
            Slider slider = (Slider)hitObject;
            string[] sliderBasicInfo = data[5].Split('|');
            slider.CurveType = sliderBasicInfo[0] switch
            {
                "B" => Slider.CurveTypes.Bezire,
                "C" => Slider.CurveTypes.Catmull,
                "L" => Slider.CurveTypes.Linear,
                "P" => Slider.CurveTypes.PerfectCircle,
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
                slider.CurvePoints.Add(pixel);
            }
            
            if (slider.CurvePoints.Count == 0)
            {
                throw new InvalidBeatmapException("Slider must have at least one curve point.");
            }
            slider.SlideTimes = int.Parse(data[6]);
            slider.Length = double.Parse(data[7]);
                    
            // EdgeSounds are ignored.
            if (data.Length <= 8)
            {
                return slider;
            }
            Regex hitSampleMatcher = new Regex(@"\d+:\d+:\d+:(?:.*?)?"); 
            foreach (var s in data)
            {
                if (hitSampleMatcher.IsMatch(s))
                {
                    slider.HitSample = HitSample.Parse(s);
                }
            }
        }
        else if (oriHitObjType.HasFlag(OriginalHitObjectType.Spinner))
        {
            hitObject = new Spinner(oriHitObjType);
            Spinner spinner = (Spinner)hitObject;
            spinner.EndTime = double.Parse(data[5]);
            if (data.Length > 6)
            {
                spinner.HitSample = HitSample.Parse(data[6]);
            }
        }
        else
        {
            throw new InvalidHitObjectTypeException("Osu", oriHitObjType);
        }
        
        hitObject.Position = new OsuPixel(double.Parse(xStr), double.Parse(yStr));
        hitObject.StartTime = double.Parse(startTimeStr);
        hitObject.HitSound = (HitSound)Enum.Parse(typeof(HitSound),hitSoundStr);
        return hitObject;
    }
}