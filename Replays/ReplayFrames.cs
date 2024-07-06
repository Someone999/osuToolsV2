using System.Text;
using HsManCommonLibrary.ExtractMethods;
using osuToolsV2.Game.Mods;
using SharpCompress.Compressors.LZMA;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;

namespace osuToolsV2.Replays;

public class ReplayFrames
{
    private bool _hasIndicatorHeader;
    private List<ReplayFrame> _replayFrames = new List<ReplayFrame>();
    private long? _randomSeed;
    
    public long? RandomSeed => _randomSeed;

    public ReplayFrame[] Frames => _replayFrames.ToArray();
    public double? ModsTimeRate { get; private set; }

    private void ApplyTimeRate(double timeRate, ReplayFrame replayFrame)
    {
        double oldModTimeRateScale = ModsTimeRate ?? 1;
        var time = replayFrame.TimeFromLastAction / timeRate / oldModTimeRateScale;
        replayFrame.TimeFromLastAction = (long)time;
    }

    private void ApplyModsInternal(IEnumerable<Mod>? mods, bool adjustActionTime)
    {
        if (mods == null)
        {
            return;
        }
        
        var timeRate = 1.0;
        var modArray = mods as Mod[] ?? mods.ToArray();
        
        foreach (var mod in modArray)
        {
            if (mod is not IChangeTimeRateMod changeTimeRateMod)
            {
                continue;
            }

            timeRate *= changeTimeRateMod.TimeRate;
        }

        if (!adjustActionTime)
        {
            ModsTimeRate = timeRate;
            return;
        }

        foreach (var replayFrame in _replayFrames)
        {
            ApplyTimeRate(timeRate, replayFrame);
        }

        ModsTimeRate = timeRate;
    }

    public bool HasSameFrames(ReplayFrames frames)
    {
        if (frames._replayFrames.Count != _replayFrames.Count)
        {
            return false;
        }

        for (var i = 0; i < _replayFrames.Count; i++)
        {
            var selfFrame = _replayFrames[i];
            var frame = frames._replayFrames[i];
            if (!selfFrame.IsSameFrame(frame))
            {
                return false;
            }
        }

        return true;
    }
    
    public ReplayFrame[] SelectDifferentFrames(ReplayFrames frames)
    {
        var minCount = Math.Min(frames._replayFrames.Count, _replayFrames.Count);

        
        List<ReplayFrame> replayFrames = new List<ReplayFrame>();
        for (var i = 0; i < minCount; i++)
        {
            var selfFrame = _replayFrames[i];
            var frame = frames._replayFrames[i];
            if (!selfFrame.IsSameFrame(frame))
            {
                replayFrames.Add(frame);
            }
        }

        return replayFrames.ToArray();
    }
    public void ApplyMods(IEnumerable<Mod>? mods)
    {
        ApplyModsInternal(mods, false);
    }
    
    public byte[] Compress() => CompressData(_replayFrames, _randomSeed, _hasIndicatorHeader);

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
        long outputSize = BitConverter.ToInt64(buffer, 0);
        long inputSize = inputStream.Length - inputStream.Position;
        decoder.Code(inputStream, outputStream, inputSize, outputSize, null);
        return outputStream.ToArray();
    }

    private static string BuildFrameString(List<ReplayFrame> frames, long? randomSeed, bool hasHeader)
    {
        StringBuilder builder = new StringBuilder();
        if (hasHeader)
        {
            builder.Append("0|256|500|0,-1|256|500|0,");
        }
        
        var frameStrings = 
            frames.Select(f => $"{f.TimeFromLastAction}|{f.X}|{f.Y}|{(int)f.ButtonState}");
        var frameStr = string.Join(",", frameStrings);
        builder.Append(frameStr);
        if (randomSeed.HasValue)
        {
            builder.Append($",-12345|0|0|{randomSeed.Value}");
        }
        
        return builder.ToString();
    }

    private static byte[] CompressData(List<ReplayFrame> frames, long? randanSeed = null, bool hasHeader = true)
    {
        return CompressData(BuildFrameString(frames, randanSeed, hasHeader));
    }
    
    
    private static byte[] CompressData(string str)
    {
        byte[] properties = { 93, 0, 0, 32, 0 };
        var realData = str.GetBytes();
        var len = (long) realData.Length;
        MemoryStream outputStream = new MemoryStream();
        outputStream.Write(properties,0 , properties.Length);
        var buffer = BitConverter.GetBytes(len);
        outputStream.Write(buffer, 0, buffer.Length);
        var encoderParameters = LzmaEncoderProperties.Default;
        using (LzmaStream lzmaStream = new LzmaStream(encoderParameters, false, null, outputStream))
        {
            lzmaStream.Write(realData, 0, realData.Length);
        }

        return outputStream.ToArray();
    }

    class ReplayFramesInternal
    {
        public bool HasHeader { get; set; }
        public List<ReplayFrame> ReplayFrames { get; set; } = new List<ReplayFrame>();
        public long? RandomSeed { get; set; }
    }
    private static void ProcessFrameData(byte[] additionalBytes, ReplayFramesInternal framesInternal)
    {
        if (additionalBytes.Length <= 0)
        {
            return;
        }

        string frames = Encoding.UTF8.GetString(DecompressData(additionalBytes));
        string[] frameStrings = frames.Split(',');
        long offset = 0;
        int headerIndicatorCounter = 0;
        for (var i = 0; i < frameStrings.Length; i++)
        {
            var frameString = frameStrings[i];
            string[] frameData = frameString.Split('|');
            if (i < 2 && frameData[1] == "256" && frameData[2] == "-500")
            {
                headerIndicatorCounter++;
                continue;
            }

            if (frameData[0] == "-12345")
            {
                framesInternal.RandomSeed ??= long.Parse(frameData[3]);
                continue;
            }

            if (string.IsNullOrEmpty(frameString))
            {
                continue;
            }

            long millisecond = long.Parse(frameData[0]);
            offset += millisecond;
            double x = double.Parse(frameData[1]);
            double y = double.Parse(frameData[2]);
            ReplayButtonState buttonState = (ReplayButtonState)int.Parse(frameData[3]);
            framesInternal.ReplayFrames.Add(new ReplayFrame(millisecond, x, y, buttonState) { Offset = offset });
        }

        //framesInternal.ReplayFrames.Sort((x, y) => Math.Sign(x.Offset - y.Offset));
        framesInternal.HasHeader = headerIndicatorCounter == 2;
    }

    public static ReplayFrames ReadFromCompressedData(byte[] data)
    {
        ReplayFrames replayFrames = new ReplayFrames();
        ReplayFramesInternal replayFramesInternal = new ReplayFramesInternal();
        ProcessFrameData(data, replayFramesInternal);
        replayFrames._randomSeed = replayFramesInternal.RandomSeed;
        replayFrames._replayFrames = replayFramesInternal.ReplayFrames;
        replayFrames._hasIndicatorHeader = replayFramesInternal.HasHeader;
        return replayFrames;
    }
    
    public static ReplayFrames ReadFromReplay(Replay replay)
    {
        ReplayFrames replayFrames = ReadFromCompressedData(replay.AdditionalData);
        replayFrames.ApplyModsInternal(replay.ScoreInfo.Mods, false);
        return replayFrames;
    }
}