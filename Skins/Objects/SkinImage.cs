﻿using System.Drawing;
using System.Runtime.Versioning;
using osuToolsV2.Attributes;

namespace osuToolsV2.Skins.Objects;

public class SkinImage : ISkinImage
{
    ////Removed for compatibility with older versions of .net framework
    //private MemoryStream? _imgMemoryStream;
    public string FilePath { get; }
    public string FileName { get; }
    public SkinImage(string? path)
    {
        if (path == null || !File.Exists(path))
        {
            throw new FileNotFoundException(null, path);
        }
        FilePath = path;
        FileName = Path.GetFileName(path);
    }

//Removed for compatibility with older versions of .net framework 

// #if !NETFRAMEWORK
//     [SupportedOSPlatform("windows")]
// #endif
//     public Image LoadImage()
//     {
//         if (Environment.OSVersion.Platform != PlatformID.Win32NT)
//         {
//             throw new NotSupportedException("This function can only be called on Windows NT");
//         }
//         
//         _imgMemoryStream = new MemoryStream();
//         FileStream fileStream = File.Open(FilePath, FileMode.Open);
//         fileStream.CopyTo(_imgMemoryStream);
//         fileStream.Dispose();
//         return Image.FromStream(_imgMemoryStream);
//     }
//     
// #if !NETFRAMEWORK
//     [SupportedOSPlatform("windows")]
// #endif
//     public bool TryLoadImage(out Image? img, out Exception? exception)
//     {
//         try
//         {
//             img = LoadImage();
//             exception = null;
//             return true;
//         }
//         catch (Exception e)
//         {
//             img = null;
//             exception = e;
//             return false;
//         }
//     }
//     
//     
    public ISkinImage? GetHighResolutionImage()
    {
        string fileName = Path.GetFileNameWithoutExtension(FileName);
        string extension = Path.GetExtension(FileName);
        fileName += "@2x";
        string? directoryName = Path.GetDirectoryName(FilePath);
        string fullPath = directoryName == null
            ? fileName + extension
            : Path.Combine(directoryName, fileName + extension);
        return File.Exists(fullPath)
            ? new SkinImage(fullPath)
            : null;
    }

    ~SkinImage()
    {
        //Removed for compatibility with older versions of .net framework 
        //_imgMemoryStream?.Dispose();
    }
}