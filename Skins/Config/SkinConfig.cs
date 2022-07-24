namespace osuToolsV2.Skins.Config;

public class SkinConfig
{
    public List<SkinConfigSection> Sections { get; } = new List<SkinConfigSection>();
    public SkinConfigSection[] this[string sectionName] => Sections.Where(sec => sec.SectionName == sectionName).ToArray();
}