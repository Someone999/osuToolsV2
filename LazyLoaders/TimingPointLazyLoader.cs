using osuToolsV2.Beatmaps.TimingPoints;

namespace osuToolsV2.LazyLoaders;

public class TimingPointLazyLoader : ILazyLoader<TimingPointCollection>
{
    private readonly IEnumerable<string> _lines;

    public TimingPointLazyLoader(IEnumerable<string> lines)
    {
        _lines = lines;
    }

    public bool Loaded { get; private set; }
    public bool Loading { get; private set; }

    public TimingPointCollection LoadObject()
    {
        Loading = true;
        List<TimingPoint> timingPoints = new List<TimingPoint>();
        foreach (var line in _lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            timingPoints.Add(new TimingPoint(line));
        }

        Loaded = true;
        Loading = false;
        return new TimingPointCollection(timingPoints);
    }
}