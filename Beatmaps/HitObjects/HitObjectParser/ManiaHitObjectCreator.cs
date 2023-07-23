using osuToolsV2.Beatmaps.HitObjects.Mania;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Exceptions;

namespace osuToolsV2.Beatmaps.HitObjects.HitObjectParser;

public class ManiaHitObjectCreator : IHitObjectCreator
{
    private static ConvertHitObjectCreator _convertHitObjectCreator = new ConvertHitObjectCreator();
    private static OsuHitObjectCreator _osuHitObjectCreator = new OsuHitObjectCreator();
    public IHitObject CreateHitObject(string[] data, IBeatmap beatmap)
    {
        IHitObject realHitObject = _convertHitObjectCreator.CreateHitObject(data, beatmap);
        var beatmapColumn = beatmap.DifficultyAttributes.CircleSize;
        if (realHitObject.OriginalHitObjectType.HasFlag(OriginalHitObjectType.HitCircle))
        {
            realHitObject = _osuHitObjectCreator.CreateHitObject(data, beatmap);
            ManiaHit maniaHit = new ManiaHit(realHitObject.OriginalHitObjectType)
            {
                HitSound = realHitObject.HitSound,
                HitSample = realHitObject.HitSample,
                StartTime = realHitObject.StartTime,
                Position = realHitObject.Position,
                Column = (int) Math.Floor(realHitObject.Position.X * beatmapColumn / 512d)
            };

            return maniaHit;
        }


        if (!realHitObject.OriginalHitObjectType.HasFlag(OriginalHitObjectType.ManiaHold))
        {
            throw new InvalidHitObjectTypeException("Mania", realHitObject.OriginalHitObjectType);
        }
        
        
        
        ManiaHold maniaHold = new ManiaHold(realHitObject.OriginalHitObjectType)
        {
            HitSound = realHitObject.HitSound,
            HitSample = realHitObject.HitSample,
            StartTime = realHitObject.StartTime,
            Position = realHitObject.Position
        };
            
        int colonIdx = data[5].IndexOf(':');
        string endTimeStr = data[5].Substring(0, colonIdx);
        string hitSampleStr = data[5].Substring(colonIdx + 1);
        maniaHold.EndTime = double.Parse(endTimeStr);
        maniaHold.HitSample = HitSample.Parse(hitSampleStr);
        maniaHold.Column = (int) Math.Floor(maniaHold.Position.X * beatmapColumn / 512d);
        return maniaHold;

    }
}