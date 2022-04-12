namespace osuToolsV2.Beatmaps.HitObjects.Mania;

public interface IManiaNote : IHitObject
{
     int Column { get; set; }
     int BeatmapColumn { get; set; }
}