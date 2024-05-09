namespace osuToolsV2.Utils;

public static class PerformancePointUtil
{
    public static double CalculateBestListScaledPp(List<double> ppList, int limit = 100)
    {
        if (limit > 100)
        {
            limit = 100;
        }

        ppList.Sort((x, y) => y.CompareTo(x));
        double scaledPp = 0;
        for (int i = 0; i < limit; i++)
        {
            var rawPp = Math.Round(ppList[i], 3);
            var ppPosition = i + 1;
            var scaleFactor = Math.Pow(0.95, ppPosition - 1);
            scaledPp += Math.Round(rawPp * scaleFactor, 3);
        }

        return scaledPp;
    }
    
    public static List<double> GetBestListScaledPp(List<double> ppList, int limit = 100)
    {
        if (limit > 100)
        {
            limit = 100;
        }

        ppList.Sort((x, y) => y.CompareTo(x));
        List<double> scaledPp = new List<double>();
        for (int i = 0; i < limit; i++)
        {
            var rawPp = Math.Round(ppList[i], 3);
            var ppPosition = i + 1;
            var scaleFactor = Math.Pow(0.95, ppPosition - 1);
            scaledPp.Add(Math.Round(rawPp * scaleFactor, 3));
        }

        return scaledPp;
    }
    
}