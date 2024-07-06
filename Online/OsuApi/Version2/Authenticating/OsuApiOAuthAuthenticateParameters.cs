namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OsuApiOAuthAuthenticateParameters
{
    public OAuthClientCredentials ClientCredentials { get; set; } = new OAuthClientCredentials();
    public OAuthGrantParameters GrantParameters { get; set; } = new OAuthGrantParameters();
}
