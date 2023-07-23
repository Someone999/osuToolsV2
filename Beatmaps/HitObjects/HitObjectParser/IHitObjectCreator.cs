namespace osuToolsV2.Beatmaps.HitObjects.HitObjectParser;

public interface IHitObjectCreator
{
    IHitObject CreateHitObject(string[] data, IBeatmap beatmap);
}