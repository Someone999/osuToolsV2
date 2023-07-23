namespace osuToolsV2.Beatmaps.HitObjects;

[Flags]
public enum OriginalHitObjectType
{
    HitCircle = 1 << 0,
    Slider = 1 << 1,
    NewCombo = 1 << 2,
    Spinner = 1 << 3,
    ComboColor1 = 1 << 4,
    ComboColor2 = 1 << 5,
    ComboColor3 = 1 << 6,
    ManiaHold = 1 << 7
}