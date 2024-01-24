using System.ComponentModel;

namespace Util.Generators.Enums
{
    /// <summary>
    /// 主题模式
    /// </summary>
    public enum ThemeMode
    {
        /// <summary>
        /// 亮色
        /// </summary>
        [Description("亮色")] Light = 1,

        /// <summary>
        /// 暗色
        /// </summary>
        [Description("暗色")] Dark = 2,

        /// <summary>
        /// 自动
        /// </summary>
        [Description("自动")] Auto = 3
    }
}