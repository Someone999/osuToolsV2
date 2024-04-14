using System.Reflection;
using HsManCommonLibrary.NestedValues.NestedValueAdapters;
using HsManCommonLibrary.NestedValues.Utils;
using HsManCommonLibrary.Reflections;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Online.OsuApi.Responses;

namespace osuToolsV2.Online.OsuApi.Version1;

public static class OsuApiInvoker
{
    private static HttpClient _client = new HttpClient();

    public static async Task<HttpApiResponse<JToken, JToken>> InvokeApiAsync(OsuApiRequest request)
    {
        string url = request.BuildUrl();
        var res = await _client.GetAsync(url);
        var resStr = await res.Content.ReadAsStringAsync();
        var jsonData = JsonConvert.DeserializeObject<JToken>(resStr);
        if (jsonData == null)
        {
            return new HttpApiResponse<JToken, JToken>(null, default, res);
        }

        if (jsonData is JObject jObject && jObject.ContainsKey("error"))
        {
            return new HttpApiResponse<JToken, JToken>(jsonData, default, res);
        }

       
        return new HttpApiResponse<JToken, JToken>(null, jsonData , res);
    }
}