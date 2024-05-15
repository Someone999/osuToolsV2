using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score;

namespace osuToolsV2.Writer.Replays;

public class ReplyObjectWriter : IObjectWriter<Replay, BinaryWriter>
{
    private BinaryWriter _writer;

    public ReplyObjectWriter(Stream stream)
    {
        _writer = new BinaryWriter(stream);
    }

    public ReplyObjectWriter(string fileName) : 
        this(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
    {
    }
    public void Write(Replay obj)
    {
        var writer = Writer;
        writer.Write((byte)obj.LegacyRuleset);
        writer.Write(obj.GameVersion);
        writer.Write((byte)0x0B);
        writer.Write(obj.BeatmapMd5);
        writer.Write((byte)0x0B);
        writer.Write(obj.PlayerName);
        writer.Write((byte)0x0B);
        writer.Write(obj.ReplayMd5);
        writer.Write((short)obj.ScoreInfo.Count300);
        writer.Write((short)obj.ScoreInfo.Count100);
        writer.Write((short)obj.ScoreInfo.Count50);
        writer.Write((short)obj.ScoreInfo.CountGeki);
        writer.Write((short)obj.ScoreInfo.CountKatu);
        writer.Write((short)obj.ScoreInfo.CountMiss);
        writer.Write(obj.ScoreInfo.Score ?? 0);
        writer.Write((short)obj.ScoreInfo.MaxCombo);
        writer.Write(obj.ScoreInfo.Perfect);
        writer.Write((int?)obj.ScoreInfo.Mods?.ToLegacyMod() ?? 0);
        writer.Write((byte)0x0B);
        writer.Write(string.Join(",", from l in obj.LifeBarGraphsInternal select l.ToFileFormat()) + ",");
        writer.Write(obj.PlayTime.Ticks);
        writer.Write(obj.AdditionalData.Length);
        writer.Write(obj.AdditionalData);
        writer.Write(obj.OnlineId);
    }

    public void Dispose()
    {
        Writer.Dispose();
    }

    public void Write(object obj)
    {
        Write((Replay)obj);
    }

    public bool NeedClose => true;
    public void Close()
    {
        Writer.Dispose();
        Writer.BaseStream.Dispose();
    }
    
    public BinaryWriter Writer {
        get => _writer;
        set
        {
            if (IsWriting)
            {
                return;
            }

            _writer = value;
        }
    }
    
    public bool IsWriting { get; private set; }
}