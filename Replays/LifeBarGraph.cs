namespace osuToolsV2.Replays;

public class LifeBarGraph
{
    public LifeBarGraph(string lifeBarGraphStr)
    {
        string[] parts = lifeBarGraphStr.Split('|');
        if (parts.Length != 2)
        {
            return;
        }

        Offset = double.Parse(parts[0]);
        HpPercent = double.Parse(parts[1]);
    }

    public double Offset { get; } = -1;
    public double HpPercent { get; } = -1;

    public string ToFileFormat()
    {
        return $"{Offset}|{HpPercent}";
    }
    
}