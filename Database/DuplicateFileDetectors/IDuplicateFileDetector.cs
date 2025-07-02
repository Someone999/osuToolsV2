namespace osuToolsV2.Database.DuplicateFileDetectors;

public interface IDuplicateFileDetector
{
    Dictionary<string, HashSet<string>> GetDuplicateFilesByMd5(string dir);
    Dictionary<string,  HashSet<string>> GetDuplicateFilesByFileContent(string dir);
}