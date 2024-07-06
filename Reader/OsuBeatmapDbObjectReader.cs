using System.Security.Cryptography;
using osuToolsV2.Beatmaps;
using osuToolsV2.Database;
using osuToolsV2.Database.Beatmap;
using osuToolsV2.GameInfo;
using osuToolsV2.Replays;
using osuToolsV2.Rulesets;
using osuToolsV2.Rulesets.Legacy;

namespace osuToolsV2.Reader;

public class OsuBeatmapDbObjectReader : IObjectReader<BinaryReader, OsuBeatmapDb>
{
    private string _dbPath;
    private bool _manifestHasRead;
    private BinaryReader _reader;


    public OsuBeatmapDbObjectReader(Stream stream)
    {
        _dbPath = "";
        _reader = new BinaryReader(stream);
    }
    
    public OsuBeatmapDbObjectReader(string dbPath)
    {
        _dbPath = dbPath;
        var stream = File.Open(dbPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        _reader = new BinaryReader(stream);
    }

    public OsuBeatmapDbObjectReader() : this(Path.Combine(OsuInfo.GetInstance().OsuDirectory, "osu!.db"))
    {
    }
    
    
    
    object? IObjectReader<BinaryReader>.Read()
    {
        return Read();
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

    void ReadManifest(OsuBeatmapDb beatmapDb)
    {
        beatmapDb.Manifest = new OsuManifest();
        if (_manifestHasRead)
        {
            return;
        }
        
        beatmapDb.Manifest.Version = Reader.ReadInt32();
        beatmapDb.Manifest.FolderCount = Reader.ReadInt32();
        beatmapDb.Manifest.AccountUnlocked = Reader.ReadBoolean();
        beatmapDb.Manifest.AccountUnlockTime = new DateTime(Reader.ReadInt64());
        beatmapDb.Manifest.PlayerName = Reader.ReadOsuString();
        beatmapDb.Manifest.BeatmapCount = Reader.ReadInt32();
        _manifestHasRead = true;
    }

    private BeatmapMetadata ReadBeatmapMetadata()
    {
        var artist = Reader.ReadOsuString();
        var artistUnicode = Reader.ReadOsuString();
        var title = Reader.ReadOsuString();
        var titleUnicode = Reader.ReadOsuString();
        var creator = Reader.ReadOsuString();
        var version = Reader.ReadOsuString();
        var audioFileName = Reader.ReadOsuString();
        var md5Hash = Reader.ReadOsuString();
        var beatmapFileName = Reader.ReadOsuString();
        return new BeatmapMetadata
        {
            Artist = artist,
            ArtistUnicode = artistUnicode,
            Title = title,
            TitleUnicode = titleUnicode,
            Creator = creator,
            Version = version,
            AudioFileName = audioFileName,
            Md5Hash = md5Hash,
            BeatmapFileName = beatmapFileName,
        };
    }

    private void ReadBeatmapStatistics(OsuBeatmap osuBeatmap)
    {
        osuBeatmap.HitCircle = Reader.ReadInt16();
        osuBeatmap.Slider = Reader.ReadInt16();
        osuBeatmap.Spinner = Reader.ReadInt16();
    }

    private DifficultyAttributes ReadDifficultyAttributesNewVersion()
    {
        var approachRate = Reader.ReadSingle();
        var circleSize = Reader.ReadSingle();
        var hpDrain = Reader.ReadSingle();
        var overallDifficulty = Reader.ReadSingle();
        return new DifficultyAttributes
        {
            ApproachRate = approachRate,
            CircleSize = circleSize,
            HpDrain = hpDrain,
            OverallDifficulty = overallDifficulty
        };
    }
    
    private DifficultyAttributes ReadDifficultyAttributesOldVersion()
    {
        var approachRate = Reader.ReadByte();
        var circleSize = Reader.ReadByte();
        var hpDrain = Reader.ReadByte();
        var overallDifficulty = Reader.ReadByte();
        return new DifficultyAttributes
        {
            ApproachRate = approachRate,
            CircleSize = circleSize,
            HpDrain = hpDrain,
            OverallDifficulty = overallDifficulty
        };
    }

    private DifficultyAttributes ReadDifficultyAttributes(int version)
    {
        return version < 20140609 ? ReadDifficultyAttributesOldVersion() : ReadDifficultyAttributesNewVersion();
    }


    private Dictionary<int, double> ReadRulesetModStarsPair()
    {
        var dict = new Dictionary<int, double>();
        var length = Reader.ReadInt32();
        for (var i = 0; i < length; i++)
        {
            Reader.ReadByte();
            var mod = Reader.ReadInt32();
            Reader.ReadByte();
            var stars = Reader.ReadDouble();
            dict.TryAdd(mod, stars);
        }

        return dict;
    }

    private void ReadModStarsPair(OsuBeatmap osuBeatmap)
    {
        osuBeatmap.ModStarPair[LegacyRuleset.Osu] = ReadRulesetModStarsPair();
        osuBeatmap.ModStarPair[LegacyRuleset.Taiko] = ReadRulesetModStarsPair();
        osuBeatmap.ModStarPair[LegacyRuleset.Catch] = ReadRulesetModStarsPair();
        osuBeatmap.ModStarPair[LegacyRuleset.Mania] = ReadRulesetModStarsPair();
    }

    private void SkipAchievedGrades()
    {
        //Archived grades in osu!std
        Reader.ReadByte();
        
        //Archived grades in taiko
        Reader.ReadByte();
        
        //Archived grades in catch
        Reader.ReadByte();
        
        //Archived grades in osu!mania
        Reader.ReadByte();
    }

    private void ReadBeatmapTimes(OsuBeatmap osuBeatmap)
    {
        osuBeatmap.DrainTime = TimeSpan.FromSeconds(Reader.ReadInt32());
        osuBeatmap.TotalTime = TimeSpan.FromMilliseconds(Reader.ReadInt32());
        osuBeatmap.PreviewPoint = TimeSpan.FromMilliseconds(Reader.ReadInt32());
    }
    
    private List<OsuBeatmapTimingPoint> ReadTimingPoints()
    {
        var countToRead = Reader.ReadInt32();
        List<OsuBeatmapTimingPoint> timingPoints = new List<OsuBeatmapTimingPoint>();
        for (var i = 0; i < countToRead; i++)
        {
            var bpm = Reader.ReadDouble();
            var offset = Reader.ReadDouble();
            var inherit = Reader.ReadBoolean();
            timingPoints.Add(new OsuBeatmapTimingPoint(bpm, offset, inherit));
        }

        return timingPoints;
    }
    
    private OsuBeatmap ReadBeatmap(OsuManifest manifest)
    {
        if (manifest.Version < 20191106)
        {
            Reader.ReadInt32();
        }
        
        OsuBeatmap osuBeatmap = new OsuBeatmap
        {
            Metadata = ReadBeatmapMetadata(),
            BeatmapStatus = (OsuBeatmapStatus) Reader.ReadByte(),
        };
        ReadBeatmapStatistics(osuBeatmap);
        osuBeatmap.LastModificationTime = new DateTime(Reader.ReadInt64());
        osuBeatmap.DifficultyAttributes = ReadDifficultyAttributes(manifest.Version);
        Reader.ReadDouble();
        if (manifest.Version >= 20140609)
        {
            ReadModStarsPair(osuBeatmap);
        }
        
        ReadBeatmapTimes(osuBeatmap);
        osuBeatmap.TimingPointsInternal = ReadTimingPoints();
        
        //Difficulty ID
        Reader.ReadInt32();

        osuBeatmap.BeatmapId = Reader.ReadInt32();
        osuBeatmap.ThreadId = Reader.ReadInt32();
        
        SkipAchievedGrades();

        //Local offset
        Reader.ReadInt16();
        
        //StackLeniency
        Reader.ReadSingle();
        
        osuBeatmap.Ruleset = Ruleset.FromLegacyRuleset((LegacyRuleset) Reader.ReadByte());
        osuBeatmap.Metadata.Source = Reader.ReadOsuString();
        osuBeatmap.Metadata.Tags = Reader.ReadOsuString();
        
        //Online offset
        Reader.ReadInt16();
        
        //Title font
        Reader.ReadOsuString();
        
        //Is beatmap never played
        Reader.ReadBoolean();
        
        //Last played time
        Reader.ReadInt64();
        
        //Is beatmap use osz2
        Reader.ReadBoolean();
        osuBeatmap.FolderName = Reader.ReadOsuString();
        
        //Last check time
        Reader.ReadInt64();
        
        //Ignore beatmap sound
        Reader.ReadBoolean();
        
        //Ignore beatmap skin
        Reader.ReadBoolean();
        
        //Disable StoryBoard
        Reader.ReadBoolean();
        
        //Disable video
        Reader.ReadBoolean();
        
        //Visual override
        Reader.ReadBoolean();

        if (manifest.Version < 20140609)
        {
            Reader.ReadInt16();
        }
        
        //Last modification time
        Reader.ReadInt32();

        //Mania scroll speed
        Reader.ReadByte();

        var ruleset = osuBeatmap.Ruleset.LegacyRuleset;
        double? noModStarRating = 0;
        
        if (ruleset == null)
        {
            noModStarRating = null;
        }
        else if(osuBeatmap.ModStarPair[ruleset.Value].TryGetValue(0, out var tmpRating))
        {
            noModStarRating = tmpRating;
        }
        else
        {
            noModStarRating = null;
        }

        osuBeatmap.Stars = noModStarRating;
        return osuBeatmap;
    }

    private List<OsuBeatmap> ReadBeatmaps(OsuManifest manifest)
    {
        List<OsuBeatmap> osuBeatmaps = new List<OsuBeatmap>();
        var beatmapCount = manifest.BeatmapCount;
        for (int i = 0; i < beatmapCount; i++)
        {
            osuBeatmaps.Add(ReadBeatmap(manifest));
        }

        return osuBeatmaps;
    }
    
    public OsuBeatmapDb Read()
    {
        OsuBeatmapDb beatmapDb = new OsuBeatmapDb();
        ReadManifest(beatmapDb);
        beatmapDb.Beatmaps = new OsuBeatmapCollection(ReadBeatmaps(beatmapDb.Manifest));
        beatmapDb.Manifest.Permission = (UserPermission)Reader.ReadInt32();
        beatmapDb.DatabaseFilePath = _dbPath;
        beatmapDb.Md5 = MD5.Create().ComputeHash(File.ReadAllBytes(_dbPath)).GetMd5String();
        return beatmapDb;
    }
}