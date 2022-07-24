namespace osuToolsV2.Skins.Config;

public class SkinConfigParser
{
    public SkinConfig Config { get; } = new SkinConfig();
    
    public SkinConfigParser(string skinConfigFile)
    {
        Parse(skinConfigFile);
    }

    private void Parse(string configFilePath)
    {
        var lines = File.ReadAllLines(configFilePath);
        SkinConfigSection? currentSection = null;
        foreach (var line in lines)
        {
            if (line.StartsWith("//"))
            {
                continue;
            }
            
            if (line.StartsWith("[") && line.EndsWith("]"))
            {
                currentSection = new SkinConfigSection(line.Substring(1, line.Length - 2));
                Config.Sections.Add(currentSection);
            }
            else
            {
                int colonIdx = line.IndexOf(':');
                if (colonIdx == -1)
                {
                    continue;
                }

                string propertyName = line.Substring(0, colonIdx).Trim();
                string propertyValue = line.Substring(colonIdx + 1).Trim();
                
                currentSection?.ConfigItems.Add(new SkinConfigItem(propertyName, propertyValue));
            }
        }
    }
}