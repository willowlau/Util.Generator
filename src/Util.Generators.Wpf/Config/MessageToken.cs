namespace Util.Generators.Config
{
    /// <summary>
    /// 消息会话类型
    /// </summary>
    public class MessageToken
    {
        /// <summary>
        /// 全屏开关
        /// </summary>
        public static readonly string FullSwitch = nameof(FullSwitch);

        /// <summary>
        /// 主窗口
        /// </summary>
        public static readonly string MainWindow = nameof(MainWindow);

        /// <summary>
        /// 加载显示内容
        /// </summary>
        public static readonly string LoadShowContent = nameof(LoadShowContent);

        /// <summary>
        /// Dialog容器
        /// </summary>
        public static readonly string DialogContainer = nameof(DialogContainer);

        /// <summary>
        /// 切换语言
        /// </summary>
        public static readonly string LangUpdated = nameof(LangUpdated);

        /// <summary>
        /// 切换皮肤
        /// </summary>
        public static readonly string SkinUpdated = nameof(SkinUpdated);

        /// <summary>
        /// 全局异常
        /// </summary>
        public static readonly string FatalHandler = nameof(FatalHandler);
    }
}