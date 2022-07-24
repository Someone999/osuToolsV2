namespace osuToolsV2.Skins;

public class SkinFile : IEqualityComparer<SkinFile>
{
    public SkinFile(string fileFullName)
    {
        FileFullName = Path.GetFileName(fileFullName);
        FileName = Path.GetFileNameWithoutExtension(fileFullName);
        Extension = Path.GetExtension(fileFullName);
        if (File.Exists(fileFullName))
        {
            FilePath = fileFullName;
        }
    }
    public string? FilePath { get; }
    public string FileFullName { get; }
    public string FileName { get;  }
    public List<SkinFile> Frames { get; } = new List<SkinFile>();
    public string Extension { get; }

    public override bool Equals(object? obj)
    {
        if (obj is SkinFile skinFile)
        {
            return Equals(this, skinFile);
        }
        return false;
    }
    
    public override int GetHashCode()
    {
        unchecked
        {
            return (FileName.GetHashCode() * 397) ^ Extension.GetHashCode();
        }
    }

    public bool Equals(SkinFile? x, SkinFile? y)
    {
        if (ReferenceEquals(x, y))
            return true;
        if (ReferenceEquals(x, null))
            return false;
        if (ReferenceEquals(y, null))
            return false;
        if (x.GetType() != y.GetType())
            return false;
        return x.FileName == y.FileName && x.Extension == y.Extension;
    }
    
    public int GetHashCode(SkinFile obj)
    {
        unchecked
        {
            return (obj.FileName.GetHashCode() * 397) ^ obj.Extension.GetHashCode();
        }
    }
}