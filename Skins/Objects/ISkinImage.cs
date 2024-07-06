using System.Drawing;

namespace osuToolsV2.Skins.Objects;

public interface ISkinImage : ISkinObject
{
    //Removed for compatibility to old versions .net framework 
    /*Image LoadImage();
    bool TryLoadImage(out Image? img, out Exception? exception);*/
    ISkinImage? GetHighResolutionImage();
}