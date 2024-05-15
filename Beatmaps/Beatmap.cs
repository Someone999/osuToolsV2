using osuToolsV2.Beatmaps.BeatmapReader;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Rulesets;
using osuToolsV2.Score;
using osuToolsV2.StoryBoard.Commands;

namespace osuToolsV2.Beatmaps;

public class Beatmap<THitObject> : IBeatmap<THitObject> where THitObject: IHitObject
{
    public IEnumerable<THitObject> GetAs()
    {
        if (HitObjects == null)
        {
            yield break;
        }
        
        foreach (var hitObject in HitObjects)
        {
            yield return (THitObject) hitObject;
        }
    }

    public override string ToString()
    {
        return $"{Metadata.Artist} - {Metadata.Title} [{Metadata.Version}]";
    }
    public List<TimingPoint> TimingPoints { get; set; } = new();
    public List<BreakTime> BreakTimes { get; internal set; } = new();
    public StoryBoardCommandBase[]? InlineStoryBoardCommand { get; internal set; }
    public BeatmapMetadata Metadata { get; set; } = new();
    public DifficultyAttributes DifficultyAttributes { get; set; } = new DifficultyAttributes();

    public List<IHitObject>? HitObjects { get; set; }
    
    public Ruleset Ruleset { get; set; } = new EmptyRuleset();
    private double GetMostCommonBpm()
    {
        Dictionary<double, int> timingPointsTimes = new Dictionary<double, int>();
        var unInheritedTimingPoints = TimingPoints.Where(p => !p.Inherited).ToArray();
        if (unInheritedTimingPoints.Length == 1)
        {
            return unInheritedTimingPoints[0].Bpm;
        }
        foreach (var timingPoint in unInheritedTimingPoints)
        {
            double roundedBpm = Math.Round(timingPoint.Bpm, 2);
            if (!timingPointsTimes.TryAdd(roundedBpm, 1))
            {
                timingPointsTimes[roundedBpm]++;
            }
        }
        var orderedBpmList = from kv in timingPointsTimes orderby kv.Value descending select kv;
        double mostCommonBpm = orderedBpmList.FirstOrDefault().Key;
        return mostCommonBpm;
    }

    private double? _internalBpm;

    public double Bpm
    {
        get
        {
            _internalBpm ??= GetMostCommonBpm();
            return _internalBpm.Value;
        }
        set => _internalBpm = value;
    }

    public virtual int GetHitObjectCount(ScoreInfo scoreInfo)
    {
        return HitObjects?.Count ?? 0;
    }

    public virtual int GetMaxCombo(ScoreInfo scoreInfo)
    {
        return GetHitObjectCount(scoreInfo);
    }

    public CountdownType CountdownType { get; set; }
    public double StackLeniency { get; set; }
    public double AudioLeadIn { get; set; }
    public TimeSpan PreviewTime { get; set; }
    public SampleSet SampleSet { get; set; }
    public bool? LetterboxInBreaks { get; set; } = false;
    public SpecialStyle SpecialStyle { get; set; } = SpecialStyle.None;
    public bool? EpilepsyWarning { get; set; } = false;
    public double? CountdownOffset { get; set; }
    public bool? UseSkinSprites { get; set; }
    public string? SkinPreference { get; set; }
    public OverlayPosition? OverlayPosition { get; set; }
    public bool WidescreenStoryboard { get; set; }
    public bool? SamplesMatchPlaybackRate { get; set; }
    public List<int> Bookmarks { get; set; } = new();
    public double DistanceSpacing { get; set; }
    public double BeatDivisor { get; set; } = 4;
    public double GridSize { get; set; } = 32;
    public double TimelineZoom { get; set; } = 1;
}

public class Beatmap : Beatmap<IHitObject>
{
    public static Beatmap FromFile(string path) => new DefaultFileBeatmapReader(path).Read();
}