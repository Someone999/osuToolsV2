﻿using osuToolsV2.Rulesets.Legacy;

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
        public OsuBeatmapCollection Find(string keyWord,
            BeatmapFindOption option = BeatmapFindOption.Contains)
        {
            var b = new OsuBeatmapCollection();
            var keyword = keyWord.ToUpper();
            foreach (var beatmap in Beatmaps)
            {
                var allinfo = beatmap.ToString().ToUpper() + " " + beatmap.Metadata.Source.ToUpper() + " " +
                              beatmap.Metadata.Tags.ToUpper() + " " + beatmap.Metadata.Creator.ToUpper();
                if (option == BeatmapFindOption.Contains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        var newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Metadata.Title.ToUpper() == newkeyw || beatmap.Metadata.TitleUnicode.ToUpper() == newkeyw ||
                            beatmap.Metadata.Artist.ToUpper() == newkeyw || beatmap.Metadata.ArtistUnicode.ToUpper() == newkeyw ||
                            beatmap.Metadata.Creator.ToUpper() == newkeyw || beatmap.Metadata.Tags.ToUpper() == newkeyw ||
                            beatmap.Metadata.Source.ToUpper() == newkeyw ||
                            beatmap.Metadata.Version.ToUpper() == newkeyw)
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                    }
                    else if (allinfo.Contains(keyword))
                    {
                        b.Add(beatmap);
                    }
                }

                if (option == BeatmapFindOption.NotContains)
                {
                    if (keyword.StartsWith("${") && keyword.EndsWith("}"))
                    {
                        var newkeyw = keyword.Trim('$', '}', '{');
                        if (beatmap.Metadata.Title.ToUpper() != newkeyw && beatmap.Metadata.TitleUnicode.ToUpper() != newkeyw &&
                            beatmap.Metadata.Artist.ToUpper() != newkeyw && beatmap.Metadata.ArtistUnicode.ToUpper() != newkeyw &&
                            beatmap.Metadata.Creator.ToUpper() != newkeyw && beatmap.Metadata.Tags.ToUpper() != newkeyw &&
                            beatmap.Metadata.Source.ToUpper() != newkeyw &&
                            beatmap.Metadata.Version.ToUpper() != newkeyw)
                            if (!b.Contains(beatmap))
                                b.Add(beatmap);
                    }
                    else if (!allinfo.Contains(keyword))
                    {
                        b.Add(beatmap);
                    }
                }
            }

            return b;
        }

        /// <summary>
        ///     根据谱面的ID查找谱面
        /// </summary>
        /// <param name="id">BeatmapID或BeatmapSetID</param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<OsuBeatmap> Find(int id, BeatmapIdType type = BeatmapIdType.BeatmapId)
        {
            var lst = new List<OsuBeatmap>();
            if (id == -1)
            {
                return new List<OsuBeatmap>();
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
            
            return lst;
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
        public OsuBeatmapCollection Find(LegacyRuleset ruleset,
            BeatmapFindOption option = BeatmapFindOption.Contains)
        {
            var bc = new OsuBeatmapCollection();
            foreach (var b in _beatmaps)
            {
                switch (option)
                {
                    case BeatmapFindOption.Contains:
                        if (b.Ruleset.LegacyRuleset == ruleset && !bc.Contains(b))
                        {
                            bc.Add(b);
                        }
                        break;
                    case BeatmapFindOption.NotContains:
                        if (b.Ruleset.LegacyRuleset != ruleset && !bc.Contains(b))
                        {
                            bc.Add(b);
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(option), option, null);
                }
            }

            return bc;
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