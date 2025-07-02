using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.LazyLoaders;

public class BreakTimeLazyLoader : ILazyLoader<BreakTimeCollection>
{
    private readonly Beatmap _beatmap;
    private IEnumerable<string>? _lines;
    private BreakTimeCollection? _cached;

    public BreakTimeLazyLoader(Beatmap beatmap, IEnumerable<string> lines)
    {
        _beatmap = beatmap;
        _lines = lines;
    }

    public bool Loaded { get; private set; }
    public bool Loading { get; private set;  }

    public BreakTimeCollection LoadObject()
    {
        if (_cached != null)
        {
            return  _cached;
        }

        if (_lines == null)
        {
            throw new InvalidOperationException();
        }
        
        List<BreakTime> breakTimes = new List<BreakTime>();
        Loading = true;
        foreach (var definition in _lines)
        {
            if (string.IsNullOrEmpty(definition))
            {
                continue;
            }

            string[] splitData = definition.Split(',');
            if (splitData.Length == 0)
            {
                continue;
            }

            if (string.IsNullOrEmpty(definition) || definition.StartsWith("//") || definition.StartsWith("["))
            {
                continue;
            }

            var parseSuccess = Enum.TryParse<StoryBoardEventType>(splitData[0], out var val);
            if (!parseSuccess || val != StoryBoardEventType.BreakTime)
            {
                continue;
            }
            
            breakTimes.Add(new BreakTime(double.Parse(splitData[1]), double.Parse(splitData[2])));
        }

        Loaded = true;
        Loading = false;
        _lines = null;
        _cached = new BreakTimeCollection(breakTimes);
        return _cached;
    }
}