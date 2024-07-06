using osuToolsV2.Beatmaps.BeatmapReader;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.LazyLoaders;
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

    internal HitObjectLazyLoader? HitObjectLazyLoader { get; set; }
    internal StoryBoardCommandLazyLoader? InlineStoryBoardCommandLazyLoader { get; set; }
    internal StoryBoardCommandLazyLoader? StoryBoardCommandLazyLoader { get; set; }
    internal TimingPointLazyLoader? TimingPointLazyLoader { get; set; }
    private TimingPointCollection? _timingPointCollection;

    public TimingPointCollection TimingPointCollection
    {
        get
        {
            if (_timingPointCollection != null)
            {
                return _timingPointCollection;
            }

            if (TimingPointLazyLoader == null)
            {
                throw new InvalidOperationException("Lazy loader not initialized properly");
            }

            _timingPointCollection = TimingPointLazyLoader.LoadObject();
            return _timingPointCollection;
        }
        internal set => _timingPointCollection = value;
    }

    internal BreakTimeLazyLoader? BreakTimeLazyLoader { get; set; }
    internal BreakTimeCollection? BreakTimeCollection;
    public BreakTimeCollection? BreakTimes
    {
        get
        {
            if (BreakTimeCollection != null)
            {
                return BreakTimeCollection;
            }

            if (BreakTimeLazyLoader == null)
            {
                throw new InvalidOperationException("Lazy loader not initialized properly");
            }

            if (BreakTimeLazyLoader.Loaded)
            {
                return BreakTimeCollection;
            }
            
            BreakTimeCollection = BreakTimeLazyLoader.LoadObject();
            return BreakTimeCollection;
        }
        internal set => BreakTimeCollection = value;
    }

    private StoryBoardCommandBase[]? _inlineStoryBoardCommandBases;
    private StoryBoardCommandBase[]? _storyBoardCommandBases;
    public StoryBoardCommandBase[]? StoryBoardCommand
    {
        get
        {
            if (_storyBoardCommandBases != null)
            {
                return _storyBoardCommandBases;
            }

            if (StoryBoardCommandLazyLoader == null)
            {
                throw new InvalidOperationException("Lazy loader not initialized properly");
            }

            if (StoryBoardCommandLazyLoader.Loaded)
            {
                return _storyBoardCommandBases;
            }
            
            _storyBoardCommandBases = StoryBoardCommandLazyLoader.LoadObject();
            return _storyBoardCommandBases;
        }
        internal set => _storyBoardCommandBases = value;
    }

    public StoryBoardCommandBase[]? InlineStoryBoardCommand
    {
        get
        {
            if (_inlineStoryBoardCommandBases != null)
            {
                return _inlineStoryBoardCommandBases;
            }

            if (InlineStoryBoardCommandLazyLoader == null)
            {
                throw new InvalidOperationException("Lazy loader not initialized properly");
            }

            if (InlineStoryBoardCommandLazyLoader.Loaded)
            {
                return _inlineStoryBoardCommandBases;
            }
            
            _inlineStoryBoardCommandBases = InlineStoryBoardCommandLazyLoader.LoadObject();
            return _inlineStoryBoardCommandBases;
        }
        internal set => _inlineStoryBoardCommandBases = value;
    }
    public BeatmapMetadata Metadata { get; set; } = new();
    public DifficultyAttributes DifficultyAttributes { get; set; } = new DifficultyAttributes();

    private List<IHitObject>? _hitObjects;
    public List<IHitObject>? HitObjects 
    {
        get
        {
            if (_hitObjects != null)
            {
                return _hitObjects;
            }
            
            if (HitObjectLazyLoader == null)
            {
                throw new InvalidOperationException("Lazy loader not initialized properly");
            }

            _hitObjects = HitObjectLazyLoader.LoadObject();
            return _hitObjects;
        }

        set => _hitObjects = value;
    }
    
    public Ruleset Ruleset { get; set; } = new EmptyRuleset();

    private double? _internalBpm;

    public double Bpm
    {
        get
        {
            _internalBpm ??= TimingPointCollection.GetMostCommonBpm();
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
    public static Beatmap? FromFile(string path) => new DefaultFileBeatmapReader(path).Read();
}