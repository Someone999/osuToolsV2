using System.Collections;
using System.Diagnostics;
using HsManCommonLibrary.Utils;
using Newtonsoft.Json;
using osuToolsV2.Beatmaps;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.Game.Mods;
using osuToolsV2.GameInfo;
using osuToolsV2.Reader;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Osu;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.Utils;
using SharpCompress;

namespace osuToolsV2;

public class Class1
{
    static async Task Main(string[] args)
    {
        OsuBeatmapDbObjectReader reader = new OsuBeatmapDbObjectReader();
        var collection = reader.Read();
        var beatmap = collection.Beatmaps.SearchByKeyword("(S)");
        if (beatmap.Count == 0)
        {
            return;
        }

        var b = beatmap[0].ToBeatmap();
        var storyboardLazyLoader = b.StoryBoardCommandLazyLoader;

        for (int i = 0; i < 20; i++)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            storyboardLazyLoader?.LoadObject();
            stopwatch.Stop();
        
            Console.WriteLine($"{i} : {stopwatch.ElapsedMilliseconds / 1000.0}");
        }
    }
}