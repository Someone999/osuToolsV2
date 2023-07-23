using osuToolsV2.Beatmaps.BeatmapReader;
using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.GameInfo;
using osuToolsV2.Rulesets;
using osuToolsV2.Score;
using BeatmapMetadata = osuToolsV2.Beatmaps.BeatmapMetadata;
using DifficultyAttributes = osuToolsV2.Beatmaps.DifficultyAttributes;
using IBeatmap = osuToolsV2.Beatmaps.IBeatmap;
using OsuScoreInfo = osuToolsV2.Database.Score.OsuScoreInfo;

namespace osuToolsV2.Database.Beatmap
{
    /// <summary>
    ///     谱面，存储的信息多于Beatmap类
    /// </summary>
    public class OsuBeatmap : IBeatmap
    {
        internal List<OsuBeatmapTimingPoint> TimingPoints = new List<OsuBeatmapTimingPoint>();

        /// <summary>
        ///     谱面的难度星级
        /// </summary>
        public double? Stars { get; set; }

        public List<IHitObject>? HitObjects { get; set; }

        /// <summary>
        ///     存放谱面的文件夹的名称
        /// </summary>
        public string FolderName { get; internal set; } = string.Empty;

        /// <summary>
        ///     谱面的游戏模式
        /// </summary>
        public Ruleset Ruleset { get; set; } = new EmptyRuleset();


        public BeatmapMetadata Metadata { get; set; } = new BeatmapMetadata();

        /// <summary>
        ///     谱面的状态
        /// </summary>
        public OsuBeatmapStatus BeatmapStatus { get; internal set; } = OsuBeatmapStatus.Unknown;

        /// <summary>
        ///     谱面中圈圈的数量
        /// </summary>
        public short HitCircle { get; internal set; }

        /// <summary>
        ///     谱面中滑条的数量
        /// </summary>
        public short Slider { get; internal set; }

        /// <summary>
        ///     谱面中转盘的数量
        /// </summary>
        public short Spinner { get; internal set; }

        /// <summary>
        ///     谱面上次修改的时间
        /// </summary>
        public DateTime LastModificationTime { get; internal set; }

        /// <summary>
        ///     谱面的长度
        /// </summary>
        public TimeSpan DrainTime { get; internal set; }

        /// <summary>
        ///     音频的长度
        /// </summary>
        public TimeSpan TotalTime { get; internal set; }

        /// <summary>
        ///     音频的预览点
        /// </summary>
        public TimeSpan PreviewPoint { get; internal set; }

        public int PreviewTime 
        { 
            get => (int)PreviewPoint.TotalMilliseconds;
            set => throw new NotSupportedException();
        }

        public DifficultyAttributes DifficultyAttributes { get; set; } = new DifficultyAttributes();

        /// <summary>
        ///     谱面的时间点
        /// </summary>
        public IReadOnlyList<OsuBeatmapTimingPoint> TimePoints => TimingPoints.AsReadOnly();

        /// <summary>
        ///     谱面ID
        /// </summary>
        public int? BeatmapId { get; set; }

        /// <summary>
        ///     谱面集的ID
        /// </summary>
        public int? BeatmapSetId { get; set; }

        /// <summary>
        ///     包含部分Mod与难度星级的字典
        /// </summary>
        public DifficultyRate ModStarPair { get; internal set; } = new DifficultyRate();

        /// <summary>
        /// </summary>
        public int ThreadId { get; internal set; }

        /// <summary>
        ///     将OsuBeatmap转换成字符串形式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"{Metadata.Artist} - {Metadata.Title} [{Metadata.Version}]";
        }

        /// <summary>
        ///     将OsuBeatmap转化成<seealso cref="Beatmap" />
        /// </summary>
        /// <returns></returns>
        public osuToolsV2.Beatmaps.Beatmap ToBeatmap()
        {
            OsuInfo info = new OsuInfo();
            return new DefaultBeatmapDecoder(Path.Combine(info.BeatmapDirectory, FolderName,
                Metadata.BeatmapFileName ?? "")).Decode();
        }

