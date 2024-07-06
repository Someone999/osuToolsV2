namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public interface IModPropertyEvaluator<out TResult>
{
    TResult Evaluate(IEnumerable<Mod> mods);
}