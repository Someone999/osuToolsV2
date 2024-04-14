using System.Text;

namespace osuToolsV2.Online.OsuApi.Version1;

public class OsuApiRequest
{
    /// <summary>
    ///  Argument references can be found on https://github.com/ppy/osu-api/wiki
    /// </summary>
    List<KeyValuePair<string, string>> _arguments  = new List<KeyValuePair<string, string>>();
    public string ApiKey { get; set; } = "";
    public string ApiPath { get; set; } = "";

    public void AddArgument(object key, object val)
    {
        var keyString = key.ToString();
        var valString = val.ToString() ?? "";
        if (string.IsNullOrEmpty(keyString))
        {
            return;
        }
        
        _arguments.Add(new KeyValuePair<string, string>(keyString, valString));
    }

    private const string OsuUrl = "osu.ppy.sh"; 

    public string BuildUrl()
    {
        UriBuilder builder = new UriBuilder();
        builder.Host = OsuUrl;
        builder.Path = ApiPath;
        StringBuilder argumentsBuilder = new StringBuilder();
        AddArgument("k", ApiKey);
        for (int i = 0; i < _arguments.Count; i++)
        {
            argumentsBuilder.Append($"{_arguments[i].Key}={_arguments[i].Value}");
            if (i < _arguments.Count - 1)
            {
                argumentsBuilder.Append('&');
            }
        }

        builder.Query = argumentsBuilder.ToString();
        builder.Scheme = "https://";
        string builtUri = builder.ToString();
        return builtUri;
    }
}
