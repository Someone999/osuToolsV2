﻿namespace osuToolsV2.Online.OsuApi.Version2.Beatmap;

public class CurrentUserAttributes
{
    public string[] BeatmapsetDiscussionPermissions { get; internal set; } = Array.Empty<string>();
    public string[] ChatChannelUserAttributes { get; internal set; } = Array.Empty<string>();
}