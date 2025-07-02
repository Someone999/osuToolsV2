using osuToolsV2.Beatmaps.TimingPoints;

namespace osuToolsV2.LazyLoaders;

public class TimingPointLazyLoader : ILazyLoader<TimingPointCollection>
{
    private IEnumerable<string>? _lines;
    private TimingPointCollection? _cache;

    public TimingPointLazyLoader(IEnumerable<string> lines)
    {
        _lines = lines;
    }

    public bool Loaded { get; private set; }
    public bool Loading { get; private set; }

    public TimingPointCollection LoadObject()
    {
        if (_cache != null)
        {
            return _cache;
        }

        if (_lines == null)
        {
            throw new InvalidOperationException();
        }
        
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
        _cache = new TimingPointCollection(timingPoints);
        _lines = null;
        return _cache;
    }
}