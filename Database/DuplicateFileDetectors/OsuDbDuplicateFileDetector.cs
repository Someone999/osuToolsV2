using System.Security.Cryptography;
using osuToolsV2.ExtraMethods;
using osuToolsV2.Reader;

namespace osuToolsV2.Database.DuplicateFileDetectors;

public class OsuDbDuplicateFileDetector : IDuplicateFileDetector
{
    public Dictionary<string, HashSet<string>> GetDuplicateFilesByMd5(string dir)
    {
        Dictionary<string, HashSet<string>> duplicateMap = new Dictionary<string, HashSet<string>>();
        var files = Directory.GetFiles(dir, "osu!.db.*", SearchOption.TopDirectoryOnly);
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
        var files = Directory.GetFiles(dir, "osu!.db.*", SearchOption.TopDirectoryOnly);
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