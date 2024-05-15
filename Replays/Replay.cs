using System.Text;
using Microsoft.VisualBasic;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score;
using osuToolsV2.Writer;
using SharpCompress.Compressors;
using ZstdSharp;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;

namespace osuToolsV2.Replays;

public class Replay
{
    public LegacyRuleset LegacyRuleset { get; set; }
    public Ruleset GetRuleset() => Ruleset.FromLegacyRuleset(LegacyRuleset);
    public int GameVersion { get; set; }
    public ScoreInfo ScoreInfo { get; internal set; } = new ScoreInfo();
    public string ReplayMd5 { get; set; } = Md5Tools.InvalidMd5;
    public string BeatmapMd5 { get; set; } = Md5Tools.InvalidMd5;
    public string PlayerName { get; set; } = "";
    public DateTime PlayTime { get; set; } = DateTime.Now;
    internal readonly List<LifeBarGraph> LifeBarGraphsInternal = new List<LifeBarGraph>();

    public LifeBarGraph[] LifeBarGraphs => LifeBarGraphsInternal.ToArray();
    public long OnlineId { get; set; } = 0;

    public byte[] AdditionalData { get; set; } = Array.Empty<byte>();

    public Stream GetAdditionalDataAsStream()
    {
        return new MemoryStream(AdditionalData);
    }

    public void WriteToFile(string file)
    {
        FileStream fileStream = File.Open(file, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
        BinaryWriter writer = new BinaryWriter(fileStream);
        writer.Write((byte)LegacyRuleset);
        writer.Write(GameVersion);
        writer.Write((byte)0x0B);
        writer.Write(BeatmapMd5);
        writer.Write((byte)0x0B);
        writer.Write(PlayerName);
        writer.Write((byte)0x0B);
        writer.Write(ReplayMd5);
        writer.Write((short)ScoreInfo.Count300);
        writer.Write((short)ScoreInfo.Count100);
        writer.Write((short)ScoreInfo.Count50);
        writer.Write((short)ScoreInfo.CountGeki);
        writer.Write((short)ScoreInfo.CountKatu);
        writer.Write((short)ScoreInfo.CountMiss);
        writer.Write(ScoreInfo.Score ?? 0);
        writer.Write((short)ScoreInfo.MaxCombo);
        writer.Write(ScoreInfo.Perfect);
        writer.Write((int?)ScoreInfo.Mods?.ToLegacyMod() ?? 0);
        writer.Write((byte)0x0B);
        writer.Write(string.Join(",", from l in LifeBarGraphsInternal select l.ToFileFormat()) + ",");
        writer.Write(PlayTime.Ticks);
        writer.Write(AdditionalData.Length);
        writer.Write(AdditionalData);
        writer.Write(OnlineId);
        writer.Close();
    }

    public void WriteTo<TWriterType>(IObjectWriter<TWriterType> writer)
    {
        writer.Write((byte)LegacyRuleset);
        writer.Write(GameVersion);
        writer.Write((byte)0x0B);
        writer.Write(BeatmapMd5);
        writer.Write((byte)0x0B);
        writer.Write(PlayerName);
        writer.Write((byte)0x0B);
        writer.Write(ReplayMd5);
        writer.Write((short)ScoreInfo.Count300);
        writer.Write((short)ScoreInfo.Count100);
        writer.Write((short)ScoreInfo.Count50);
        writer.Write((short)ScoreInfo.CountGeki);
        writer.Write((short)ScoreInfo.CountKatu);
        writer.Write((short)ScoreInfo.CountMiss);
        writer.Write(ScoreInfo.Score ?? 0);
        writer.Write((short)ScoreInfo.MaxCombo);
        writer.Write(ScoreInfo.Perfect);
        writer.Write((int?)ScoreInfo.Mods?.ToLegacyMod() ?? 0);
        writer.Write((byte)0x0B);
        writer.Write(string.Join(",", from l in LifeBarGraphsInternal select l.ToFileFormat()) + ",");
        writer.Write(PlayTime.Ticks);
        writer.Write(AdditionalData.Length);
        writer.Write(AdditionalData);
        writer.Write(OnlineId);
        writer.Close();
    }
}