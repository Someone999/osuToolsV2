using System.Security.Cryptography;
using System.Text.RegularExpressions;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.HitObjectParser;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Exceptions;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.Beatmaps.BeatmapReader;

public class DefaultBeatmapDecoder : IBeatmapDecoder
{
    private static MD5 _md5Clac = MD5.Create();
    static readonly Regex VersionMatcher = new Regex("osu file format v(\\d*)");
    private Beatmap _beatmap = new Beatmap();
    private string _beatmapFile;

    public DefaultBeatmapDecoder(string filePath)
    {
        _beatmapFile = filePath;
    }

    static StoryBoardCommandBase[] StoryBoardCommandProcessor(List<string> lines, Beatmap b)
    {
        List<string> realStoryCommand = new List<string>();
        foreach (var str in lines)
        {
            if (string.IsNullOrEmpty(str))
            {
                continue;
            }
            string[] splitData = str.Split(',');
            if (splitData.Length == 0)
            {
                continue;
            }
            if (string.IsNullOrEmpty(str) || str.StartsWith("//"))
            {
                continue;
            }

            var parseSuccess = Enum.TryParse<StoryBoardEventType>(splitData[0], out var val);


            StoryBoardEventType type = parseSuccess ? val : StoryBoardEventType.Unknown;
            switch (type)
            {
                case StoryBoardEventType.Background:
                    b.Metadata.BackgroundInfo.FileName = splitData[2].Trim('"');
                    if (splitData.Length < 4)
                    {
                        break;
                    }
                    b.Metadata.BackgroundInfo.X = double.Parse(splitData[3]);
                    
                    if (splitData.Length < 5)
                    {
                        break;
                    }
                    b.Metadata.BackgroundInfo.Y = double.Parse(splitData[4]);
                    break;
                case StoryBoardEventType.Video:
                    b.Metadata.VideoInfo.StartTime = double.Parse(splitData[1]);
                    b.Metadata.VideoInfo.FileName = splitData[2].Trim('"');
                    if (splitData.Length < 4)
                    {
                        break;
                    }
                    b.Metadata.VideoInfo.X = double.Parse(splitData[3]);
                    
                    if (splitData.Length < 5)
                    {
                        break;
                    }
                    b.Metadata.VideoInfo.Y = double.Parse(splitData[4]);
                    break;
                case StoryBoardEventType.BreakTime:
                    b.BreakTimes.Add(new BreakTime(double.Parse(splitData[1]), double.Parse(splitData[2])));
                    break;
                default: realStoryCommand.Add(str);
                    break;
            }
        }
        StoryBoardCommandParser parser = new StoryBoardCommandParser(realStoryCommand.ToArray());
        return parser.Parse();
    }

    static List<IHitObject> HitObjectProcessor(List<string> lines, IBeatmap b)
    {
        List<IHitObject> hitObjects = new List<IHitObject>();
        foreach (var line in lines)
        {
            IHitObjectCreator hitObjectCreator = b.Ruleset.LegacyRuleset switch
            {
                LegacyRuleset.Osu => new OsuHitObjectCreator(),
                LegacyRuleset.Taiko => new TaikoHitObjectCreator(),
                LegacyRuleset.Catch => new CatchHitObjectCreator(),
                LegacyRuleset.Mania => new ManiaHitObjectCreator(),
                LegacyRuleset.None => throw new InvalidBeatmapException(),
                _ => throw new InvalidBeatmapException()
            };
            
            if (hitObjectCreator == null)
            {
                throw new InvalidBeatmapException();
            }
            
            hitObjects.Add(hitObjectCreator.CreateHitObject(line.Split(','), b));
        }
        
        return hitObjects;
    }

