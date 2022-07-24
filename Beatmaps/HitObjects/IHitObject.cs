using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects;

public interface IHitObject
{ 
    OsuPixel Position { get; set; }
    double StartTime { get; set;}
    HitObjectType HitObjectType { get; }
    HitSound HitSound { get; set;}
    HitSample HitSample { get; set;}
    void Parse(string[] data);
    OriginalHitObjectType OriginalHitObjectType { get; }
    string ToFileFormat();
}