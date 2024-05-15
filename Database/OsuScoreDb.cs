using osuToolsV2.Database.Score;
using osuToolsV2.Exceptions;
using osuToolsV2.Game.Legacy;
using osuToolsV2.GameInfo;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Database
{
    /// <summary>
    ///  Get all scores from scores.db
    ///  Use <see cref="osuToolsV2.Reader.OsuScoreDbObjectReader"/> to create a <see cref="OsuScoreDb"/>> object/>
    /// </summary>
    public class OsuScoreDb
    {
        internal OsuScoreDb()
        {
        }
        internal List<OsuScoreInfo> ScoresInternal = new List<OsuScoreInfo>();
        /// <summary>
        ///     存储的分数
        /// </summary>
        public IReadOnlyList<OsuScoreInfo> Scores => ScoresInternal.AsReadOnly();

        /// <summary>
        ///     scores,db中的头部数据
        /// </summary>
        public ScoreManifest Manifest { get; internal set; } = new ScoreManifest(-1);
    }
}