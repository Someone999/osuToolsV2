using osuToolsV2.Beatmaps;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.GameInfo;
using SharpCompress.Archives.Zip;

namespace osuToolsV2.Utils;

public static class BeatmapPackUtil
{
    #if NETFRAMEWORK
    private static string GetRelativePath(string path, string relatedTo)
    {
        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            path = path + Path.DirectorySeparatorChar;
        }
        
        if (!relatedTo.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            relatedTo = relatedTo + Path.DirectorySeparatorChar;
        }

        return path.Replace(relatedTo, "");
    }
    #else
    private static string GetRelativePath(string path, string relatedTo)
    {
        return Path.GetRelativePath(relatedTo, path);
    }
    #endif
    public static void PackBeatmap(this OsuBeatmap beatmap, string targetFile, bool overwrite = true)
    {
        if (File.Exists(targetFile) && !overwrite)
        {
            throw new IOException("File already exists and overwrite is set to false.");
        }
        
        var osuInfo = OsuInfo.GetInstance();
        var beatmapDirectory = osuInfo.BeatmapDirectory;
        var beatmapFolder = beatmap.FolderName;
        var fullPath = Path.Combine(beatmapDirectory, beatmapFolder);
        
        using ZipArchive zipArchive = ZipArchive.Create();
        var files = Directory.GetFiles(fullPath, "*", SearchOption.AllDirectories);
        foreach (var file in files)
        {
            var relatePath = GetRelativePath(file, fullPath);
            var stream = File.OpenRead(file);
            zipArchive.AddEntry(relatePath, stream, true);
        }

        using var targetStream = File.Open(targetFile, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        zipArchive.SaveTo(targetStream);
    }
    
}