    static List<TimingPoint> TimingPointProcessor(List<string> lines, IBeatmap beatmap)
    {
        List<TimingPoint> timingPoints = new List<TimingPoint>();
        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }
            timingPoints.Add(new TimingPoint(line));
        }
        return timingPoints;
    }
    
    
    public Beatmap Decode(StringReader? stringReader = null)
    {
        DataSection currentSection = DataSection.None;
        StringReader reader = stringReader ?? new StringReader(File.ReadAllText(_beatmapFile));
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
        _beatmap.Metadata.BeatmapFileVersion = int.Parse(m.Groups[1].Value);
        _beatmap.Metadata.Md5Hash = _md5Clac.ComputeHash(File.ReadAllBytes(_beatmapFile)).GetMd5String();
        string beatmapFileName = Path.GetFileNameWithoutExtension(_beatmapFile);
        string beatmapDirectory = Path.GetDirectoryName(_beatmapFile) ?? "./";
        List<string> storyboardCommand = new List<string>();
        List<string> hitObjects = new List<string>();
        List<string> timingPoints = new List<string>();
        string storyBoardFile = Path.Combine(beatmapDirectory, beatmapFileName + ".osb");
        
        if (File.Exists(storyBoardFile))
        {
            storyboardCommand.AddRange(File.ReadAllLines(storyBoardFile));
        }
        
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
            

            int colonIndex = line.IndexOf(":", StringComparison.Ordinal);
            string key = "";
            string value = "";
            if (colonIndex != -1)
            {
                key = line.Substring(0, colonIndex);
                value = line.Substring(colonIndex + 1);
            }
            _beatmap.Metadata.BeatmapFileName = Path.GetFileName(_beatmapFile);
            _beatmap.Metadata.BeatmapFullPath = _beatmapFile;
            switch (currentSection)
            {
                case DataSection.Events:
                    storyboardCommand.Add(line);
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
                            _beatmap.Metadata.AudioFileName = value;
                            break;
                        case "AudioLeadIn" :
                            _beatmap.AudioLeadIn = double.Parse(value);
                            break;
                        case "PreviewTime":
                            _beatmap.PreviewTime = TimeSpan.FromMilliseconds(double.Parse(value));
                            break;
                        case "Countdown":
                            _beatmap.CountdownType = (CountdownType)Enum.Parse(typeof(CountdownType), value);
                            break;
                        case "SampleSet":
                            _beatmap.SampleSet = (SampleSet)Enum.Parse(typeof(SampleSet), value);
                            break;
                        case "StackLeniency":
                            _beatmap.StackLeniency = double.Parse(value);
                            break;
                        case "Mode":
                            LegacyRuleset legacyRuleset = (LegacyRuleset)int.Parse(value);
                            _beatmap.Ruleset = Ruleset.FromLegacyRuleset(legacyRuleset);
                            break;
                        case "LetterboxInBreaks":
                            int rslt0 = int.Parse(value);
                            _beatmap.LetterboxInBreaks = rslt0 != 0;
                            break;
                        case "UseSkinSprites":
                            int rslt1 = int.Parse(value);
                            _beatmap.UseSkinSprites = rslt1 != 0;
                            break;
                        case "OverlayPosition":
                            _beatmap.OverlayPosition = (OverlayPosition)Enum.Parse(typeof(OverlayPosition), value);
                            break;
                        case "SkinPreference" :
                            _beatmap.SkinPreference = value;
                            break;
                        case "EpilepsyWarning":
                            int rslt2 = int.Parse(value);
                            _beatmap.EpilepsyWarning = rslt2 != 0;
                            break;
                        case "CountdownOffset":
                            _beatmap.CountdownOffset = double.Parse(value);
                            break;
                        case "SpecialStyle":
                            _beatmap.SpecialStyle = (SpecialStyle)Enum.Parse(typeof(SpecialStyle), value);
                            break;
                        case "WidescreenStoryboard":
                            int rslt3 = int.Parse(value);
                            _beatmap.WidescreenStoryboard = rslt3 != 0;
                            break;
                        case "SamplesMatchPlaybackRate":
                            int rslt4 = int.Parse(value);
                            _beatmap.SamplesMatchPlaybackRate = rslt4 != 0;
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
                            _beatmap.Bookmarks.AddRange(from strVal in value.Split(',') select int.Parse(strVal));
                            break;
                        case "DistanceSpacing":
                            _beatmap.DistanceSpacing = double.Parse(value);
                            break;
                        case "BeatDivisor":
                            _beatmap.BeatDivisor = double.Parse(value);
                            break;
                        case "GridSize":
                            _beatmap.GridSize = double.Parse(value);
                            break;
                        case "TimelineZoom" :
                            _beatmap.TimelineZoom = double.Parse(value);
                            break;
                    }
                    break;
                case DataSection.Metadata:
                    switch (key)
                    {
                        case "Artist":
                            _beatmap.Metadata.Artist = value;
                            break;
                        case "ArtistUnicode":
                            _beatmap.Metadata.ArtistUnicode = value;
                            break;
                        case "Title":
                            _beatmap.Metadata.Title = value;
                            break;
                        case "TitleUnicode":
                            _beatmap.Metadata.TitleUnicode = value;
                            break;
                        case "Creator":
                            _beatmap.Metadata.Creator = value;
                            break;
                        case "Version":
                            _beatmap.Metadata.Version = value;
                            break;
                        case "Source":
                            _beatmap.Metadata.Source = value;
                            break;
                        case "Tags":
                            _beatmap.Metadata.Tags = value;
                            break;
                        case "BeatmapID":
                            _beatmap.Metadata.BeatmapId = int.Parse(value);
                            break;
                        case "BeatmapSetID":
                            _beatmap.Metadata.BeatmapSetId = int.Parse(value);
                            break;
                    }
                    break;
                
                case DataSection.Difficulty:
                    switch (key)
                    {
                        case "HPDrainRate":
                            _beatmap.DifficultyAttributes.HpDrain = double.Parse(value);
                            break;
                        case "CircleSize":
                            _beatmap.DifficultyAttributes.CircleSize = double.Parse(value);
                            break;
                        case "OverallDifficulty":
                            _beatmap.DifficultyAttributes.OverallDifficulty = double.Parse(value);
                            break;
                        case "ApproachRate":
                            _beatmap.DifficultyAttributes.ApproachRate = double.Parse(value);
                            break;
                        case "SliderMultiplier":
                            _beatmap.DifficultyAttributes.SliderMultiplier = double.Parse(value);
                            break;
                        case "SliderTickRate":
                            _beatmap.DifficultyAttributes.SliderTickRate = double.Parse(value);
                            break;
                    }
                    break;
                case DataSection.None:
                default: break;
            }
            
        }
        _beatmap.HitObjects = new List<IHitObject>();
        _beatmap.HitObjects.AddRange(HitObjectProcessor(hitObjects, _beatmap));
        _beatmap.TimingPoints = TimingPointProcessor(timingPoints, _beatmap);
        _beatmap.InlineStoryBoardCommand = StoryBoardCommandProcessor(storyboardCommand, _beatmap);

        return _beatmap;
    }
}