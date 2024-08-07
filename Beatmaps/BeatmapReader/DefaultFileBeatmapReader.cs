using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.HitObjects.HitObjectParser;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Beatmaps.Misc;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Exceptions;
using osuToolsV2.ExtraMethods;
using osuToolsV2.Graphic;
using osuToolsV2.LazyLoaders;
using osuToolsV2.Reader;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Commands.Resources;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.Beatmaps.BeatmapReader;

public class DefaultFileBeatmapReader : IFileBeatmapReader
{
    private enum FindState
    {
        None,
        Found,
        NotFound,
        OffsetTooLate,
        Skipped
    }
    private static MD5 _md5Clac = MD5.Create();
    static readonly Regex VersionMatcher = new Regex("osu file format v(\\d*)");
    private Beatmap _beatmap = new Beatmap();
    private string _beatmapFile;
    private StringReader? _reader;

    public DefaultFileBeatmapReader(string filePath)
    {
        _beatmapFile = filePath;
    }
    
    private static readonly Regex InvalidPathCharsRegex = 
        new Regex("[" + new string(Path.GetInvalidPathChars()
            .Concat(Path.GetInvalidFileNameChars()).ToArray()) + "]");


    private string NormalizePath(string path)
    {
        return InvalidPathCharsRegex.Replace(path, "");
    }

    
    private string GetBeatmapSetStoryBoardFileName(BeatmapMetadata metadata)
    {
        var file = $"{metadata.Artist} - {metadata.Title} ({metadata.Creator}).osb";
        string normalized = NormalizePath(file);
        return normalized;
    }
    
    
    
    StoryBoardCommandLazyLoader CreateStoryBoardCommandLazyLoader(Beatmap b)
    {
        var lines = new List<string>();
        var beatmapFolder = Path.GetDirectoryName(_beatmapFile) ?? throw new InvalidOperationException();
        var metadata = _beatmap.Metadata;
        var beatmapSetStoryBoardFile = GetBeatmapSetStoryBoardFileName(metadata);
        
        
        beatmapSetStoryBoardFile = Path.Combine(beatmapFolder, beatmapSetStoryBoardFile);
        
        if (File.Exists(beatmapSetStoryBoardFile))
        {
            lines.AddRange(File.ReadAllLines(beatmapSetStoryBoardFile));
        }
        
        _ = lines.FirstOrDefault(f =>
        {
            var state = FindVideo(f);
            return state is FindState.Found or FindState.OffsetTooLate;
        });
        
        _ = lines.FirstOrDefault(f =>
        {
            var state = FindBackground(f);
            return state is FindState.Found or FindState.OffsetTooLate;
        });
        
        return new StoryBoardCommandLazyLoader(lines, b);
    }
    
    StoryBoardCommandLazyLoader CreateInlineStoryBoardCommandLazyLoader(List<string> lines, Beatmap b)
    {
        _ = lines.FirstOrDefault(f =>
        {
            var state = FindVideo(f);
            return state is FindState.Found or FindState.OffsetTooLate;
        });
        
        _ = lines.FirstOrDefault(f =>
        {
            var state = FindBackground(f);
            return state is FindState.Found or FindState.OffsetTooLate;
        });
        
        return new StoryBoardCommandLazyLoader(lines, b);
    }

    HitObjectLazyLoader CreateHitObjectLazyLoader(List<string> lines, Beatmap b)
    {
        return new HitObjectLazyLoader(lines, b);
    }

    TimingPointLazyLoader CreateTimingPointLazyLoader(List<string> lines)
    {
        return new TimingPointLazyLoader(lines);
    }

    BreakTimeLazyLoader CreateBreakTimeLazyLoader(List<string> lines, Beatmap b)
    {
        return new BreakTimeLazyLoader(lines, b);
    }
    

