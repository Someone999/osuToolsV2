using osuToolsV2.Game.Mods;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Reader;

public class ReplayObjectReader : IObjectReader<BinaryReader, Replay>
{
    private BinaryReader _reader;

    public ReplayObjectReader(Stream stream)
    {
        _reader = new BinaryReader(stream);
    }
    
    public ReplayObjectReader(string fileName) : 
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
        var legacyRuleset = (LegacyRuleset)Reader.ReadByte();
        var ruleset = Ruleset.FromLegacyRuleset(legacyRuleset);
        Replay replay = new Replay
        {
            LegacyRuleset = legacyRuleset,
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
                Mods = ModManager.FromInteger(Reader.ReadInt32(), ruleset, false)
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