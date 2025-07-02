using System.Diagnostics;
using System.Text.Json.Nodes;
using HsManCommonLibrary.NestedValues.Attributes;
using HsManCommonLibrary.NestedValues.NestedValueAdapters;
using HsManCommonLibrary.NestedValues.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Beatmaps;
using osuToolsV2.Game.Mods;
using osuToolsV2.Online.OsuApi.Version1;
using osuToolsV2.Reader;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Mania.Mods;
using osuToolsV2.Rulesets.Mania.ScoreProcessor;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2;

class A
{
    [AutoAssign]
    public List<ApiBeatmap> Beatmaps { get; set; } = new List<ApiBeatmap>();
}
public class Class1
{
    static void Main(string[] args)
    {

        /*var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        var fileContent = File.ReadAllText(Path.Combine(desktop, "get_beatmaps.json"));
        var objs = JsonConvert.DeserializeObject<JArray>(fileContent);
        var nestedVal = new JsonNestedValueStoreAdapter().ToNestedValue(objs);
        
        Stopwatch myImplStopwatch = new Stopwatch();
        myImplStopwatch.Start();
        A beatmaps = new A();
        for (int i = 0; i < 50; i++)
        {
            ObjectAssigner.AssignTo(beatmaps, nestedVal, null);
        }
        
        myImplStopwatch.Stop();
        Console.WriteLine($"My impl elapsed time: {myImplStopwatch.ElapsedMilliseconds} ms");
        
        Stopwatch newtonsoftStopwatch = new Stopwatch();
        newtonsoftStopwatch.Start();
        for (int i = 0; i < 50; i++)
        {
            var x = objs!.ToObject<ApiBeatmap[]>();
        }
        
        newtonsoftStopwatch.Stop();
        Console.WriteLine($"Newtonoft elapsed time: {newtonsoftStopwatch.ElapsedMilliseconds} ms");*/

        string path = @"D:\a\s\osu\osu!\Songs\552712 Marshmello - Alone\Marshmello - Alone (Pachiru) [Secretpipe's Normal].osu";
        var beatmap = Beatmap.FromFile(path);
        var hitObjects = beatmap?.StoryBoardCommand;
        if (hitObjects == null)
        {
            return;
        }

        
        foreach (var h in hitObjects)
        {
            Console.WriteLine(h.AsMainStoryBoardCommand().FileName);
        }




    }

    
}
