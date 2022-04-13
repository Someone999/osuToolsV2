using osuToolsV2.Beatmaps.BreakTimes;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Rulesets;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.StoryBoard.Enums;

namespace osuToolsV2.Beatmaps;

public static class BeatmapTools
{
    public static StoryBoardCommandBase[] StoryCommandProcessor(List<string> lines, Beatmap b)
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
            
            StoryBoardEventType type = (StoryBoardEventType)Enum.Parse(typeof(StoryBoardEventType),splitData[0]);
            switch (type)
            {
                case StoryBoardEventType.Background:
                    b.Metadata.BackgroundFileName = splitData[2].Trim('"');
                    break;
                case StoryBoardEventType.Video:
                    b.Metadata.VideoFileName = splitData[2].Trim('"');
                    b.HasVideo = true;
                    break;
                case StoryBoardEventType.BreakTime:
                    b.BreakTimes.Add(new BreakTime(double.Parse(splitData[1]), double.Parse(splitData[1])));
                    break;
                default: realStoryCommand.Add(str);
                    break;
            }
        }
        StoryBoardCommandParser parser = new StoryBoardCommandParser(realStoryCommand.ToArray());
        return parser.Parse();
    }

    public static List<IHitObject> HitObjectProcessor(List<string> lines, IBeatmap b)
    {
        List<IHitObject> hitObjects = new List<IHitObject>();
        foreach (var line in lines)
        {
            hitObjects.Add(b.Ruleset.CreateHitObject(b, line.Split(',')));
        }
        return hitObjects;
    }

    public static List<TimingPoint> TimingPointProcessor(List<string> lines, IBeatmap beatmap)
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
}