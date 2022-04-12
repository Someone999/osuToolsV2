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
        
        hitSample.NormalSet = (SampleSet)Enum.Parse(typeof(SampleSet),normalSampleStr);
        hitSample.NormalSet = (SampleSet)Enum.Parse(typeof(SampleSet),additionSampleStr);
        hitSample.Index = int.Parse(idxStr);
        hitSample.Volume = int.Parse(volumeStr);
        hitSample.FileName = data[4];
        return hitSample;
    }
}