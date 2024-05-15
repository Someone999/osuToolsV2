using System.Text;
using HsManCommonLibrary.ExtractMethods;
using osuToolsV2.Game.Mods;
using osuToolsV2.Rulesets.Osu.Mods;
using SharpCompress.Compressors.LZMA;
using Decoder = SharpCompress.Compressors.LZMA.Decoder;

namespace osuToolsV2.Replays;

public class ReplayFrames
{
    private List<ReplayFrame> _replayFrames = new List<ReplayFrame>();
    private long? _randomSeed;
    
    public long? RandomSeed => _randomSeed;

    public ReplayFrame[] Frames => _replayFrames.ToArray();

    private void ApplyTimeRate(double timeRate, ReplayFrame replayFrame)
    {
        var time = replayFrame.TimeFromLastAction / timeRate;
        replayFrame.TimeFromLastAction = (long)time;
    }
    
    public void ApplyMods(IEnumerable<Mod>? mods)
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

        foreach (var replayFrame in _replayFrames)
        {
            ApplyTimeRate(timeRate, replayFrame);
        }
    }
    
    public byte[] Compress() => CompressData(_replayFrames, _randomSeed);

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

    private static string BuildFrameString(List<ReplayFrame> frames, long? randomSeed)
    {
        StringBuilder builder = new StringBuilder("0|256|500|0,-1|256|500|0,");
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
    private static byte[] CompressData(List<ReplayFrame> frames, long? randanSeed = null)
    {
        return CompressData(BuildFrameString(frames, randanSeed));
    }
    
    
    private static byte[] CompressData(string str)
    {
        byte[] properties = { 93, 0, 0, 32, 0 };
        var realData = str.GetBytes();
        var len = (long) realData.Length;
        MemoryStream outputStream = new MemoryStream();
        outputStream.Write(properties);
        outputStream.Write(BitConverter.GetBytes(len));
        var encoderParameters = LzmaEncoderProperties.Default;
        using (LzmaStream lzmaStream = new LzmaStream(encoderParameters, false, null, outputStream))
        {
            lzmaStream.Write(realData);
        }

        return outputStream.ToArray();
    }

    private static void ProcessFrameData(byte[] additionalBytes, List<ReplayFrame> replayFrames, out long? randomSeed)
    {
        randomSeed = null;
        if (additionalBytes.Length <= 0)
        {
            return;
        }

        string frames = Encoding.UTF8.GetString(DecompressData(additionalBytes));
        string[] frameStrings = frames.Split(',');
        long offset = 0;
        for (var i = 0; i < frameStrings.Length; i++)
        {
            var frameString = frameStrings[i];
            string[] frameData = frameString.Split('|');
            if (i < 2 && frameData[1] == "256" && frameData[2] == "-500")
            {
                continue;
            }

            if (frameData[0] == "-12345")
            {
                randomSeed = long.Parse(frameData[3]);
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
            replayFrames.Add(new ReplayFrame(millisecond, x, y, buttonState) { Offset = offset });
        }

        replayFrames.Sort((x, y) => Math.Sign(x.Offset - y.Offset));
    }

    public static ReplayFrames ReadFromCompressedData(byte[] data)
    {
        ReplayFrames replayFrames = new ReplayFrames();
        ProcessFrameData(data, replayFrames._replayFrames, out replayFrames._randomSeed);
        return replayFrames;
    }
}