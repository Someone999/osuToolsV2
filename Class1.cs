using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using HsManCommonLibrary.NestedValues.NestedValueAdapters;
using HsManCommonLibrary.NestedValues.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using osuToolsV2.Beatmaps;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Database;
using osuToolsV2.Database.Score;
using osuToolsV2.Game.Legacy;
using osuToolsV2.GameInfo;
using osuToolsV2.Online.OsuApi.Version1;
using osuToolsV2.Online.OsuApi.Version2.Authenticating;
using osuToolsV2.Online.OsuApi.Version2.Beatmap.Difficulties;
using osuToolsV2.Online.OsuApi.Version2.Requests;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Mania.Mods;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreCalculators;
using osuToolsV2.StoryBoard;
using osuToolsV2.StoryBoard.Commands;
using osuToolsV2.Utils;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;


namespace osuToolsV2;

public class Class1
{
    static async Task Main(string[] args)
    {
        /*List<OsuScoreInfo> matchedScore = new List<OsuScoreInfo>();
        try
        {
            string y = @"D:\a\s\osu\osu!\scores.db.637247466398860153";
            //.Where(s => s.Mods.Any(m => m is OsuTargetPracticeMod))
            OsuScoreDb scoreDb = new OsuScoreDb(y, false);
            var x = scoreDb.Scores;
            foreach (var s in x)
            {
                var b = s.GetOsuBeatmap();
                if (b == null)
                {
                    continue;
                }

                bool titleMatch = b.Metadata.Title.Contains("Lost my pieces");
                bool versionMatch = b.Metadata.Version.Contains("Easy");

                if (titleMatch && versionMatch)
                {
                    matchedScore.Add(s);
                }
            }

            matchedScore.Sort((s1, s2)
                => s2.PlayTime.CompareTo(s1.PlayTime));
            matchedScore.ForEach(s =>
                Console.WriteLine(ScoreVisualUtil.BuildScoreDisplayString(s, s.ScoreProcessor)));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }*/

        OsuApiOAuthAuthenticator apiOAuthAuthenticator = new OsuApiOAuthAuthenticator();
        OsuApiOAuthAuthenticateParameters authAuthenticateParameters = new OsuApiOAuthAuthenticateParameters();
        authAuthenticateParameters.ClientCredentials.ClientId = "2208";
        authAuthenticateParameters.ClientCredentials.ClientSecret = "c388i4oeP8e8PxHZogS0faXkmhbuWmCvvRVLWnOl";
        authAuthenticateParameters.GrantParameters = OAuthGrantParameters.CreateApiGrantParameters();
        var token = apiOAuthAuthenticator.RequireTokenAsync(authAuthenticateParameters);
        ApiV2BeatmapDifficultyAttributesQueryParameter parameter = new ApiV2BeatmapDifficultyAttributesQueryParameter();
        parameter.BeatmapId = 2082541;
        ApiV2BeatmapDifficultyAttributesQueryRequest<ManiaBeatmapDifficultyAttribute> request =
            new ApiV2BeatmapDifficultyAttributesQueryRequest<ManiaBeatmapDifficultyAttribute>();
        var r = request.QueryAsync(token.Result.Data,
            OsuApiQueryContext<ApiV2BeatmapDifficultyAttributesQueryParameter>.Create(parameter)).Result.Data;



    }
}