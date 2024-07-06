using System.Collections;
using HsManCommonLibrary.Utils;
using osuToolsV2.Beatmaps.BreakTimes;
using EqualityUtils = HsManCommonLibrary.ValueHolders.EqualityUtils;

namespace osuToolsV2.Beatmaps;

public class BreakTimeCollection : IEnumerable<BreakTime>
{

    private readonly List<BreakTime> _breakTimes;
    private Dictionary<double, int> _startSearchIndex = new Dictionary<double, int>();

    public BreakTimeCollection(IEnumerable<BreakTime> breakTimes)
    {
        if (breakTimes is List<BreakTime> list)
        {
            _breakTimes = list;
            return;
        }
        
        _breakTimes = new List<BreakTime>(breakTimes);
    }

    [Flags]
    private enum BinaryFindFlags
    {
        FindLess = 1,
        FindLarger = 2,
        FindEquals = 4
    }
    
    private static BreakTime? BinarySearchByOffset(List<BreakTime> list, double offset, BinaryFindFlags findFlags)
    {
        if (findFlags.HasFlag(BinaryFindFlags.FindLess) && findFlags.HasFlag(BinaryFindFlags.FindLarger))
        {
            throw new InvalidOperationException("Can not search from two directions at same time.");
        }
        
        int low = 0;
        int high = list.Count - 1;

        while (low <= high)
        {
            int mid = low + (high - low) / 2;
            int compareResult = list[mid].StartTime.CompareTo(offset);

            switch (compareResult)
            {
                case 0 when findFlags.HasFlag(BinaryFindFlags.FindEquals):
                    return list[mid];
                case > 0:
                {
                    if (findFlags.HasFlag(BinaryFindFlags.FindLarger))
                    {
                        if (mid == 0 || list[mid - 1].StartTime.CompareTo(offset) < 0)
                        {
                            return list[mid];
                        }
                    }
                    high = mid - 1;
                    break;
                }
                default:
                {
                    if (findFlags.HasFlag(BinaryFindFlags.FindLess))
                    {
                        if (mid == list.Count - 1 || list[mid + 1].StartTime.CompareTo(offset) > 0)
                        {
                            return list[mid];
                        }
                    }
                    low = mid + 1;
                    break;
                }
            }
        }

        return null;
    }

    public BreakTime? SearchBreakTimeAfter(double offset, bool inclusiveOffset)
    {

        var flags = BinaryFindFlags.FindLarger;
        if (inclusiveOffset)
        {
            flags |= BinaryFindFlags.FindEquals;
        }
        
        return BinarySearchByOffset(_breakTimes, offset, flags);
    }
    
    public BreakTime? SearchBreakTimeBefore(double offset, bool inclusiveOffset)
    {
        var flags = BinaryFindFlags.FindLess;
        if (inclusiveOffset)
        {
            flags |= BinaryFindFlags.FindEquals;
        }
        
        return BinarySearchByOffset(_breakTimes, offset, flags);
    }
    
    public int GetIndexOf(double offset, bool inclusiveOffset)
    {
        var flags = BinaryFindFlags.FindLess;
        if (inclusiveOffset)
        {
            flags |= BinaryFindFlags.FindEquals;
        }
        
        var r = BinarySearchByOffset(_breakTimes, offset, flags);
        if (r == null)
        {
            return -1;
        }
        
        return _breakTimes.IndexOf(r);
    }
    
    public bool InPeriod(double offset)
    {
        var last = SearchBreakTimeBefore(offset, true);
        if (last == null)
        {
            return false;
        }

        return last.StartTime < offset && last.EndTime > offset;
    }

    public BreakTime this[int i] => _breakTimes[i];
    public IEnumerator<BreakTime> GetEnumerator() => _breakTimes.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public int Count => _breakTimes.Count;
    public IReadOnlyList<BreakTime> AsList() => _breakTimes.AsReadOnly();
}