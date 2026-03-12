namespace osuToolsV2.Database.Beatmap.BeatmapSearching;

public class SearchTextList
{
    private HashSet<string> _searchList = new HashSet<string>();

    public void AddString(string s)
    {
        if (string.IsNullOrEmpty(s))
        {
            return;
        }
        
        _searchList.Add(s);
    }

    public bool AnyMatch(string keyword)
    {
        return _searchList.Any(s => s.Contains(keyword));
    }
    
    public bool AnySubsequenceMatch(string keyword)
    {
        if (!keyword.Contains(' '))
        {
            return AnyMatch(keyword);
        }

        var keywordList = keyword.Split(' ');
        return keywordList.Any(AnyMatch);
    }
    
    public bool AllSubsequenceMatch(string keyword)
    {
        if (!keyword.Contains(' '))
        {
            return AnyMatch(keyword);
        }

        var keywordList = keyword.Split(' ');
        return keywordList.All(AnyMatch);
    }
    
    
    public bool AllMatch(Func<string, bool> predicate)
    {
        return _searchList.All(predicate);
    }
    
    public bool ExactMatch(string keyword)
    {
        return _searchList.Any(s => s == keyword);
    }
}