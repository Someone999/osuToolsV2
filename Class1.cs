using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using HsManCommonLibrary.NestedValues.NestedValueAdapters;
using HsManCommonLibrary.NestedValues.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Beatmaps;
using osuToolsV2.Database;
using osuToolsV2.GameInfo;
using osuToolsV2.Online.OsuApi.Version1;
using osuToolsV2.Online.OsuApi.Version2.Requests;
using osuToolsV2.Replays;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreCalculators;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;


namespace osuToolsV2;

public class Class1
{
    static async Task Main(string[] args)
    {
        OsuApiOAuthAuthenticator apiOAuthAuthenticator = new OsuApiOAuthAuthenticator();
        OsuApiOAuthAuthenticateParameters parameters = new OsuApiOAuthAuthenticateParameters();
        parameters.ClientId = "2208";
        parameters.ClientSecret = "c388i4oeP8e8PxHZogS0faXkmhbuWmCvvRVLWnOl";
        var x= await apiOAuthAuthenticator.RequireTokenAsync(parameters, OsuApiGrantType.ClientCredential, "public identify");
        var context = OsuApiQueryContext<int>.Create(1443533);
        BeatmapQueryRequest queryRequest = new BeatmapQueryRequest();
        if (x.Data == null)
        {
            return;
        }
        var b = await queryRequest.QueryAsync(x.Data, context);
        IBeatmap? beatmap = b.Data?.ToApiV2ConvertBeatmap();
        ;


    }
}

