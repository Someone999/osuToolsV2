﻿namespace osuToolsV2.Beatmaps.HitObjects.Sounds;

[Flags]
public enum HitSound
{
    Default = 0,
    Normal = 1 << 0,
    Whistle = 1 << 1,
    Finish = 1 << 2,
    Clap = 1 << 3
}