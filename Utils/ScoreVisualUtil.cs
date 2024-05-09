using System.Text;
using osuToolsV2.Database.Score;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Utils;

public class ScoreVisualUtil
{
    private static string BuildHitsDisplayString(ScoreInfo scoreInfo)
    {
        StringBuilder hitsString = new StringBuilder($"{scoreInfo.CountGeki}/");
        hitsString.Append(scoreInfo.CountKatu).Append('/');
        hitsString.Append(scoreInfo.Count300).Append('/');
        hitsString.Append(scoreInfo.Count100).Append('/');
        hitsString.Append(scoreInfo.Count50).Append('/');
        hitsString.Append(scoreInfo.CountMiss);
        return hitsString.ToString();
    }
    public static string BuildScoreDisplayString(ScoreInfo scoreInfo, IScoreProcessor scoreProcessor)
    {
        double accuracy = scoreProcessor.GetAccuracy(scoreInfo);
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append(accuracy.ToString("p2")).AppendLine();
        stringBuilder.Append(BuildHitsDisplayString(scoreInfo));
        return stringBuilder.ToString();
    }
    
    public static string BuildScoreDisplayString(OsuScoreInfo osuScoreInfo, IScoreProcessor scoreProcessor)
    {
        var scoreInfo = osuScoreInfo.ScoreInfo;
        double accuracy = scoreProcessor.GetAccuracy(scoreInfo);
        StringBuilder stringBuilder = new StringBuilder();
        
        stringBuilder.Append($"Player: {osuScoreInfo.PlayerName} ")
            .Append($"Played at: {osuScoreInfo.PlayTime} ")
            .Append($"Accuracy: {accuracy:p2}").AppendLine();
        
        stringBuilder.Append($"Score: {scoreInfo.Score} " + BuildHitsDisplayString(scoreInfo));
        return stringBuilder.ToString();
    }
}