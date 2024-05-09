using osuToolsV2.Online.OsuApi.Responses;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;

namespace osuToolsV2.Online.OsuApi.Version2.Requests;

public class OsuApiQueryContext<T> : OsuApiQueryContext
{
    public OsuApiQueryContext(T? context = default) : base(context)
    {
        Context = context;
    }

    public new T? Context { get; set; }
    public static OsuApiQueryContext<T> Create(T? val) => new OsuApiQueryContext<T>(val);
}




public interface IApiV2Request<TRet, TContext>
{
    Task<OsuApiV2Response<TRet>> QueryAsync(OsuOAuthToken token, OsuApiQueryContext<TContext> context);
}



