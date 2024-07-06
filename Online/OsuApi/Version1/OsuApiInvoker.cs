using System.Net.Http;
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
    
    public static async Task<HttpApiResponse<JToken, TResult>> InvokeApiAsync<TResult>(OsuApiRequest request)
    {
        string url = request.BuildUrl();
        var res = await _client.GetAsync(url);
        var resStr = await res.Content.ReadAsStringAsync();
        var jsonData = JsonConvert.DeserializeObject<JToken>(resStr);
        if (jsonData == null)
        {
            return new HttpApiResponse<JToken, TResult>(null, default, res);
        }

        if (jsonData is JObject jObject && jObject.ContainsKey("error"))
        {
            return new HttpApiResponse<JToken, TResult>(jsonData, default, res);
        }

       
        return new HttpApiResponse<JToken, TResult>(null, jsonData.ToObject<TResult>() , res);
    }
    
    public static async Task<HttpApiResponse<TError, TResult>> InvokeApiAsync<TResult, TError>(OsuApiRequest request)
    {
        string url = request.BuildUrl();
        var res = await _client.GetAsync(url);
        var resStr = await res.Content.ReadAsStringAsync();
        var jsonData = JsonConvert.DeserializeObject<JToken>(resStr);
        if (jsonData == null)
        {
            return new HttpApiResponse<TError, TResult>(default, default, res);
        }

        if (jsonData is JObject jObject && jObject.ContainsKey("error"))
        {
            return new HttpApiResponse<TError, TResult>(jsonData.ToObject<TError>(), default, res);
        }

       
        return new HttpApiResponse<TError, TResult>(default, jsonData.ToObject<TResult>() , res);
    }
}