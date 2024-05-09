using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class ApiV2ConvertBeatmap : IBeatmap
{
    public BeatmapMetadata Metadata { get; set; } = new BeatmapMetadata();
    public DifficultyAttributes DifficultyAttributes { get; set; } = new DifficultyAttributes();

    public List<IHitObject>? HitObjects
    {
        get => null;
        set => throw new NotSupportedException();
    }
    public Ruleset Ruleset { get; set; } = Ruleset.FromLegacyRuleset(LegacyRuleset.Osu);
    public double Bpm { get; set; }
}

