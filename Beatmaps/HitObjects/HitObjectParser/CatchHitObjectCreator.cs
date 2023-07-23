using osuToolsV2.Beatmaps.HitObjects.Catch;
using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Exceptions;

namespace osuToolsV2.Beatmaps.HitObjects.HitObjectParser;

public class CatchHitObjectCreator : IHitObjectCreator
{
    private static OsuHitObjectCreator _osuHitObjectCreator = new OsuHitObjectCreator();
    public IHitObject CreateHitObject(string[] data, IBeatmap beatmap)
    {
        IHitObject realHitObject = _osuHitObjectCreator.CreateHitObject(data, beatmap);
        if (realHitObject.OriginalHitObjectType.HasFlag(OriginalHitObjectType.HitCircle))
        {
            var hitCircle = realHitObject;
            Fruit fruit = new Fruit(realHitObject.OriginalHitObjectType)
            {
                Position = hitCircle.Position,
                StartTime = hitCircle.StartTime,
                HitSound = hitCircle.HitSound,
                HitSample = hitCircle.HitSample
            };

            return fruit;
        }
            
        if(realHitObject.OriginalHitObjectType.HasFlag(OriginalHitObjectType.Slider))
        {
            var slider = (Slider)realHitObject;
            JuiceStream juiceStream = new JuiceStream(slider.OriginalHitObjectType)
            {
                Position = slider.Position,
                StartTime = slider.StartTime,
                HitSound = slider.HitSound,
                HitSample = slider.HitSample,
                CurvePoints = slider.CurvePoints,
                CurveType = slider.CurveType,
                Lenght = slider.Length,
                SlideTimes = slider.SlideTimes
            };
                
            return juiceStream;
        }

        if (realHitObject.OriginalHitObjectType.HasFlag(OriginalHitObjectType.Spinner))
        {
            Spinner spinner = (Spinner)realHitObject;
            Banana banana = new Banana(realHitObject.OriginalHitObjectType)
            {
                Position = spinner.Position,
                StartTime = spinner.StartTime,
                HitSound = spinner.HitSound,
                HitSample = spinner.HitSample,
                EndTime = spinner.EndTime
            };
            return banana;
        }

        throw new InvalidHitObjectTypeException("Catch", realHitObject.OriginalHitObjectType);
    }
}