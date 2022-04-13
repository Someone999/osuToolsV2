using System.Globalization;
using osuToolsV2.Beatmaps.HitObjects.Sounds;
using osuToolsV2.Graphic;

namespace osuToolsV2.Beatmaps.HitObjects;

public static class HitObjectTools
{
    public static void GenericParse( IHitObject hitObject, string[] data, out OriginalHitObjectType oriHitObjType)
    {
        string xStr = data[0], yStr = data[1],startTimeStr = data[2], typeStr = data[3], hitSoundStr = data[4];
        hitObject.Position = new OsuPixel(double.Parse(xStr), double.Parse(yStr));
        hitObject.StartTime = double.Parse(startTimeStr);
        string objType = new string(Convert.ToString(int.Parse(typeStr), 2).Reverse().ToArray());
        oriHitObjType = 0;
        for (int i = 0; i < objType.Length; i++)
        {
            if (objType[i] == '0')
            {
                continue;
            }
            oriHitObjType |= (OriginalHitObjectType)(1 << i);
        }
        hitObject.HitSound = (HitSound)Enum.Parse(typeof(HitSound),hitSoundStr);
    }
}