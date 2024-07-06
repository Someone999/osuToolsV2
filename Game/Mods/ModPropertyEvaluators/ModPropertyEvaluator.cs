namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class ModPropertyEvaluator
{
    public IModPropertyEvaluator<double> ScoreMultiplierModPropertyEvaluator { get; set; } =
        new ScoreMultiplierModPropertyEvaluator();

    public IModPropertyEvaluator<double> TimeRateModPropertyEvaluator { get; set; } = new TimeRateModPropertyEvaluator();
    public IModPropertyEvaluator<bool> IsRankedModPropertyEvaluator { get; set; } = new IsRankedModPropertyEvaluator();
    public IModPropertyEvaluator<bool> AllowsFailModPropertyEvaluator { get; set; } = new AllowsFailModPropertyEvaluator();
    public IModPropertyEvaluator<bool> IsHiddenModPropertyEvaluator { get; set; } = new IsHiddenModPropertyEvaluator();
}