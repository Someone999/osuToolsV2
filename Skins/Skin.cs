using System.Text.RegularExpressions;
using osuToolsV2.Game.Config.Parser;
using osuToolsV2.Skins.Config;
using osuToolsV2.Skins.Objects;

namespace osuToolsV2.Skins;

public class Skin
{
    private string?[] _fileNames;
    private List<SkinFile> _skinFiles = new List<SkinFile>();
    private string _skinPath;
    static T CheckNull<T>(T obj)
    {
        return obj ?? throw new InvalidOperationException();
    }
    
    public Skin(string skinPath)
    {
        string lastPathLevel = Path.GetFileName(skinPath);
        if (lastPathLevel == "skin.ini")
        {
            string? tmpDirectoryName = Path.GetDirectoryName(skinPath);
            _skinPath = string.IsNullOrEmpty(tmpDirectoryName)
                ? "skin.ini"
                : tmpDirectoryName;
        }
        else
        {
            string skinCfgFilePath = Path.Combine(skinPath, "skin.ini");
            if (!File.Exists(skinCfgFilePath))
            {
                throw new FileNotFoundException(null, skinCfgFilePath);
            }
            _skinPath = skinPath;
        }
        SkinConfig = new SkinConfigParser(Path.Combine(_skinPath, "skin.ini")).Config;
        var filePaths = Directory.GetFiles(_skinPath, "*", SearchOption.TopDirectoryOnly);
        _fileNames = filePaths.Select(Path.GetFileName).ToArray();
        var lastFileName = "";
        var lastExtension = "";
        Regex frameImagesMatcher = new Regex(@"(.*?)-\d+\.(.*)");
        for (var i = 0; i < _fileNames.Length; i++)
        {
            var fileName = _fileNames[i];
            if (fileName == null)
            {
                continue;
            }
            MatchCollection matches = frameImagesMatcher.Matches(fileName);
            string currentFileName = "", currentExtension = "";
            SkinFile? lastGroup = null;
            if (matches.Count != 0)
            {
                foreach (Match match in matches)
                {
                    if (!match.Success || matches.Count != 1 || match.Groups.Count != 3)
                    {
                        continue;
                    }
                    currentFileName = match.Groups[1].Value;
                    currentExtension = match.Groups[2].Value;
                    SkinFile tmpSkinFile = new SkinFile(filePaths[i]);
                    if (lastFileName != currentFileName || lastExtension != currentExtension)
                    {
                        lastGroup = tmpSkinFile;
                        _skinFiles.Add(tmpSkinFile);
                    }
                    else
                    {
                        lastGroup?.Frames.Add(tmpSkinFile);
                    }
                }
            }
            else
            {
                var s = new SkinFile(filePaths[i]);
                s.Frames.Add(s);
                _skinFiles.Add(s);
            }
            lastFileName = currentFileName;
            lastExtension = currentExtension;
        }
    }
    public SkinConfig SkinConfig { get; set; }
    public SkinFile? GetSkinFilePath(string fileName)
    {
        return _skinFiles.FirstOrDefault(f => f.FileName == fileName);
    }

    public SkinImage[] GetSkinImage(string fileName)
    {
        var files = GetSkinFilePath(fileName);
        return files?.Frames.Select(i => new SkinImage(i.FilePath)).ToArray() ?? new SkinImage[0];
    }
}