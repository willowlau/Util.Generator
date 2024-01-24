using System.ComponentModel;

namespace Util.Generators.Enums
{
    /// <summary>
    /// 窗口状态
    /// </summary>
    public enum WindowStatus
    {
        /// <summary>
        /// 导航界面
        /// </summary>
        [Description("导航界面")] Navigation = 1,

        /// <summary>
        /// 功能界面
        /// </summary>
        [Description("功能界面")] Function = 2
    }
}