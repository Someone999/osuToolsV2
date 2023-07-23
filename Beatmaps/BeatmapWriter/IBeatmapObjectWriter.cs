using osuToolsV2.Beatmaps.HitObjects;
using osuToolsV2.Beatmaps.TimingPoints;
using osuToolsV2.Writer;

namespace osuToolsV2.Beatmaps.BeatmapWriter;

public interface IBeatmapObjectWriter<out TWriterType> : IObjectWriter<Beatmap, TWriterType>
{
    IObjectWriter<Beatmap, TWriterType>? GeneralDataObjectWriter { get; }
    IObjectWriter<Beatmap, TWriterType>? EditorDataObjectWriter { get; }
    IObjectWriter<BeatmapMetadata, TWriterType>? MetadataObjectWriter { get; }
    IObjectWriter<DifficultyAttributes, TWriterType>? DifficultyObjectWriter { get; }
    IObjectWriter<Beatmap, TWriterType>? EventsObjectWriter { get; }
    IObjectWriter<List<TimingPoint>, TWriterType>? TimingPointObjectWriter { get; }
    IObjectWriter<List<IHitObject>, TWriterType>? HitObjectsObjectWriter { get; }
}

