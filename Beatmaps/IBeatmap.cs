﻿using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Beatmaps;

public interface IBeatmap
{
    BeatmapMetadata Metadata { get; set; }
    DifficultyAttributes DifficultyAttributes { get; set; }
    int? BeatmapId { get; set; }
    int? BeatmapSetId { get; set; }
    double? Stars { get; set; }
    List<IHitObject>? HitObjects { get; set; }
    Ruleset Ruleset { get; set; }
    public double Bpm { get; set; }
}