using osuToolsV2.Game.Mods;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Score;

namespace osuToolsV2.Reader;

public class ReplyObjectReader : IObjectReader<BinaryReader, Replay>
{
    private BinaryReader _reader;

    public ReplyObjectReader(Stream stream)
    {
        _reader = new BinaryReader(stream);
    }
    
    public ReplyObjectReader(string fileName) : 
        this(File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
    {
    }
    
    object? IObjectReader<BinaryReader>.Read()
    {
        return Read();
    }

    public Replay? Read()
    {
        IsReading = true;
        ScoreInfo scoreInfo = new ScoreInfo();
        Replay replay = new Replay
        {
            LegacyRuleset = (LegacyRuleset)Reader.ReadByte(),
            GameVersion = Reader.ReadInt32(),
            BeatmapMd5 = Reader.ReadOsuString() ?? throw new FormatException(),
            PlayerName = Reader.ReadOsuString() ?? throw new FormatException(),
            ReplayMd5 = Reader.ReadOsuString() ?? throw new FormatException(),
            ScoreInfo =
            {
                Count300 = Reader.ReadInt16(),
                Count100 = Reader.ReadInt16(),
                Count50 = Reader.ReadInt16(),
                CountGeki = Reader.ReadInt16(),
                CountKatu = Reader.ReadInt16(),
                CountMiss = Reader.ReadInt16(),
                Score = Reader.ReadInt32(),
                MaxCombo = Reader.ReadInt16(),
                Perfect = Reader.ReadByte() != 0,
                Mods = ModList.FromInteger(Reader.ReadInt32())
            }
        };
        string? lifeBarGraphStr = Reader.ReadOsuString();
        var lifeBarGraphStrings = lifeBarGraphStr?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
        replay.LifeBarGraphsInternal.AddRange(from s in lifeBarGraphStrings select new LifeBarGraph(s));
        replay.PlayTime = new DateTime(Reader.ReadInt64());
        var additionalDataLength = Reader.ReadInt32();
        if (additionalDataLength != 0)
        {
            replay.AdditionalData = Reader.ReadBytes(additionalDataLength);
        }

        replay.OnlineId = Reader.ReadInt64();
        IsReading = false;
        return replay;
    }

    public BinaryReader Reader
    {
        get => _reader;
        set
        {
            if (IsReading)
            {
                return;
            }
            
            _reader = value;
        }
    }

    public bool IsReading { get; private set; }
}