using osuToolsV2.Beatmaps.HitObjects.Osu;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.HitObjects.Taiko;
using osuToolsV2.Exceptions;

namespace osuToolsV2.Beatmaps.HitObjects.HitObjectParser;

public class TaikoHitObjectCreator : IHitObjectCreator
{
    private static OsuHitObjectCreator _osuHitObjectCreator = new OsuHitObjectCreator();
    public IHitObject CreateHitObject(string[] data, IBeatmap beatmap)
    {
        var realHitObject = _osuHitObjectCreator.CreateHitObject(data, beatmap);
        return CreateTaikoHit(realHitObject);
    }
        
    private static IHitObject CreateTaikoHit(IHitObject realHitObject)
    {
        ITaikoHit taikoHit;
        var oriType = realHitObject.OriginalHitObjectType;
        if (!oriType.HasFlag(OriginalHitObjectType.HitCircle))
        {
            return CreateDrumRoll(realHitObject);
        }

        HitCircle circle = (HitCircle)realHitObject;
        bool isWhistle = (circle.HitSound & HitSound.Whistle) != 0;
        bool isClap = (circle.HitSound & HitSound.Clap) != 0;
        bool isWFinish = (circle.HitSound & HitSound.Finish) != 0;
        if (isWhistle || isClap)
        {
            taikoHit = isWFinish
                ? new TaikoLargeBlueHit(oriType)
                : new TaikoBlueHit(oriType);
        }
        else
        {
            taikoHit = isWFinish
                ? new TaikoLargeRedHit(oriType)
                : new TaikoRedHit(oriType);
        }

        taikoHit.Position = circle.Position;
        taikoHit.HitSample = circle.HitSample;
        taikoHit.HitSound = circle.HitSound;
        taikoHit.StartTime = circle.StartTime;
        return taikoHit;
    }
        
    private static IHitObject CreateDrumRoll(IHitObject realHitObject)
    {
        if (realHitObject is not Slider slider)
        {
            return CreateDenDen(realHitObject);
        }



        return new Drumroll(slider.OriginalHitObjectType)
        {
            StartTime = slider.StartTime,
            Length = slider.Length,
        };
    }

    private static IHitObject CreateDenDen(IHitObject realHitObject)
    {
        if (realHitObject is not Spinner spinner)
        {
            throw new InvalidHitObjectTypeException("Taiko", realHitObject.OriginalHitObjectType);
        }
            
        IHitObject denden = new DenDen(realHitObject.OriginalHitObjectType)
        {
            StartTime = spinner.StartTime,
            EndTime = spinner.EndTime,
        };
        return denden;
    }
}