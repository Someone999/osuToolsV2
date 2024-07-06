namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class AllowsFailModPropertyEvaluator : IModPropertyEvaluator<bool>
{
    public bool Evaluate(IEnumerable<Mod> mods)
    {
        return mods.All(m => m.AllowsFail());
    }
}