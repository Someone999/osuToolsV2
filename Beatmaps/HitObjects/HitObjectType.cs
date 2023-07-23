namespace osuToolsV2.Beatmaps.HitObjects;
[Flags]
public enum HitObjectType
{
    None = 0,
    HitCircle = 1 << 0,
    Slider = 1 << 1,
    Spinner = 1 << 2,
    TaikoRedHit = 1 << 3 | HitCircle,
    TaikoBlueHit = 1 << 4 | HitCircle,
    TaikoLargeRedHit = 1 << 5 | HitCircle,
    TaikoLargeBlueHit = 1 << 6 | HitCircle,
    Drumroll = 1 << 7 | Slider,
    DenDen = 1 << 8 | Spinner,
    Fruit = 1 << 9 | HitCircle,
    JuiceStream = 1 << 10 | Slider,
    Banana = 1 << 11 | Spinner,
    ManiaHit = 1 << 12 | HitCircle,
    ManiaHold = 1 << 13,
    NewColor = 1 << 14
}