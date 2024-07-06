using HsManCommonLibrary.ValueHolders;

namespace osuToolsV2.Beatmaps;

public class BeatmapObjectHolder<T> : ValueHolder<T>
{
    public string? FullPath { get; set; }
}