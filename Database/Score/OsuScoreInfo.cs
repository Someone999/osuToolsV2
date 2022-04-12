using System.Diagnostics;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.Exceptions;
using osuToolsV2.Game.Legacy;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.ScoreInfo;

namespace osuToolsV2.Database.Score
{
    /// <summary>
    ///     scores.db中存储的成绩
    /// </summary>
    public class OsuScoreInfo
    {
        private static OsuBeatmapDb _currentDb = new OsuBeatmapDb();
        internal ModList InternalMods;

        /// <summary>
        ///     使用特定的数据来构造一个ScoreDBData对象
        /// </summary>
        /// <param name="ruleset">游戏模式</param>
        /// <param name="ver">游戏版本</param>
        /// <param name="bmd5">谱面的MD5</param>
        /// <param name="name">玩家名</param>
        /// <param name="rmd5">回放的MD5</param>
        /// <param name="count300">300的数量</param>
        /// <param name="count100">100的数量</param>
        /// <param name="count50">50的数量</param>
        /// <param name="countGeki">激或彩300的数量</param>
        /// <param name="count200">喝或200的数量</param>
        /// <param name="cmiss">Miss的数量</param>
        /// <param name="score">分数</param>
        /// <param name="maxcombo">最大连击</param>
        /// <param name="per">是否为Perfect</param>
        /// <param name="mods">使用了的Mod的整数形式</param>
        /// <param name="empty">一个必须为空的字符串</param>
        /// <param name="playtime">游玩的时间，以Tick为单位</param>
        /// <param name="verify">一个值必须为-1的整数</param>
        /// <param name="scoreId">ScoreId</param>
        public OsuScoreInfo(LegacyRuleset ruleset, int ver, string bmd5, string name, string rmd5, short count300, short count100,
            short count50, short countGeki, short count200, short cmiss, int score, short maxcombo, bool per, int mods,
            string empty, long playtime, int verify, long scoreId)
        {
            Ruleset = ruleset;
            //System.Windows.Forms.MessageBox.Show(Mode.ToString());
            ScoreInfo = Rulesets.Ruleset.FromLegacyRuleset(ruleset).CreateScoreInfo();
            GameVersion = ver;
            BeatmapMd5 = bmd5;
            ReplayMd5 = rmd5;
            PlayerName = name;
            ScoreInfo.CountGeki = countGeki;
            ScoreInfo.Count300 = count300;
            ScoreInfo.CountKatu = count200;
            ScoreInfo.Count100 = count100;
            ScoreInfo.Count50 = count50;
            ScoreInfo.CountMiss = cmiss;
            ScoreInfo.Score = score;
            ScoreInfo.MaxCombo = maxcombo;
            Perfect = per;
            InternalMods = 
                ModList.FromLegacyMods((LegacyGameMod)Enum.Parse(typeof(LegacyGameMod), mods.ToString()),
                    Rulesets.Ruleset.FromLegacyRuleset(ruleset));
            PlayTime = new DateTime(playtime);
            if (verify != -1 || !string.IsNullOrEmpty(empty)) 
                throw new ArgumentException("验证失败");
            ScoreId = scoreId;
            Debug.Assert(count300 + count100 + count50 + cmiss != 0);
            
        }
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

        public IScoreInfo ScoreInfo { get; private set; }
        /// <summary>
        ///     是否达成Perfect判定
        /// </summary>
        public bool Perfect { get; }

        /// <summary>
        ///     本次游戏使用的Mod
        /// </summary>
        public Mod[] Mods => InternalMods.ToArray();

        /// <summary>
        ///     游玩时间
        /// </summary>
        public DateTime PlayTime { get; }

        /// <summary>
        ///     分数ID
        /// </summary>
        public long ScoreId { get; }
        
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
        public OsuBeatmap? GetOsuBeatmap()
        {
            try
            {
                return _currentDb.Beatmaps?.FindByMd5(BeatmapMd5);
            }
            catch (BeatmapNotFoundException)
            {
                return null;
            }

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