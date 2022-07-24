using System.Text;
using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.BeatmapWriter.DefaultWriters;
using osuToolsV2.Replays;
using osuToolsV2.Skins;
using osuToolsV2.Writer.DefaultWriters;

namespace osuToolsV2;

public class Class1
{
    static void Main(string[] args)
    {
        Replay replay = Replay.ReadFromFile(@"D:\a\s\osu\osu!\Replays\2668585799 - Shiena Nishizawa - FUBUKI [Starry's 4K Lv.16] (2022-07-23) OsuMania.osr");
        replay.ScoreInfo.Count300 = 0;
        replay.ScoreInfo.CountKatu = 0;
        replay.ScoreInfo.Score = 2147483647;
        replay.ScoreInfo.MaxCombo = 0;
        replay.WriteToFile(@"D:\a\s\osu\osu!\Replays\2668585799 - Shiena Nishizawa - FUBUKI [Starry's 4K Lv.16] (2022-07-23) OsuMania.fake.osr");

        ;
    }
}