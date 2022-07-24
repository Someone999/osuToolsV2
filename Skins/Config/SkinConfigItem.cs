namespace osuToolsV2.Skins.Config;

public class SkinConfigItem
{
    public SkinConfigItem(string propertyName, string propertyValue)
    {
        PropertyName = propertyName;
        PropertyValue = propertyValue;
    }
    public string PropertyName { get; set; }
    public string PropertyValue { get; set; }

    public bool TryConvertTo<T>(out T? val)
    {
        try
        {
            val = (T)Convert.ChangeType(PropertyValue, typeof(T));
            return true;
        }
        catch (Exception)
        {
            val = default;
            return false;
        }
    }
    
    
}