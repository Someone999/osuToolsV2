using System.Drawing;
using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Graphic;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Commands.Resources;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.LazyLoaders;

public class StoryBoardCommandLazyLoader : ILazyLoader<StoryBoardCommandBase[]>
{
    private StoryBoardCommandBase[]? _cache;
    private IEnumerable<string>? _storyBoardCommandDefinitions;
    private readonly Beatmap _beatmap;

    public StoryBoardCommandLazyLoader(Beatmap beatmap, IEnumerable<string> storyBoardCommandDefinitions)
    {
        _beatmap = beatmap;
        _storyBoardCommandDefinitions = storyBoardCommandDefinitions;
    }

    public bool Loaded { get; private set; }
    public bool Loading { get; private set;}

    public StoryBoardCommandBase[] LoadObject()
    {
        if (_cache != null)
        {
            return _cache;
        }

        if (_storyBoardCommandDefinitions == null)
        {
            throw new InvalidOperationException();
        }
        Loading = true;
        List<string> realStoryCommand = new List<string>();
        List<BreakTime> breakTimes = new List<BreakTime>();
        foreach (var str in _storyBoardCommandDefinitions)
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

            if (string.IsNullOrEmpty(str) || str.StartsWith("//") || str.StartsWith("["))
            {
                continue;
            }

            var parseSuccess = Enum.TryParse<StoryBoardEventType>(splitData[0], out var val);

           
            StoryBoardEventType type = parseSuccess ? val : StoryBoardEventType.Unknown;
            switch (type)
            {
                case StoryBoardEventType.Background:

                    if (_beatmap.Metadata.BackgroundHolder.IsInitialized())
                    {
                        break;
                    }
                    
                    var background =  new Background
                    {
                        Layer = (StoryBoardLayer) int.Parse(splitData[1]),
                        FileName = splitData[2].Trim('"')
                    };
                    var x = double.Parse(splitData[3]);
                    var y = double.Parse(splitData[4]);
                    background.Position = new OsuPixel(x, y);
        
                    _beatmap.Metadata.BackgroundHolder.SetValue(background);
                    
                    var fullPath0 = _beatmap.Metadata.BeatmapFullPath;
                    if (string.IsNullOrEmpty(fullPath0))
                    {
                        break;
                    }

                    var dir0 = Path.GetDirectoryName(fullPath0) ?? "";
                    _beatmap.Metadata.BackgroundHolder.FullPath = Path.Combine(dir0, background.FileName);
                    break;
                case StoryBoardEventType.Video:
                    if (_beatmap.Metadata.VideoHolder.IsInitialized())
                    {
                        break;
                    }
                    
                    var startTime = int.Parse(splitData[1]);
                    var video = new Video
                    {
                        StartTime = startTime,
                        FileName = splitData[2].Trim('"')
                    };

                    _beatmap.Metadata.VideoHolder.SetValue(video);
                    var fullPath = _beatmap.Metadata.BeatmapFullPath;
                    if (string.IsNullOrEmpty(fullPath))
                    {
                        break;
                    }

                    var dir = Path.GetDirectoryName(fullPath) ?? "";
                    _beatmap.Metadata.VideoHolder.FullPath = Path.Combine(dir, video.FileName);
                    break;
                case StoryBoardEventType.BreakTime:
                    var loader = _beatmap.BreakTimeLazyLoader;
                    if (loader != null && (loader.Loaded || loader.Loading))
                    {
                        break;
                    }

                    _beatmap.BreakTimeCollection ??= new BreakTimeCollection(new List<BreakTime>());
                    breakTimes.Add(new BreakTime(double.Parse(splitData[1]), double.Parse(splitData[2])));
                    break;
                default:
                    realStoryCommand.Add(str);
                    break;
            }
        }

        _beatmap.BreakTimeCollection = new BreakTimeCollection(breakTimes);
        StoryBoardCommandParser parser = new StoryBoardCommandParser(realStoryCommand.ToArray());
        Loaded = true;
        Loading = false;
        _cache = parser.Parse();
        _storyBoardCommandDefinitions = null;
        return _cache;
    }
}