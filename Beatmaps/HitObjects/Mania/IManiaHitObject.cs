namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public interface IManiaHitObject : IHitObject
{
     int Column { get; set; }
     int BeatmapColumn { get; set; }
}