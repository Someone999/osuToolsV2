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