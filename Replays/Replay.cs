using System.Security.Cryptography;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.ScoreInfo;
using osuToolsV2.Writer;
using osuToolsV2.Writer.DefaultWriters;

namespace osuToolsV2.Replays;

public class Replay
{
    public LegacyRuleset LegacyRuleset { get; set; }
    public Ruleset GetRuleset() => Ruleset.FromLegacyRuleset(LegacyRuleset);
    public int GameVersion { get; set; }
    public IScoreInfo ScoreInfo { get; set; } = new OsuScoreInfo();
    public string ReplayMd5 { get; set; } = Md5Tools.InvalidMd5;
    public string BeatmapMd5 { get; set; } = Md5Tools.InvalidMd5;
    public string PlayerName { get; set; } = "";
    public DateTime PlayTime { get; set; } = DateTime.Now;
    private List<LifeBarGraph> LifeBarGraphs { get; } = new List<LifeBarGraph>();
    private byte[] _additionalData = new byte[0];
    public long OnlineId { get; set; } = 0;
    public byte[] AdditionalData => _additionalData;

    public static Replay ReadFromFile(string file)
    {
        Replay replay = new Replay();
        BinaryReader binaryReader = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read));
        replay.LegacyRuleset = (LegacyRuleset)binaryReader.ReadByte();
        replay.GameVersion = binaryReader.ReadInt32();
        replay.BeatmapMd5 = binaryReader.ReadOsuString() ?? throw new FormatException();
        replay.PlayerName = binaryReader.ReadOsuString() ?? throw new FormatException();
        replay.ReplayMd5 = binaryReader.ReadOsuString() ?? throw new FormatException();
        replay.ScoreInfo = replay.GetRuleset().CreateScoreInfo();
        replay.ScoreInfo.Count300 = binaryReader.ReadInt16();
        replay.ScoreInfo.Count100 = binaryReader.ReadInt16();
        replay.ScoreInfo.Count50 = binaryReader.ReadInt16();
        replay.ScoreInfo.CountGeki = binaryReader.ReadInt16();
        replay.ScoreInfo.CountKatu = binaryReader.ReadInt16();
        replay.ScoreInfo.CountMiss = binaryReader.ReadInt16();
        replay.ScoreInfo.Score = binaryReader.ReadInt32();
        replay.ScoreInfo.MaxCombo = binaryReader.ReadInt16();
        replay.ScoreInfo.Perfect = binaryReader.ReadByte() != 0;
        replay.ScoreInfo.Mods = ModList.FromInteger(binaryReader.ReadInt32());
        string? lifeBarGraphStr = binaryReader.ReadOsuString();
        var lifeBarGraphStrings = lifeBarGraphStr?.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
        replay.LifeBarGraphs.AddRange(from s in lifeBarGraphStrings select new LifeBarGraph(s));
        replay.PlayTime = new DateTime(binaryReader.ReadInt64());
        var additionalDataLength = binaryReader.ReadInt32();
        if (additionalDataLength != 0)
        {
            replay._additionalData = binaryReader.ReadBytes(additionalDataLength);
        }
        replay.OnlineId = binaryReader.ReadInt64();
        return replay;
    }

    public void WriteToFile(string file)
    {
        BinaryWriter writer = new BinaryWriter(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read));
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
        writer.Write(string.Join(",", from l in LifeBarGraphs select l.ToFileFormat()) + ",");
        writer.Write(PlayTime.Ticks);
        writer.Write(_additionalData.Length);
        writer.Write(_additionalData);
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
        writer.Write(string.Join(",", from l in LifeBarGraphs select l.ToFileFormat()) + ",");
        writer.Write(PlayTime.Ticks);
        writer.Write(_additionalData.Length);
        writer.Write(_additionalData);
        writer.Write(OnlineId);
        writer.Close();
    }
}