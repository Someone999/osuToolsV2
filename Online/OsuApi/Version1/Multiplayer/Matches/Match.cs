using Newtonsoft.Json;

namespace osuToolsV2.Online.OsuApi.Version1.Multiplayer.Matches;

public class Match
{
    [JsonProperty("match")]
    public MatchMetadata Metadata { get; set; } = new MatchMetadata();
    public MatchGame[] Games { get; set; } = Array.Empty<MatchGame>();
}