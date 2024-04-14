namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class OsuApiQueryContext
{
    public OsuApiQueryContext(object? context = default)
    {
        Context = context;
    }

    public object? Context { get; set; }
}