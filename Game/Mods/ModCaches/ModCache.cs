namespace osuToolsV2.Game.Mods.ModCaches;

public class ModCache
{
    public ConflictModCache ConflictModCache { get; } = new ConflictModCache();
    public RulesetModCache RulesetModCache { get; } = new RulesetModCache();
}