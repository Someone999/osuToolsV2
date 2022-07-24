namespace osuToolsV2.Skins.Config;

public class SkinConfigSection
{
    public SkinConfigSection(string sectionName)
    {
        SectionName = sectionName;
    }
    public string SectionName { get; set; } = "";
    public List<SkinConfigItem> ConfigItems { get; } = new List<SkinConfigItem>();
    

}