
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Database.Beatmap
{
    /// <summary>
    ///     包含指定模式的指定Mods与Star的键值对。
    /// </summary>
    public class DifficultyRate
    {
        internal Dictionary<LegacyRuleset, Dictionary<int, double>> Difficuties =
            new Dictionary<LegacyRuleset, Dictionary<int, double>>();

        /// <summary>
        ///     使用游戏模式获取难度字典
        /// </summary>
        /// <param name="mode"></param>
        /// <returns></returns>
        public Dictionary<int, double> this[LegacyRuleset mode]
        {
            get
            {
                try
                {
                    return Difficuties[mode];
                }
                catch
                {
                    return new Dictionary<int, double> { { 0, 0 } };
                }
            }
        }

        internal void Add(LegacyRuleset mode, int modCombine, double stars)
        {
            Difficuties[mode].Add(modCombine, stars);
        }

        /// <summary>
        ///     获取指定Mod在指定模式下对应的星星数
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="modCombine"></param>
        /// <returns></returns>
        public double GetStars(LegacyRuleset mode, int modCombine)
        {
            try
            {
                return Difficuties[mode][modCombine];
            }
            catch
            {
                return 0;
            }
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
            try
            {
                Difficuties[mode][modCombine] = stars;
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     将模式的难度字典更改为指定字典
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="dict"></param>
        public void SetModeDict(LegacyRuleset mode, Dictionary<int, double> dict)
        {
            Difficuties[mode] = dict;
        }
    }
}