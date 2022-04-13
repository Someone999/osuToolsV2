namespace osuToolsV2.Beatmaps.HitObjects.Sounds;

public class HitSample
{
    public SampleSet NormalSet { get; set; } = SampleSet.Normal;
    public SampleSet AdditionSet { get; set; } = SampleSet.Normal;
    public int Index { get; set; } = 0;
    public int Volume { get; set; } = 100;
    public string? FileName { get; set; }

    public static HitSample Empty { get; } = new();

    public static HitSample Parse(string data)
    {
        return Parse(data.Split(':'));
    }
    
    public static HitSample Parse(string[] data)
    {
        HitSample hitSample = new HitSample();
        string normalSampleStr = data[0];
        string additionSampleStr = data[1];
        string idxStr = data[2], volumeStr = data[3];
        
        hitSample.NormalSet = string.IsNullOrEmpty(normalSampleStr)? SampleSet.None : (SampleSet)Enum.Parse(typeof(SampleSet),normalSampleStr);
        hitSample.AdditionSet = string.IsNullOrEmpty(additionSampleStr)? SampleSet.None :(SampleSet)Enum.Parse(typeof(SampleSet),additionSampleStr);
        
        hitSample.Index = string.IsNullOrEmpty(idxStr) ? 0 : int.Parse(idxStr);
        hitSample.Volume =string.IsNullOrEmpty(volumeStr) ? 100 : int.Parse(volumeStr);
        hitSample.FileName = data[4];
        return hitSample;
    }
}