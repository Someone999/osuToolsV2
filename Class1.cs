using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using HsManCommonLibrary.Utils;
using Newtonsoft.Json;
using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.BeatmapReader;
using osuToolsV2.Database;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.Database.DuplicateFileDetectors;
using osuToolsV2.Game.Mods;
using osuToolsV2.GameInfo;
using osuToolsV2.Reader;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Osu;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.Rulesets.Osu.ScoreProcessor;
using osuToolsV2.Utils;
using SharpCompress;

namespace osuToolsV2;

public static class BinaryConverter
{
}

public class Class1
{
    static void Main(string[] args)
    {
        /*var e = Environment.GetCommandLineArgs();
        OsuBeatmapDbObjectReader reader = new OsuBeatmapDbObjectReader();
        var db = reader.Read();
        var osuBeatmap = db.Beatmaps.SearchByKeyword("Slave tower another")[0];
        var scoreDbReader = new OsuScoreDbObjectReader();
        var scoreDb = scoreDbReader.Read();
        if (scoreDb == null)
        {
            return;
        }

        var scores = scoreDb
            .Scores
            .Where(s => s.BeatmapMd5 == osuBeatmap.Metadata.Md5Hash)
            .OrderByDescending(b => b.PlayTime)
            .ToArray();
        foreach (var score in scores)
        {
            Console.WriteLine(ScoreVisualUtil.BuildScoreDisplayString(score, new OsuScoreProcessor()));
            Console.WriteLine();
        }*/

        /*OsuDbDuplicateFileDetector detector = new OsuDbDuplicateFileDetector();
        var x = detector.GetDuplicateFilesByMd5(OsuInfo.GetInstance().OsuDirectory);*/
        

    }

    internal class Abc
    {
    }
}