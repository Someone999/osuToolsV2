namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class TimeRateModPropertyEvaluator : IModPropertyEvaluator<double>
{
    public double Evaluate(IEnumerable<Mod> mods)
    {
        return mods.OfType<IChangeTimeRateMod>().Aggregate(1.0, (current, mod) => current * mod.TimeRate);
    }
}