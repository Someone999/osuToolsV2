namespace osuToolsV2.LazyLoaders;

public interface ILazyLoader<out T>
{
    bool Loaded { get; }
    bool Loading { get; }
    T LoadObject();
}