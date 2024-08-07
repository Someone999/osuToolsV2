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
        var fileName = @"D:\a\s\osu\osu!\Songs\Quantum-RM-01\Quantum(None) [mania].osu";
        var reader = new DefaultFileBeatmapReader(fileName);
        var b = reader.Read();
        if (b == null)
        {
            return;
        }
        var x = b.HitObjects;
        var y = b.InlineStoryBoardCommand;
        var z = b.TimingPointCollection;
        
    }

    internal class Abc
    {
    }
}