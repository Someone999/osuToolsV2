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
                    b.Metadata.BackgroundInfo.FileName = splitData[2].Trim('"');
                    b.Metadata.BackgroundInfo.X = double.Parse(splitData[3]);
                    b.Metadata.BackgroundInfo.Y = double.Parse(splitData[4]);
                    break;
                case StoryBoardEventType.Video:
                    b.Metadata.VideoInfo.StartTime = double.Parse(splitData[1]);
                    b.Metadata.VideoInfo.FileName = splitData[2].Trim('"');
                    b.Metadata.VideoInfo.X = double.Parse(splitData[3]);
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