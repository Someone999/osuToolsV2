using System.Diagnostics;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.Exceptions;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;
using osuToolsV2.Reader;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score;
using osuToolsV2.Score.ScoreProcessor;

namespace osuToolsV2.Database.Score
{
    /// <summary>
    ///     scores.db中存储的成绩
    /// </summary>
    public class OsuScoreInfo
    {
        public OsuScoreInfo(ScoreInfo scoreInfo, int gameVersion, string beatmapMd5, string replayMd5)
        {
            ScoreInfo = scoreInfo;
            var legacyRuleset = scoreInfo.Ruleset?.LegacyRuleset;
            if (legacyRuleset == null)
            {
                throw new InvalidOperationException("Unknown ruleset");
            }
                
            Ruleset = legacyRuleset.Value;
            ScoreProcessor = Rulesets.Ruleset.FromLegacyRuleset(Ruleset).CreateScoreProcessor();
            PlayerName = scoreInfo.PlayerName ?? "";
            PlayTime = scoreInfo.PlayTime;
            GameVersion = gameVersion;
            BeatmapMd5 = beatmapMd5;
            ReplayMd5 = replayMd5;
        }

        public IScoreProcessor ScoreProcessor { get; set; }
        
        /// <summary>
        ///     游戏版本
        /// </summary>
        public int GameVersion { get; }

        /// <summary>
        ///     游戏模式
        /// </summary>
        public LegacyRuleset Ruleset { get; }

        /// <summary>
        ///     谱面的MD5
        /// </summary>
        public string BeatmapMd5 { get; }

        /// <summary>
        ///     玩家名
        /// </summary>
        public string PlayerName { get; }

        /// <summary>
        ///     回放的MD5
        /// </summary>
        public string ReplayMd5 { get; }

        public ScoreInfo ScoreInfo { get; private set; }

        /// <summary>
        ///     是否达成Perfect判定
        /// </summary>
        public bool Perfect => ScoreInfo.Perfect;

        /// <summary>
        ///     本次游戏使用的Mod
        /// </summary>
        public Mod[] Mods => ScoreInfo.Mods?.ToArray() ?? Array.Empty<Mod>();

        /// <summary>
        ///     游玩时间
        /// </summary>
        public DateTime PlayTime { get; }

        /// <summary>
        ///     分数ID
        /// </summary>
        public long ScoreId { get; internal set; }
        
        public double? TargetPracticeTotalAccuracy { get; internal set; }
        
        /// <summary>
        ///     确定指定的对象是否等于当前对象。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object? obj)
        {
            if (obj is OsuScoreInfo info)
            {
                return info.ReplayMd5 == ReplayMd5 && info.BeatmapMd5 == BeatmapMd5;
            }

            return false;
        }

        /// <summary>
        ///     默认哈希函数
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return ReplayMd5.GetHashCode();
        }
        /// <summary>
        /// 获取MD5与成绩BeatmapMD5相同的谱面
        /// </summary>
        /// <returns>
        /// 成功返回相应谱面，失败返回null
        /// </returns>
        public OsuBeatmap? GetOsuBeatmap(OsuBeatmapDb beatmapDb)
        {
            return beatmapDb.Beatmaps.FindByMd5(BeatmapMd5);
        }

        /// <summary>
        ///     使用ReplayMD5判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuScoreInfo? a, OsuScoreInfo? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return a.Equals(b);
        }

        /// <summary>
        ///     使用时间判断两个成绩是否为同一个成绩
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator !=(OsuScoreInfo? a, OsuScoreInfo? b)
        {
            if (a is null && b is null) return true;
            if (a is null || b is null) return false;
            return !a.Equals(b);
        }
    }

    
}