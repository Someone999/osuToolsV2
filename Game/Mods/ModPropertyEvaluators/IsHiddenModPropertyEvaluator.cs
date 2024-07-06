namespace osuToolsV2.Game.Mods.ModPropertyEvaluators;

public class IsHiddenModPropertyEvaluator : IModPropertyEvaluator<bool>
{
    public bool Evaluate(IEnumerable<Mod> mods)
    {
        return mods.Any(m => m is HiddenMod or FlashlightMod or FadeInMod);
    }
}