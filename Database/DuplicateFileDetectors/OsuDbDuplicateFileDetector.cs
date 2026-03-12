using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using HsManCommonLibrary.Common.ConditionChains;
using osuToolsV2.ExtraMethods;
using osuToolsV2.Reader;

namespace osuToolsV2.Database.DuplicateFileDetectors;

public class OsuDbDuplicateFileDetector : IDuplicateFileDetector
{
    private struct FileMetadata
    {
        public long FileSize { get; set; }

        public FileMetadata(FileInfo info)
        {
            FileSize = info.Length;
        }

        public override int GetHashCode()
        {
            return FileSize.GetHashCode();
        }

        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            Condition condition = new Condition();
            condition.Set(obj is long l && l == FileSize);
            var e = condition.CreateEvaluator();
            bool finalState = false;
            e.ChainCompleted += (_, state) => finalState = state;
            e.Execute(new ConditionContext());
            return finalState;
        }
    }
    
    private IEnumerable<string> FilterFiles(IEnumerable<string> files)
    {
        Dictionary<FileMetadata, List<string>> dict = new(); 
        foreach(var file in files)
        {
            FileInfo info = new FileInfo(file);
            dict[new FileMetadata(info)].Add(file);
        }
        
        List<string> filteredFiles = new();
        foreach (var pair in dict.Where(pair => pair.Value.Count != 1))
        {
            filteredFiles.AddRange(pair.Value);
        }

        return filteredFiles;
    }

    public Dictionary<string, HashSet<string>> GetDuplicateFilesByMd5(string dir)
    {
        Dictionary<string, HashSet<string>> duplicateMap = new Dictionary<string, HashSet<string>>();
        var files = FilterFiles(Directory.GetFiles(dir, "osu!.db.*", SearchOption.TopDirectoryOnly));
        foreach (var file in files)
        {
            var data = File.ReadAllBytes(file);
            var md5Str = MD5.HashData(data).GetMd5String();
            if (duplicateMap.TryGetValue(md5Str, out var list))
            {
                list.Add(file);
                continue;
            }
            
            list = new  HashSet<string> { file };
            duplicateMap.Add(md5Str, list);
        }

        return duplicateMap
            .Where(p => p.Value.Count > 1)
            .ToDictionary(k => k.Key, v => v.Value);
    }

    
    public Dictionary<string,  HashSet<string>> GetDuplicateFilesByFileContent(string dir)
    {
        Dictionary<DuplicateJudgementInfo, HashSet<string>> duplicateMap = new Dictionary<DuplicateJudgementInfo, HashSet<string>>();
        var files = FilterFiles(Directory.GetFiles(dir, "osu!.db.*", SearchOption.TopDirectoryOnly));
        //var md5Checked = GetDuplicateFilesByMd5(dir);
        
        foreach (var file in files)
        {
            OsuBeatmapDbObjectReader reader = new OsuBeatmapDbObjectReader(file);
            try
            {
                var db = reader.Read();
                DuplicateJudgementInfo judgementInfo = new DuplicateJudgementInfo(db.Manifest);
                if (duplicateMap.TryGetValue(judgementInfo, out var list))
                {
                    list.Add(file);
                    continue;
                }
                
                list = new HashSet<string> { file };
                duplicateMap.Add(judgementInfo, list);
            }
            catch (Exception)
            {
                // Skip the judgement
            }
        }

        return duplicateMap 
            .Where(p => p.Value.Count > 1)
            .ToDictionary(k => k.Value.First(), v => v.Value);
    }
}