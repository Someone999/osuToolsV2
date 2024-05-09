namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OAuthClientCredentials
{
    public string ClientId { get; set; } = "";
    public string ClientSecret { get; set; } = "";
}


public class OAuthGrantParameters
{
    public string GrantType { get; set; } = "";
    public List<string> Scopes { get; set; } = new List<string>();

    public static OAuthGrantParameters CreateApiGrantParameters() => new()
    {
        GrantType = OsuApiGrantType.ClientCredential,
        Scopes =
        {
            OsuApiScope.Public
        }
    };
    
    public static OAuthGrantParameters CreateUserAuthenticateGrantParameters(List<string> scopes) => new()
    {
        GrantType = OsuApiGrantType.AuthorizationCode,
        Scopes = scopes
    };
}

public class OsuApiOAuthAuthenticateParameters
{
    public OAuthClientCredentials ClientCredentials { get; set; } = new OAuthClientCredentials();
    public OAuthGrantParameters GrantParameters { get; set; } = new OAuthGrantParameters();
}
