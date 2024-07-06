namespace osuToolsV2.Attributes;

public class CurrentlyUnavailableAttribute : Attribute
{
    private readonly string _reason;

    public CurrentlyUnavailableAttribute(string reason)
    {
        _reason = reason;
    }
}