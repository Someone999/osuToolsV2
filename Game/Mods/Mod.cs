using osuToolsV2.Beatmaps;

namespace osuToolsV2.Game.Mods
{
    public abstract class Mod
    {
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract string ShortName { get; }
        public virtual double ScoreMultiplier { get;  } = 1.0;
        public virtual ModType ModType { get;  } = ModType.Fun;
        public virtual void Apply(DifficultyAttributes difficulty)
        { }
        public virtual bool IsRanked => true;
        public virtual bool AllowsFail() => true;
        public virtual Type[] ConflictMods => Type.EmptyTypes;
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(obj, null))
            {
                return false;
            }
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            if (obj is Mod mod)
            {
                return GetType() == obj.GetType();
            }

            return false;
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
    }
}
