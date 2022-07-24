namespace osuToolsV2.Game.Config.Parser;

public class OsuConfigFileParser
{
    private string _path;

    public Dictionary<string, Dictionary<string, string>> Configs { get; set; } = new();
    public OsuConfigFileParser(string path)
    {
        _path = path;
        Parse();
    }

    void Parse()
    {
        string[] lines = File.ReadAllLines(_path);
        string? appKey = null;
        string? propertyName, valueStr;
        int commentIdx = 0;
        foreach (var line in lines)
        {
            if (line.StartsWith("//"))
            {
                Configs[appKey ?? throw new InvalidOperationException()].Add($"#Comment{commentIdx++}", line);
                continue;
            }
            
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                appKey = line.Substring(1, line.Length - 1);
                if (Configs.ContainsKey(appKey))
                {
                    continue;
                }
                Configs.Add(appKey, new Dictionary<string, string>());
            }
            else
            {
                int colonIndex = line.IndexOf(':');
                if (colonIndex == -1)
                {
                    continue;
                }
                propertyName = line.Substring(0, colonIndex).Trim();
                valueStr = line.Substring(colonIndex + 1).Trim();
                Configs[appKey ?? throw new InvalidOperationException()].Add(propertyName, valueStr);
            }
        }
    }
}