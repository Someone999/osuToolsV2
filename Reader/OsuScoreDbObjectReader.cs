using osuToolsV2.Database;
using osuToolsV2.Database.Score;
using osuToolsV2.Game.Mods;
using osuToolsV2.GameInfo;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;
using osuToolsV2.Rulesets.Osu.Mods;
using osuToolsV2.Score;

namespace osuToolsV2.Reader;

public class OsuScoreDbObjectReader : IObjectReader<BinaryReader, OsuScoreDb>
{
    private string _dbFilePath = "";
    private BinaryReader _reader;

    public OsuScoreDbObjectReader(Stream stream)
    {
        _reader = new BinaryReader(stream);
    }

    public OsuScoreDbObjectReader(string dbFilePath)
    {
        _dbFilePath = dbFilePath;
        var stream = File.Open(dbFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        _reader = new BinaryReader(stream);
    }
    
    public OsuScoreDbObjectReader() : this(Path.Combine(OsuInfoOld.GetInstance().OsuDirectory, "scores.db"))
    {
    }
    
    
    object? IObjectReader<BinaryReader>.Read()
    {
        return Read();
    }

    public OsuScoreDb? Read()
    {
        OsuScoreDb scoreDb = new OsuScoreDb();
        try
        {
            
            scoreDb.Manifest = ReadManifest();
            scoreDb.ScoresInternal = ReadScores();
            return scoreDb;
        }
        catch (Exception e)
        {
            Reader.Dispose();
            return scoreDb;
        }
       
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

    private ScoreManifest ReadManifest()
    {
        return new ScoreManifest(Reader.ReadInt32());
    }

    private List<OsuScoreInfo> ReadScores()
    {
        List<OsuScoreInfo> scoreInfos = new List<OsuScoreInfo>();
        var beatmapCount = Reader.ReadInt32();
        for (int i = 0; i < beatmapCount; i++)
        {
            ReadBeatmapScoreInfo(scoreInfos);
        }

        return scoreInfos;
    }
    

    private void ReadBeatmapScoreInfo(List<OsuScoreInfo> scoreInfos)
    {
        //Beatmap MD5
        Reader.ReadOsuString();
        var scoreCount = Reader.ReadInt32();
        for (int i = 0; i < scoreCount; i++)
        {
            scoreInfos.Add(ReadScoreInfo());
        }
    }

    private OsuScoreInfo ReadScoreInfo()
    {
        var ruleset = Ruleset.FromLegacyRuleset((LegacyRuleset)Reader.ReadByte());
        int gameVersion = Reader.ReadInt32();
        string beatmapMd5 = Reader.ReadOsuString();
        string playerName = Reader.ReadOsuString();
        string replayMd5 = Reader.ReadOsuString();
        short count300 = Reader.ReadInt16();
        short count100 = Reader.ReadInt16();
        short count50 = Reader.ReadInt16();
        short countGeki = Reader.ReadInt16();
        short countKatu = Reader.ReadInt16();
        short countMiss = Reader.ReadInt16();
        int score = Reader.ReadInt32();
        short maxCombo = Reader.ReadInt16();
        bool perfect = Reader.ReadBoolean();
        var mods = ModManager.FromInteger(Reader.ReadInt32(), ruleset, false);
        var verifyString = Reader.ReadOsuString();
        if (verifyString != string.Empty)
        {
            const string stringNotEmptyInvalidFormatError = 
                "Expected an empty string but received a non-empty string. Invalid score format.";
            throw new FormatException(stringNotEmptyInvalidFormatError);
        }
        
        var timestamp = Reader.ReadInt64();
        var verifyInteger = Reader.ReadInt32();
        if (verifyInteger != -1)
        {
            const string integerNotMinusOneFormatError = 
                "Expected -1 but received a non-empty string. Invalid score format.";
            throw new FormatException(integerNotMinusOneFormatError);
            
        }
        var onlineId = Reader.ReadInt64();
        double? targetPracticeAcc = null;
        
        if (ruleset.LegacyRuleset == LegacyRuleset.Osu && mods.Any(m => m is OsuTargetPracticeMod))
        {
            targetPracticeAcc = Reader.ReadDouble();
        }

        ScoreInfo scoreInfo = new ScoreInfo()
        {
            Ruleset = ruleset,
            Mods = mods,
            CountGeki = countGeki,
            CountKatu = countKatu,
            Count300 = count300,
            Count100 = count100,
            Count50 = count50,
            CountMiss = countMiss,
            MaxCombo = maxCombo,
            PlayerName = playerName,
            Score = score,
            Perfect = perfect,
            PlayTime = new DateTime(timestamp)
        };

        return new OsuScoreInfo(scoreInfo, gameVersion, beatmapMd5, replayMd5)
        {
            ScoreId = onlineId,
            TargetPracticeTotalAccuracy = targetPracticeAcc
        };
    }
}