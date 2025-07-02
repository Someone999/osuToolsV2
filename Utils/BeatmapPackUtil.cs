using osuToolsV2.Beatmaps;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.GameInfo;
using SharpCompress.Archives.Zip;

namespace osuToolsV2.Utils;

public static class BeatmapPackUtil
{
    #if !!NETCOREAPP2_0_OR_GREATER
    private static string GetCommonPath(params string[] paths)
    {
        if (paths.Length == 0)
            return string.Empty;

        // 将路径规范化并按分隔符分割
        var splitPaths = paths
            .Select(p => Path.GetFullPath(p).Trim(Path.DirectorySeparatorChar).Split(Path.DirectorySeparatorChar))
            .ToArray();

        // 以第一个路径为基准，比较其他路径的每个部分
        var commonParts = splitPaths[0];

        for (int i = 1; i < splitPaths.Length; i++)
        {
            commonParts = commonParts.TakeWhile((part, index) => index < splitPaths[i].Length && part == splitPaths[i][index]).ToArray();
        }

        // 重新组合公共部分形成路径
        string commonPath = Path.DirectorySeparatorChar + Path.Combine(commonParts);

        return commonPath;
    }
    private static string GetRelativePath(string path, string relatedTo)
    {
        if (path == relatedTo)
        {
            return ".";
        }
        
        if (!path.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            path += Path.DirectorySeparatorChar;
        }
        
        if (!relatedTo.EndsWith(Path.DirectorySeparatorChar.ToString()))
        {
            relatedTo += Path.DirectorySeparatorChar;
        }

        return path.Replace(relatedTo, "");
    }
    #else
    private static string GetRelativePath(string path, string relatedTo)
    {
        return Path.GetRelativePath(relatedTo, path);
    }
    #endif

    private static string? GetBeatmapDirectory(IBeatmap beatmap)
    {
        var beatmapFullPath = beatmap.Metadata.BeatmapFullPath;
        if (!string.IsNullOrEmpty(beatmapFullPath))
        {
            var dir = Path.GetDirectoryName(beatmapFullPath);
            if (!string.IsNullOrEmpty(dir))
            {
                return dir;
            }
        }

        if (beatmap is OsuBeatmap osuBeatmap)
        {
            return osuBeatmap.FolderName;
        }

        throw new NotSupportedException("Unknown beatmap type");
    }
    
    
    public static void PackBeatmap(this IBeatmap beatmap, string targetFile, OsuGameInfoLoader? infoLoader = null, bool overwrite = true)
    {
        if (File.Exists(targetFile) && !overwrite)
        {
            throw new IOException("File already exists and overwrite is set to false.");
        }


        infoLoader ??= OsuGameInfoLoader.GetFromCurrentOsuProcess();
       
        if (infoLoader == null)
        {
            return;
        }
        
        var beatmapDirectory = infoLoader.GameConfig?["BeatmapDirectory"]?.GetValueAs<string>();
        var beatmapFolder = GetBeatmapDirectory(beatmap);
        if (string.IsNullOrEmpty(beatmapFolder) || string.IsNullOrEmpty(beatmapDirectory))
        {
            return;
        }
        
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