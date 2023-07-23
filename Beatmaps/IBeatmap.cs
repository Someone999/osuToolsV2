using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Beatmaps;

public interface IBeatmap
{
    BeatmapMetadata Metadata { get; set; }
    DifficultyAttributes DifficultyAttributes { get; set; }
    List<IHitObject>? HitObjects { get; set; }
    Ruleset Ruleset { get; set; }
    double Bpm { get; set; }
}

public interface IBeatmap<THitObject> : IBeatmap where THitObject: IHitObject
{
    new List<THitObject>? HitObjects { get; set; }
}