namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class ScoreMultiplierModPropertyEvaluator : IModPropertyEvaluator<double>
{
    public double Evaluate(IEnumerable<Mod> mods)
    {
        return mods.Aggregate(1.0, (current, mod) => current * mod.ScoreMultiplier);
    }
}