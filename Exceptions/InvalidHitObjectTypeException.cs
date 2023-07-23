using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Rulesets;

namespace osuToolsV2.Exceptions;

public class InvalidHitObjectTypeException : osuToolsExceptionBase
{
    protected InvalidHitObjectTypeException()
    {
    }

    public InvalidHitObjectTypeException(Ruleset ruleset, OriginalHitObjectType originalHitObjectType)
    :base($"Ruleset {ruleset.Name} can not parse hitobject {originalHitObjectType}")
    {
        
    }
    
    public InvalidHitObjectTypeException(string ruleset, OriginalHitObjectType originalHitObjectType)
        :base($"Ruleset {ruleset} can not parse hitobject {originalHitObjectType}")
    {
        
    }
    
    public InvalidHitObjectTypeException(string ruleset, string originalHitObjectType)
        :base($"Ruleset {ruleset} can not parse hitobject {originalHitObjectType}")
    {
        
    }
    
    public InvalidHitObjectTypeException(string msg) : base(msg)
    {
    }

    public InvalidHitObjectTypeException(string msg, Exception? innerException) : base(msg, innerException)
    {
    }
}