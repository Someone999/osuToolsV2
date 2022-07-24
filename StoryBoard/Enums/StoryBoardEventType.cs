namespace osuToolsV2.StoryBoard.Enums
{
    /// <summary>
    ///     StoryBoard的资源类型
    /// </summary>
    public enum StoryBoardEventType
    {
        /// <summary>
        ///  背景图
        /// </summary>
        Background = 0,
        
        /// <summary>
        ///  背景视频
        /// </summary>
        Video = 1,
        
        /// <summary>
        ///  休息时间
        /// </summary>
        BreakTime = 2,
        
        /// <summary>
        ///  颜色变换
        /// </summary>
        Colour = 3,
        
        /// <summary>
        ///  精灵
        /// </summary>
        Sprite = 4,
        
        /// <summary>
        ///  音频
        /// </summary>
        Audio = 5,
        
        Sample = Audio,
        
        /// <summary>
        ///  动画
        /// </summary>
        Animation = 6
    }
}