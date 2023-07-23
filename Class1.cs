using System.Text;
using osuToolsV2.Replays;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;


namespace osuToolsV2;

public class Class1
{
    static void Main(string[] args)
    {
        string dirName = @"D:\a\s\osu\osu!\Replays";
        string fileName = "2668585799 - Nobunaga - Shinkai Shoujo [Insane] (2019-02-14) Osu.osr";
        var path = Path.Combine(dirName, fileName);
            
        Replay replay = Replay.ReadFromFile(path);
        if (replay.AdditionalData.Length == 0)
        {
            return;
        }

        

    }
}