using System.Security.Cryptography;
using osuToolsV2.Reader;

namespace osuToolsV2.Database.DuplicateFileDetectors;


class DuplicateJudgementInfo
    {
        private sealed class DuplicateJudgementInfoEqualityComparer : IEqualityComparer<DuplicateJudgementInfo>
        {
            public bool Equals(DuplicateJudgementInfo? x, DuplicateJudgementInfo? y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return x.BeatmapCount == y.BeatmapCount && x.FolderCount == y.FolderCount && x.PlayerName == y.PlayerName && x.Version == y.Version;
            }

            public int GetHashCode(DuplicateJudgementInfo obj)
            {
                return HashCode.Combine(obj.BeatmapCount, obj.FolderCount, obj.PlayerName, obj.Version);
            }
        }

        public static IEqualityComparer<DuplicateJudgementInfo> DuplicateJudgementInfoComparer
        { get; } = new DuplicateJudgementInfoEqualityComparer();

        public DuplicateJudgementInfo(OsuManifest manifest)
        {
            BeatmapCount = manifest.BeatmapCount;
            FolderCount = manifest.FolderCount;
            PlayerName = manifest.PlayerName;
            Version = manifest.Version;
        }

        protected bool Equals(DuplicateJudgementInfo other)
        {
            return BeatmapCount == other.BeatmapCount && 
                   FolderCount == other.FolderCount && 
                   PlayerName == other.PlayerName 
                   && Version == other.Version;
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((DuplicateJudgementInfo)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(BeatmapCount, FolderCount, PlayerName, Version);
        }

        public int BeatmapCount { get; }
        public int FolderCount { get; }
        public string PlayerName { get; }
        public int Version { get; }

    }
public interface IDuplicateFileDetector
{
    Dictionary<string, HashSet<string>> GetDuplicateFilesByMd5(string dir);
    Dictionary<string,  HashSet<string>> GetDuplicateFilesByFileContent(string dir);
}

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
            catch (Exception e)
            {
                // Skip the judgement
            }
        }

        return duplicateMap 
            .Where(p => p.Value.Count > 1)
            .ToDictionary(k => k.Value.First(), v => v.Value);
    }
}