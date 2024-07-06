namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class IsRankedModPropertyEvaluator : IModPropertyEvaluator<bool>
{
    public bool Evaluate(IEnumerable<Mod> mods)
    {
        return mods.All(m => m.IsRanked);
    }
}