    void ReadGeneralData(string key, string value)
    {
        switch (key)
        {
            case "AudioFilename":
                _beatmap.Metadata.AudioFileName = value;
                break;
            case "AudioLeadIn":
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
            case "SkinPreference":
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
    }

    void ReadEditorData(string key, string value)
    {
        switch (key)
        {
            case "Bookmarks":
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
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
            case "TimelineZoom":
                _beatmap.TimelineZoom = double.Parse(value);
                break;
        }
    }

    private void ReadMetadata(string key, string value)
    {
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
    }

    private void ReadDifficultyAttributes(string key, string value)
    {
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
    }

    
    FindState ParseVideo(string[] splitData)
    {
        if (_beatmap.Metadata.VideoHolder.IsInitialized())
        {
            return FindState.Found;
        }
        
        if (splitData.Length < 3 || !double.TryParse(splitData[1], out var startTime))
        {
            return FindState.Skipped;
        }

        if (startTime > 0)
        {
            return FindState.OffsetTooLate;
        }
        
        if (splitData[0] != "Video" && splitData[0] != "1")
        {
            return FindState.Skipped;
        }

        var video = new Video
        {
            StartTime = startTime,
            FileName = splitData[2].Trim('"')
        };

        _beatmap.Metadata.VideoHolder.BindValue(video);

        var beatmapDir = Path.GetDirectoryName(_beatmap.Metadata.BeatmapFullPath);
        if (string.IsNullOrEmpty(beatmapDir))
        {
            throw new InvalidOperationException();
        }
        
        _beatmap.Metadata.VideoHolder.FullPath =  Path.Combine(beatmapDir, video.FileName);
        return FindState.Found;
    }
    
    FindState ParseBackground(string[] splitData)
    {
        if (_beatmap.Metadata.BackgroundHolder.IsInitialized())
        {
            return FindState.Found;
        }
        
        if (splitData.Length < 5 || !int.TryParse(splitData[1], out var layer))
        {
            return FindState.Skipped;
        }

        if (layer != 0)
        {
            return FindState.NotFound;
        }
        
        if (splitData[0] != "Background" && splitData[0] != "0")
        {
            return FindState.Skipped;
        }
        
        var background =  new Background
        {
            Layer = (StoryBoardLayer) layer,
            FileName = splitData[2].Trim('"')
        };
        var x = double.Parse(splitData[3]);
        var y = double.Parse(splitData[4]);
        background.Position = new OsuPixel(x, y);
        
        _beatmap.Metadata.BackgroundHolder.BindValue(background);
        var beatmapDir = Path.GetDirectoryName(_beatmap.Metadata.BeatmapFullPath);
        if (string.IsNullOrEmpty(beatmapDir))
        {
            throw new InvalidOperationException();
        }
        
        _beatmap.Metadata.BackgroundHolder.FullPath =  Path.Combine(beatmapDir, background.FileName);
        return FindState.Found;
    }
    
    private FindState _videoFindState;
    private FindState _backgroundFindState;
    private FindState FindVideo(string line)
    {
        if (_videoFindState == FindState.Found)
        {
            return FindState.Found;
        }
        
        if (line.StartsWith("//") || string.IsNullOrEmpty(line))
        {
            return _videoFindState = FindState.Skipped;
        }
                        
        var parts = line.Split(',');
        _videoFindState = ParseVideo(parts);
        return _videoFindState;
    }
    
    private FindState FindBackground(string line)
    {
        if (_backgroundFindState == FindState.Found)
        {
            return FindState.Found;
        }
        
        if (line.StartsWith("//") || string.IsNullOrEmpty(line))
        {
            return _backgroundFindState = FindState.Skipped;
        }
                        
        var parts = line.Split(',');
        _backgroundFindState = ParseBackground(parts);
        return _backgroundFindState;
    }

    public Beatmap? Read()
    {
        if (!File.Exists(_beatmapFile))
        {
            return null;
        }
        
        _reader = new StringReader(File.ReadAllText(_beatmapFile));
        IsReading = true;
        DataSection currentSection = DataSection.None;
        string? line = Reader.ReadLine();
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
            line = Reader.ReadLine();
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
                    ReadGeneralData(key, value);
                    break;
                case DataSection.Editor:
                   ReadEditorData(key, value);
                    break;
                case DataSection.Metadata:
                    ReadMetadata(key, value);
                    break;

                case DataSection.Difficulty:
                   ReadDifficultyAttributes(key, value);
                    break;
                case DataSection.None:
                default: break;
            }
        }
        
        _beatmap.HitObjectLazyLoader = CreateHitObjectLazyLoader(hitObjects, _beatmap);
        _beatmap.TimingPointLazyLoader = CreateTimingPointLazyLoader(timingPoints);
        _beatmap.InlineStoryBoardCommandLazyLoader = CreateInlineStoryBoardCommandLazyLoader(storyboardCommand, _beatmap);
        _beatmap.StoryBoardCommandLazyLoader = CreateStoryBoardCommandLazyLoader(_beatmap);
        _beatmap.BreakTimeLazyLoader = CreateBreakTimeLazyLoader(storyboardCommand, _beatmap);
        IsReading = false;
        return _beatmap;
    }

    public StringReader Reader
    {
        get => _reader ?? throw new InvalidOperationException("StringReader not initialized.");
        set
        {
            if (IsReading)
            {
                return;
            }
            
            _reader = value;
        }
    }

    public bool IsReading { get; private set; }

    object? IObjectReader<StringReader>.Read()
    {
        return Read();
    }
}