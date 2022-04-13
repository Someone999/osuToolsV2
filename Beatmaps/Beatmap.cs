using System.Text.RegularExpressions;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Exceptions;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.Beatmaps;

public class Beatmap : IBeatmap
{
    
    enum DataSection
    {
        None,
        General,
        Editor,
        Metadata,
        Difficulty,
        Events,
        TimingPoints,
        Colours,
        HitObjects
    }
    
    public static Regex VersionMatcher { get; } = new Regex("osu file format v(\\d*)");
    public Beatmap(string beatmapFile)
    {
        DataSection currentSection = DataSection.None;
        StringReader reader = new StringReader(File.ReadAllText(beatmapFile));
        string? line = reader.ReadLine();
        if (line == null)
        {
            throw new InvalidBeatmapException();
        }
        Match m = VersionMatcher.Match(line);
        if (!m.Success)
        {
            throw new InvalidBeatmapException();
        }
        Metadata.BeatmapFileVersion = int.Parse(m.Groups[1].Value);
        List<string> inlineStoryboardCommand = new List<string>();
        List<string> hitObjects = new List<string>();
        List<string> timingPoints = new List<string>();
        while (true)
        {
            line = reader.ReadLine();
            if (line == null)
            {
                break;
            }

            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                currentSection = (DataSection)Enum.Parse(typeof(DataSection), line.TrimStart('[').TrimEnd(']'));
                continue;
            }
            string key = "";
            string value = "";
            string[] commaSplit = line.Split(':');
            if (commaSplit.Length > 1)
            {
                key = commaSplit[0];
                value = commaSplit[1];
            }
            
            switch (currentSection)
            {
                case DataSection.Events:
                    inlineStoryboardCommand.Add(line);
                    break;
                case DataSection.HitObjects:
                    hitObjects.Add(line);
                    break;
                case DataSection.TimingPoints:
                    timingPoints.Add(line);
                    break;
                case DataSection.Colours:
                    break;
                case DataSection.General:
                    
                    switch (key)
                    {
                        case "AudioFilename" :
                            Metadata.AudioFileName = value;
                            break;
                        case "AudioLeadIn" :
                            AudioLeadIn = double.Parse(value);
                            break;
                        case "PreviewTime":
                            PreviewTime = TimeSpan.FromMilliseconds(double.Parse(value));
                            break;
                        case "Countdown":
                            CountdownType = (CountdownType)Enum.Parse(typeof(CountdownType), value);
                            break;
                        case "SampleSet":
                            SampleSet = (SampleSet)Enum.Parse(typeof(SampleSet), value);
                            break;
                        case "StackLeniency":
                            StackLeniency = double.Parse(value);
                            break;
                        case "Mode":
                            LegacyRuleset legacyRuleset = (LegacyRuleset)int.Parse(value);
                            Ruleset = Ruleset.FromLegacyRuleset(legacyRuleset);
                            break;
                        case "LetterboxInBreaks":
                            int rslt0 = int.Parse(value);
                            LetterboxInBreaks = rslt0 != 0;
                            break;
                        case "UseSkinSprites":
                            int rslt1 = int.Parse(value);
                            UseSkinSprites = rslt1 != 0;
                            break;
                        case "OverlayPosition":
                            OverlayPosition = (OverlayPosition)Enum.Parse(typeof(OverlayPosition), value);
                            break;
                        case "SkinPreference" :
                            SkinPreference = value;
                            break;
                        case "EpilepsyWarning":
                            int rslt2 = int.Parse(value);
                            EpilepsyWarning = rslt2 != 0;
                            break;
                        case "CountdownOffset":
                            CountdownOffset = double.Parse(value);
                            break;
                        case "SpecialStyle":
                            SpecialStyle = (SpecialStyle)Enum.Parse(typeof(SpecialStyle), value);
                            break;
                        case "WidescreenStoryboard":
                            int rslt3 = int.Parse(value);
                            WidescreenStoryboard = rslt3 != 0;
                            break;
                        case "SamplesMatchPlaybackRate":
                            int rslt4 = int.Parse(value);
                            SamplesMatchPlaybackRate = rslt4 != 0;
                            break;
                    }
                    break;
                case DataSection.Editor:
                    switch (key)
                    {
                        case "Bookmarks":
                            if (string.IsNullOrEmpty(value))
                            {
                                break;
                            }
                            Bookmarks.AddRange(from strVal in value.Split(',') select int.Parse(strVal));
                            break;
                        case "DistanceSpacing":
                            DistanceSpacing = double.Parse(value);
                            break;
                        case "BeatDivisor":
                            BeatDivisor = double.Parse(value);
                            break;
                        case "GridSize":
                            GridSize = double.Parse(value);
                            break;
                        case "TimelineZoom" :
                            TimelineZoom = double.Parse(value);
                            break;
                    }
                    break;
                case DataSection.Metadata:
                    switch (key)
                    {
                        case "Artist":
                            Metadata.Artist = value;
                            break;
                        case "ArtistUnicode":
                            Metadata.ArtistUnicode = value;
                            break;
                        case "Title":
                            Metadata.Title = value;
                            break;
                        case "TitleUnicode":
                            Metadata.TitleUnicode = value;
                            break;
                        case "Creator":
                            Metadata.Creator = value;
                            break;
                        case "Version":
                            Metadata.Version = value;
                            break;
                        case "Source":
                            Metadata.Source = value;
                            break;
                        case "Tags":
                            Metadata.Tags = value;
                            break;
                        case "BeatmapID":
                            BeatmapId = int.Parse(value);
                            break;
                        case "BeatmapSetID":
                            BeatmapSetId = int.Parse(value);
                            break;
                    }
                    break;
                
                case DataSection.Difficulty:
                    switch (key)
                    {
                        case "HPDrainRate":
                            DifficultyAttributes.HpDrain = double.Parse(value);
                            break;
                        case "CircleSize":
                            DifficultyAttributes.CircleSize = double.Parse(value);
                            break;
                        case "OverallDifficulty":
                            DifficultyAttributes.OverallDifficulty = double.Parse(value);
                            break;
                        case "ApproachRate":
                            DifficultyAttributes.ApproachRate = double.Parse(value);
                            break;
                        case "SliderMultiplier":
                            DifficultyAttributes.SliderMultiplier = double.Parse(value);
                            break;
                        case "SliderTickRate":
                            DifficultyAttributes.SliderTickRate = double.Parse(value);
                            break;
                    }
                    break;
                case DataSection.None:
                default: break;
            }
            
        }
        HitObjects = BeatmapTools.HitObjectProcessor(hitObjects, this);
        TimingPoints = BeatmapTools.TimingPointProcessor(timingPoints, this);
        InlineStoryBoardCommand = BeatmapTools.StoryCommandProcessor(inlineStoryboardCommand, this);
    }
    public List<TimingPoint> TimingPoints { get; set; }
    public List<BreakTime> BreakTimes { get; } = new();
    public StoryBoardCommandBase[]? InlineStoryBoardCommand { get; private set; }
    public BeatmapMetadata Metadata { get; set; } = new BeatmapMetadata();
    public DifficultyAttributes DifficultyAttributes { get; set; } = new DifficultyAttributes();
    public int? BeatmapId { get; set; }
    public int? BeatmapSetId { get; set; }
    public double? Stars { get; set; }
    public List<IHitObject>? HitObjects { get; set; }
    public Ruleset Ruleset { get; set; } = new EmptyRuleset();
    public double Bpm { get; set; }
    public CountdownType CountdownType { get; set; }
    public double StackLeniency { get; set; }
    public double AudioLeadIn { get; set; }
    public TimeSpan PreviewTime { get; set; }
    public SampleSet SampleSet { get; set; }
    public bool LetterboxInBreaks { get; set; } = false;
    public SpecialStyle SpecialStyle { get; set; } = SpecialStyle.None;
    public bool EpilepsyWarning { get; set; } = false;
    public double CountdownOffset { get; set; } = 0;
    public bool UseSkinSprites { get; set; }
    public string? SkinPreference { get; set; }
    public OverlayPosition OverlayPosition { get; set; }
    public bool WidescreenStoryboard { get; set; }
    public bool SamplesMatchPlaybackRate { get; set; }
    public List<int> Bookmarks { get; set; } = new();
    public double DistanceSpacing { get; set; }
    public double BeatDivisor { get; set; } = 4;
    public double GridSize { get; set; } = 32;
    public double TimelineZoom { get; set; } = 1;
    public bool HasVideo { get; internal set; }
    
}