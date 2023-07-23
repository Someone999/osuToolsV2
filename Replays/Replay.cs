using System.Text;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score;
using osuToolsV2.Writer;
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
    private readonly List<LifeBarGraph> _lifeBarGraphs  = new List<LifeBarGraph>();
    private readonly List<ReplayFrame> _replayFrames  = new List<ReplayFrame>();

    public LifeBarGraph[] LifeBarGraphs => _lifeBarGraphs.ToArray();
    public ReplayFrame[] ReplayFrames => _replayFrames.ToArray();
    private byte[] _additionalData = new byte[0];
    public long OnlineId { get; set; } = 0;
    public byte[] AdditionalData => _additionalData;

    public Stream GetAdditionalDataAsStream()
    {
        return new MemoryStream(AdditionalData);
    }

    private static byte[] DecompressData(byte[] data)
    {
        MemoryStream outputStream = new MemoryStream();
        var inputStream = new MemoryStream(data);
        inputStream.Position = 0;
        byte[] properties = new byte[5];
        _ = inputStream.Read(properties, 0, 5);
        byte[] buffer = new byte[8];
        _ = inputStream.Read(buffer, 0, 8);
        Decoder decoder = new Decoder();
        decoder.SetDecoderProperties(properties);
        long outputSize = BitConverter.ToInt64(buffer);
        long inputSize = inputStream.Length - inputStream.Position;
        decoder.Code(inputStream, outputStream, inputSize, outputSize, null);
        return outputStream.ToArray();
    }

    private static void ProcessFrameData(byte[] additionalBytes, List<ReplayFrame> replayFrames)
    {
        
        if (additionalBytes.Length <= 0)
        {
            return;
        }
        
        string frames = Encoding.UTF8.GetString(DecompressData(additionalBytes));
        string[] frameStrings = frames.Split(',');
        long offset = 0;
        foreach (var frameString in frameStrings)
        {
            string[] frameData = frameString.Split('|');
            if (frameData[0] == "-12345" || string.IsNullOrEmpty(frameString))
            {
                continue;
            }
            
            long millisecond = long.Parse(frameData[0]);
            offset += millisecond;
            double x = double.Parse(frameData[1]);
            double y = double.Parse(frameData[2]);
            ReplayButtonState buttonState = (ReplayButtonState)int.Parse(frameData[3]);
            replayFrames.Add(new ReplayFrame(millisecond, x, y, buttonState){Offset = offset});
        }
        
        replayFrames.Sort((x, y) => Math.Sign(x.Offset - y.Offset));
    }
    
    public static Replay ReadFromFile(string file)
    {
        Replay replay = new Replay();
        BinaryReader binaryReader = new BinaryReader(File.Open(file, FileMode.Open, FileAccess.Read, FileShare.Read));
        replay.LegacyRuleset = (LegacyRuleset)binaryReader.ReadByte();
        replay.GameVersion = binaryReader.ReadInt32();
        replay.BeatmapMd5 = binaryReader.ReadOsuString() ?? throw new FormatException();
        replay.PlayerName = binaryReader.ReadOsuString() ?? throw new FormatException();
        replay.ReplayMd5 = binaryReader.ReadOsuString() ?? throw new FormatException();
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
        replay._lifeBarGraphs.AddRange(from s in lifeBarGraphStrings select new LifeBarGraph(s));
        replay.PlayTime = new DateTime(binaryReader.ReadInt64());
        var additionalDataLength = binaryReader.ReadInt32();
        if (additionalDataLength != 0)
        {
            replay._additionalData = binaryReader.ReadBytes(additionalDataLength);
            ProcessFrameData(replay.AdditionalData, replay._replayFrames);
        }
        replay.OnlineId = binaryReader.ReadInt64();
        return replay;
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
        writer.Write(string.Join(",", from l in _lifeBarGraphs select l.ToFileFormat()) + ",");
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
        writer.Write(string.Join(",", from l in _lifeBarGraphs select l.ToFileFormat()) + ",");
        writer.Write(PlayTime.Ticks);
        writer.Write(_additionalData.Length);
        writer.Write(_additionalData);
        writer.Write(OnlineId);
        writer.Close();
    }
}