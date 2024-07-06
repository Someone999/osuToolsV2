using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.LazyLoaders;

public class BreakTimeLazyLoader : ILazyLoader<BreakTimeCollection>
{
    private readonly IEnumerable<string> _storyBoardCommandDefinitions;
    private readonly Beatmap _beatmap;

    public BreakTimeLazyLoader(IEnumerable<string> storyBoardCommandDefinitions, Beatmap beatmap)
    {
        _storyBoardCommandDefinitions = storyBoardCommandDefinitions;
        _beatmap = beatmap;
    }
    public bool Loaded { get; private set; }
    public bool Loading { get; private set; }

    public BreakTimeCollection LoadObject()
    {
        List<BreakTime> breakTimes = new List<BreakTime>();
        Loading = true;
        foreach (var definition in _storyBoardCommandDefinitions)
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
        return new BreakTimeCollection(breakTimes);
    }
}