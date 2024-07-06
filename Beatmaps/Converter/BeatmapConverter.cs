using osuToolsV2.Beatmaps.HitObjects;

namespace osuToolsV2.Beatmaps.Converter;

public abstract class BeatmapConverter<THitObject> where THitObject: IHitObject
{
    public abstract bool CanConvert();

    public virtual THitObject ConvertHitObject(IHitObject hitObject)
    {
        return (THitObject) hitObject;
    }
    public virtual Beatmap<THitObject> Convert(Beatmap beatmap)
    {
        Beatmap<THitObject> converted = new Beatmap<THitObject>();
        converted.Metadata = beatmap.Metadata;
        converted.Bookmarks = beatmap.Bookmarks;
        converted.Bpm = beatmap.Bpm;
        converted.Ruleset = beatmap.Ruleset;
        converted.BeatDivisor = beatmap.BeatDivisor;
        converted.BreakTimes = converted.BreakTimes;
        converted.CountdownOffset = converted.CountdownOffset;
        converted.CountdownType = converted.CountdownType;
        converted.DifficultyAttributes = beatmap.DifficultyAttributes;
        converted.DistanceSpacing = beatmap.DistanceSpacing;
        converted.EpilepsyWarning = beatmap.EpilepsyWarning;
        converted.GridSize = beatmap.GridSize;
        converted.HitObjects = 
            new List<IHitObject>(beatmap.HitObjects?.Select(ConvertHitObject).Cast<IHitObject>()
                                 ?? Array.Empty<IHitObject>());
        converted.OverlayPosition = beatmap.OverlayPosition;
        converted.PreviewTime = converted.PreviewTime;
        converted.SampleSet = beatmap.SampleSet;
        converted.SkinPreference = beatmap.SkinPreference;
        converted.SpecialStyle = beatmap.SpecialStyle;
        converted.StackLeniency = beatmap.StackLeniency;
        converted.TimelineZoom = beatmap.TimelineZoom;
        converted.TimingPointCollection = beatmap.TimingPointCollection;
        converted.WidescreenStoryboard = beatmap.WidescreenStoryboard;
        converted.AudioLeadIn = beatmap.AudioLeadIn;
        converted.LetterboxInBreaks = beatmap.LetterboxInBreaks;
        converted.UseSkinSprites = beatmap.UseSkinSprites;
        converted.InlineStoryBoardCommand = beatmap.InlineStoryBoardCommand;
        converted.SamplesMatchPlaybackRate = beatmap.SamplesMatchPlaybackRate;
        return converted;
    }
}