using System.Collections;

namespace osuToolsV2.Beatmaps.TimingPoints;

public class TimingPointCollection : IEnumerable<TimingPoint>
{
    private List<TimingPoint> _timingPoints;

    public TimingPointCollection(IEnumerable<TimingPoint> timingPoints)
    {
        _timingPoints = new List<TimingPoint>(timingPoints);
    }

    public IEnumerator<TimingPoint> GetEnumerator() => _timingPoints.GetEnumerator();
   
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    private double? _mostCommonBpm;

    public TimingPoint[] TimingPoints => _timingPoints.ToArray();

    private TimingPoint? BinarySearchTimingPointByOffset(List<TimingPoint> timingPoints, double offset)
    {
        if (timingPoints.Count == 0)
        {
            return null;
        }

        int left = 0;
        int right = timingPoints.Count - 1;

        while (left <= right)
        {
            int middle = left + (right - left) / 2;
            var middlePoint = timingPoints[middle];

            if (Math.Abs(middlePoint.Offset - offset) < 1e-5)
            {
                return middlePoint;
            }

            if (middlePoint.Offset < offset)
            {
                left = middle + 1;
            }
            else
            {
                right = middle - 1;
            }
        }

        // When the loop ends, right < left. The correct TimingPoint is the one before the offset, so we return right.
        if (right >= 0)
        {
            return timingPoints[right];
        }

        return null;
    }

    public TimingPoint? TimingPointAt(double offset)
    {
        return BinarySearchTimingPointByOffset(_timingPoints, offset);
    }

    public double? BpmAt(double offset) => TimingPointAt(offset)?.Bpm;
   

    public double GetMostCommonBpm()
    {
        if (_mostCommonBpm != null)
        {
            return _mostCommonBpm.Value;
        }

        if (_timingPoints == null || !_timingPoints.Any())
        {
            throw new InvalidOperationException("No timing points available.");
        }
        Dictionary<double, int> bpmMap = new Dictionary<double, int>();
        foreach (var currentBpm in from timingPoint in _timingPoints where !timingPoint.Inherited select timingPoint.Bpm)
        {
            bpmMap.TryAdd(currentBpm, 0);
            bpmMap[currentBpm]++;
        }

        var mostCommonBpm = bpmMap.OrderByDescending(p => p.Value).First().Key;
        _mostCommonBpm = mostCommonBpm;
        return mostCommonBpm;
    }
}