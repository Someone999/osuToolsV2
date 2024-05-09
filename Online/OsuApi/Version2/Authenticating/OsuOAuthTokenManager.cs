namespace osuToolsV2.Online.OsuApi.Version2.Authenticating;

public class OsuOAuthTokenManager
{
    private OsuOAuthToken? _currentToken;
    public async Task<OsuOAuthToken?> RequireTokenAsync(OsuApiOAuthAuthenticateParameters parameters)

    {
        if (_currentToken is { Expired: false })
        {
            return _currentToken;
        }

        OsuApiOAuthAuthenticator apiOAuthAuthenticator = new OsuApiOAuthAuthenticator();
        var result = await apiOAuthAuthenticator.RequireTokenAsync(parameters);
        if (!result.IsSuccess || result.Data == null)
        {
            return null;
        }

        _currentToken = result.Data;
        return _currentToken;
    }
}