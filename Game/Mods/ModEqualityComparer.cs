namespace osuToolsV2.Game.Mods
{
    /// <summary>
    ///     Mod的比较器
    /// </summary>
    internal class ModEqualityComparer : IEqualityComparer<Mod>
    {
        public bool Equals(Mod? a, Mod? b)
        {
            if (a is null && b is null)
            {
                return true;
            }

            var aType = a?.GetType();
            var bType = b?.GetType();
            return aType == bType;
        }

        public int GetHashCode(Mod m)
        {
            return m.GetHashCode();
        }
    }
}