using osuToolsV2.Database.Beatmap;

namespace osuToolsV2.Database
{
    /// <summary>
    ///  Get all beatmap cache and some game information from osu!.db
    ///  Use <see cref="osuToolsV2.Reader.OsuBeatmapDbObjectReader"/> to create a <see cref="OsuBeatmapDb"/>> object/>
    /// </summary>
    public class OsuBeatmapDb
    {
        internal OsuBeatmapDb()
        {
        }
        public string DatabaseFilePath { get; internal set; } = "";
        
        /// <summary>
        ///     osu!的一些信息
        /// </summary>
        public OsuManifest Manifest { get; internal set; } = new();

        /// <summary>
        ///     从osu!.db读取到的谱面
        /// </summary>
        public OsuBeatmapCollection Beatmaps { get; internal set; } = new();

        /// <summary>
        ///     osu!.db的MD5
        /// </summary>
        public string Md5 { get; internal set; } = "";
    }
}