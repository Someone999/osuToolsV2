using osuToolsV2.Database.Beatmap.BeatmapSearching;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Database.Beatmap
{
    /// <summary>
    ///     存储<see cref="OsuBeatmap" />的集合
    /// </summary>
    public class OsuBeatmapCollection
    {
        /// <summary>
        ///     谱面的ID的种类
        /// </summary>
        public enum BeatmapIdType
        {
            /// <summary>
            ///     谱面ID
            /// </summary>
            BeatmapId,

            /// <summary>
            ///     谱面集ID
            /// </summary>
            BeatmapSetId
        }

        public OsuBeatmapCollection(IEnumerable<OsuBeatmap> osuBeatmaps)
        {
            _beatmaps = new List<OsuBeatmap>(osuBeatmaps);
        }
        
        public OsuBeatmapCollection()
        {
            _beatmaps = new List<OsuBeatmap>();
        }

        private readonly List<OsuBeatmap> _beatmaps;

        /// <summary>
        ///     存储的<seealso cref="OsuBeatmap" />
        /// </summary>
        public IReadOnlyList<OsuBeatmap> Beatmaps => _beatmaps.AsReadOnly();

        /// <summary>
        ///     谱面的数量
        /// </summary>
        public int Count => _beatmaps.Count;

        /// <summary>
        ///     使用整数索引从列表中获取OsuBeatmap
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public OsuBeatmap this[int x] => _beatmaps[x];

        /// <summary>
        ///     检测指定谱面是否在列表中
        /// </summary>
        /// <param name="b">要检测的谱面</param>
        /// <returns>布尔值，指示谱面是否在列表中</returns>
        public bool Contains(OsuBeatmap b)
        {
            return _beatmaps.Contains(b);
        }

        internal void Add(OsuBeatmap b)
        {
            _beatmaps.Add(b);
        }

        /// <summary>
        ///     使用关键词搜索，可指定包含或不包含
        /// </summary>
        /// <param name="keyWord">关键词</param>
        /// <param name="option">是否包含关键词</param>
        /// <returns>包含搜索结果的谱面集合</returns>
        public OsuBeatmapCollection SearchByKeyword(string keyWord,
            BeatmapFindOption option = BeatmapFindOption.Contains)
        {
            var beatmaps = new HashSet<OsuBeatmap>();
           
            var keyword = keyWord.ToUpper();
            foreach (var beatmap in Beatmaps)
            {
                SearchTextList searchTextList = new SearchTextList();
                searchTextList.AddString(beatmap.ToString().ToUpper());
                searchTextList.AddString(beatmap.Metadata.Title.ToUpper());
                searchTextList.AddString(beatmap.Metadata.TitleUnicode.ToUpper());
                searchTextList.AddString(beatmap.Metadata.Artist.ToUpper());
                searchTextList.AddString(beatmap.Metadata.ArtistUnicode.ToUpper());
                searchTextList.AddString(beatmap.Metadata.Creator.ToUpper());
                searchTextList.AddString(beatmap.Metadata.Tags.ToUpper());
                searchTextList.AddString(beatmap.Metadata.Source.ToUpper());
                searchTextList.AddString(beatmap.Metadata.Version.ToUpper());
                
                switch (option)
                {
                    case BeatmapFindOption.Contains when keyword.StartsWith("${") && keyword.EndsWith("}"):
                    {
                        var accurateKeyword = keyword.Trim('$', '}', '{');
                        if (searchTextList.ExactMatch(accurateKeyword))
                        {
                            beatmaps.Add(beatmap);
                        }

                        break;
                    }
                    case BeatmapFindOption.Contains:
                    {
                        if (searchTextList.AllSubsequenceMatch(keyword))
                        {
                            beatmaps.Add(beatmap);
                        }

                        break;
                    }
                    case BeatmapFindOption.NotContains when keyword.StartsWith("${") && keyword.EndsWith("}"):
                    {
                        var accurateKeyword = keyword.Trim('$', '}', '{');
                        if (!searchTextList.ExactMatch(accurateKeyword))
                        {
                            beatmaps.Add(beatmap);
                        }
                        break;
                    }
                    case BeatmapFindOption.NotContains:
                    {
                        if (!searchTextList.AllSubsequenceMatch(keyword))
                        {
                            beatmaps.Add(beatmap);
                        }

                        break;
                    }
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option), option, null);
                }
            }

            return new OsuBeatmapCollection(beatmaps);
        }

        /// <summary>
        ///     根据谱面的ID查找谱面
        /// </summary>
        /// <param name="id">BeatmapID或BeatmapSetID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public OsuBeatmapCollection SearchById(int id, BeatmapIdType type = BeatmapIdType.BeatmapId)
        {
            var lst = new List<OsuBeatmap>();
            if (id == -1)
            {
                return new OsuBeatmapCollection();
            }
            switch (type)
            {
                case BeatmapIdType.BeatmapId:
                    lst.AddRange(from beatmap in _beatmaps where beatmap.BeatmapId == id select beatmap);
                    break;
                case BeatmapIdType.BeatmapSetId:
                    lst.AddRange(from beatmap in _beatmaps where beatmap.BeatmapSetId == id select beatmap);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            return new OsuBeatmapCollection(lst);
        }

        /// <summary>
        ///     使用MD5在谱面列表里搜索
        /// </summary>
        /// <param name="md5"></param>
        /// <returns></returns>
        public OsuBeatmap? FindByMd5(string md5)
        {
            return Beatmaps.FirstOrDefault(beatmap => beatmap.Metadata.Md5Hash == md5);
        }

        /// <summary>
        ///     使用游戏模式来搜索谱面，可指定包括或不包括
        /// </summary>
        /// <param name="ruleset"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public OsuBeatmapCollection SearchByRuleset(LegacyRuleset ruleset,
            BeatmapFindOption option = BeatmapFindOption.Contains)
        {
            
            HashSet<OsuBeatmap> beatmaps = new HashSet<OsuBeatmap>();
            foreach (var b in _beatmaps)
            {
                switch (option)
                {
                    case BeatmapFindOption.Contains:
                        if (b.Ruleset.LegacyRuleset == ruleset)
                        {
                            beatmaps.Add(b);
                        }
                        break;
                    case BeatmapFindOption.NotContains:
                        if (b.Ruleset.LegacyRuleset != ruleset)
                        {
                            beatmaps.Add(b);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option), option, null);
                }
            }

            return new OsuBeatmapCollection(beatmaps);
        }

        /// <summary>
        ///     获取列表的枚举器
        /// </summary>
        /// <returns></returns>
        public IEnumerator<OsuBeatmap> GetEnumerator()
        {
            return _beatmaps.GetEnumerator();
        }
    }
}