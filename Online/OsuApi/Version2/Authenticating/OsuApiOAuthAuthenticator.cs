using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version2.Requests;

namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OsuApiOAuthAuthenticator
{
    private readonly Uri _tokenAllocUri = new Uri("https://osu.ppy.sh/oauth/token");
    private readonly Uri _tokenRevokeUri = new Uri("https://osu.ppy.sh/oauth/tokens/current");
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<HttpApiResponse<OsuApiV2ErrorInfo, OsuOAuthToken>> RequireTokenAsync
        (OsuApiOAuthAuthenticateParameters parameters)
    {
        UriBuilder uriBuilder = new UriBuilder
        {
            Scheme = _tokenAllocUri.Scheme,
            Host = _tokenAllocUri.Host,
            Path = _tokenAllocUri.LocalPath
        };
        Dictionary<string, string> args = new Dictionary<string, string>();
        args.Add("client_id", parameters.ClientCredentials.ClientId);
        args.Add("client_secret", parameters.ClientCredentials.ClientSecret);
        args.Add("grant_type", parameters.GrantParameters.GrantType);
        args.Add("scope", string.Join(" ", parameters.GrantParameters.Scopes));

        HttpRequestMessage requestMessage = new HttpRequestMessage();
        requestMessage.RequestUri = uriBuilder.Uri;
        requestMessage.Method = HttpMethod.Post;
        FormUrlEncodedContent content = new FormUrlEncodedContent(args);
        content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
        requestMessage.Content = content;
        
        var result = await _httpClient.SendAsync(requestMessage);
        var responseString = await result.Content.ReadAsStringAsync();
        var jObj = JsonConvert.DeserializeObject<JObject>(responseString);
        if (jObj == null)
        {
            throw new Exception("Failed to deserialize");
        }

        bool isError = jObj.ContainsKey("error");
        OsuApiV2ErrorInfo? errorInfo = null;
        OsuOAuthToken? token = null;
        if (isError)
        {
            errorInfo = jObj.ToObject<OsuApiV2ErrorInfo>();
        }
        else
        {
            token = jObj.ToObject<OsuOAuthToken>();
        }
        
        HttpApiResponse<OsuApiV2ErrorInfo, OsuOAuthToken> apiResponse =
            new HttpApiResponse<OsuApiV2ErrorInfo, OsuOAuthToken>(errorInfo, token, result);

        return apiResponse;
    }

    public async Task RevokeTokenAsync()
    {
        await _httpClient.DeleteAsync(_tokenRevokeUri);
    }
}