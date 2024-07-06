using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Database.Beatmap
{
    /// <summary>
    ///     包含指定模式的指定Mods与Star的键值对。
    /// </summary>
    public class ModDifficultyPairs
    {
        private readonly Dictionary<LegacyRuleset, Dictionary<int, double>> _difficulties =
            new Dictionary<LegacyRuleset, Dictionary<int, double>>();

        /// <summary>
        ///     使用游戏模式获取难度字典
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Dictionary<int, double> this[LegacyRuleset mode]
        {
            get => _difficulties.TryGetValue(mode, out var diff)
                    ? diff
                    : new Dictionary<int, double> { { 0, 0 } };

            internal set => _difficulties[mode] = value;
        }

        internal void Add(LegacyRuleset mode, int modCombine, double stars)
        {
            _difficulties[mode].Add(modCombine, stars);
        }

        /// <summary>
        ///     获取指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <returns></returns>
        public double GetStars(LegacyRuleset mode, int modCombine)
        {
            if (!_difficulties.TryGetValue(mode, out var modDifficultyPair))
            {
                return 0;
            }

            var r = modDifficultyPair.TryGetValue(modCombine, out var val);
            return r ? val : 0;
        }

        /// <summary>
        ///     设置指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <param name="stars" />
        /// <returns></returns>
        public void SetStar(LegacyRuleset mode, int modCombine, double stars)
        {
            if (!_difficulties.TryGetValue(mode, out var modDifficultyPair))
            {
                return;
            }

            modDifficultyPair[modCombine] = stars;
        }

        /// <summary>
        ///     将模式的难度字典更改为指定字典
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dict"></param>
        public void SetModeDict(LegacyRuleset mode, Dictionary<int, double> dict)
        {
            _difficulties[mode] = dict;
        }
    }
}