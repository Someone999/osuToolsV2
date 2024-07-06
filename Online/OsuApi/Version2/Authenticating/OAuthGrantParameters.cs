namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OAuthGrantParameters
{
    public string GrantType { get; set; } = "";
    public List<string> Scopes { get; set; } = new List<string>();
    public string? Code { get; set; }
    public string? RedirectUri { get; set; }

    public static OAuthGrantParameters CreateApiGrantParameters() => new()
    {
        GrantType = OsuApiGrantType.ClientCredential,
        Scopes =
        {
            OsuApiScope.Public
        }
    };
    
    public static OAuthGrantParameters CreateUserAuthenticateGrantParameters
        (List<string> scopes, string code, string redirectUri) => new()
    {
        GrantType = OsuApiGrantType.AuthorizationCode,
        Scopes = scopes,
        Code = code,
        RedirectUri = redirectUri
    };
}