        /// <summary>
        ///     使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool operator ==(OsuBeatmap? a, OsuBeatmap? b)
        {
            if (a is null && b is null)
                return true;
            if (a is null || b is null)
                return false;
            return a.Metadata.Md5Hash == b.Metadata.Md5Hash;
        }

        /// <summary>
        ///     使用MD5判断两个OsuBeatmap是否相同
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        public static bool operator !=(OsuBeatmap? a, OsuBeatmap? b)
        {
            if (a is null && b is null)
                return false;
            if (a is null || b is null) 
                return true;
            return a.Metadata.Md5Hash != b.Metadata.Md5Hash;
        }

        private double _bpm;
        /// <summary>
        /// 谱面中出现次数最多的Bpm
        /// </summary>
        public double Bpm
        {
            get
            {
                if (_bpm != 0)
                {
                    return _bpm;
                }
                Dictionary<double, double> bpmTime = new Dictionary<double, double>();
                var tmPts = TimePoints.Where(t => t.Bpm > 0 && !t.Inherit);
                OsuBeatmapTimingPoint? cur, nxt;
                OsuBeatmapTimingPoint? lastCur = null;
                for (var i = 0; i < TimingPoints.Count; i++)
                {
                    cur = TimePoints[i];
                    nxt = i < TimingPoints.Count - 1
                        ? TimingPoints[i + 1]
                        : null;

                    if (!cur.Inherit)
                    {
                        lastCur = cur;
                    }

                    if (nxt is not {Inherit: false} || lastCur == null)
                    {
                        continue;
                    }
                    
                    double roundedBpm = Math.Round(lastCur.Bpm, 2);
                    double duration = nxt.Offset - lastCur.Offset;
                    if (bpmTime.ContainsKey(roundedBpm))
                    {
                        bpmTime[roundedBpm] += duration;
                    }
                    else
                    {
                        bpmTime.Add(roundedBpm, duration);
                    }
                }
                if (bpmTime.Count == 0)
                {
                    bpmTime.Add(lastCur?.Bpm ?? 0, 1);
                }
                var most = from bpm in bpmTime where bpm.Key > 0 orderby bpm.Value descending select bpm;
                _bpm = most.First().Key;

                return _bpm;
            }
            set => _bpm = value;
        }

        public int GetHitObjectCount(ScoreInfo scoreInfo)
        {
            return 0;
        }

        public int GetMaxCombo(ScoreInfo scoreInfo)
        {
            return 0;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override bool Equals(object? obj)
        {
            if (obj is OsuBeatmap b)
            {
                return b.Metadata.Md5Hash == Metadata.Md5Hash;
            }
            return false;
        }
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        public override int GetHashCode()
        {
            return Metadata.Md5Hash?.GetHashCode() ?? 0;
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与谱面Md5匹配的的成绩
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<OsuScoreInfo> GetScores()
        {
            OsuScoreDb scoreDb = new OsuScoreDb();
            List<OsuScoreInfo> info = new List<OsuScoreInfo>();
            foreach (var score in scoreDb.Scores)
            {
                if(score.BeatmapMd5 == Metadata.Md5Hash)
                    info.Add(score);
            }
            return info.AsReadOnly();
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与符合条件的成绩
        /// </summary>
        /// <param name="comparison"></param>
        /// <returns></returns>

        public IReadOnlyList<OsuScoreInfo> GetScores(Comparison<OsuScoreInfo> comparison)
        {
            var lst = new List<OsuScoreInfo>(GetScores());
            lst.Sort(comparison);
            return lst.AsReadOnly();
        }
        /// <summary>
        /// 在默认的成绩数据库中寻找与符合条件的成绩
        /// </summary>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public IReadOnlyList<OsuScoreInfo> GetScores(IComparer<OsuScoreInfo> comparer)
        {
            var lst = new List<OsuScoreInfo>(GetScores());
            lst.Sort(comparer);
            return lst.AsReadOnly();
        }
    }
}