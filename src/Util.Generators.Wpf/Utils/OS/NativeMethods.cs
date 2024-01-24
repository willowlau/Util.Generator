using System;
using System.Runtime.InteropServices;

namespace Util.Generators.Utils.OS
{
    /// <summary>
    /// 本地方法
    /// </summary>
    public static class NativeMethods
    {
        /// <summary>
        /// 该函数设置由不同线程产生的窗口的显示状态
        /// </summary>
        /// <param name="hWnd">窗口句柄</param>
        /// <param name="cmdShow">指定窗口如何显示。查看允许值列表，请查阅ShowWlndow函数的说明部分</param>
        /// <returns>如果函数原来可见，返回值为非零；如果函数原来被隐藏，返回值为零</returns>
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        /// <summary>
        ///  该函数将创建指定窗口的线程设置到前台，并且激活该窗口。键盘输入转向该窗口，并为用户改各种可视的记号。
        ///  系统给创建前台窗口的线程分配的权限稍高于其他线程。 
        /// </summary>
        /// <param name="hWnd">将被激活并被调入前台的窗口句柄</param>
        /// <returns>如果窗口设入了前台，返回值为非零；如果窗口未被设入前台，返回值为零</returns>
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        #region HelpButtonHelper

        /// <summary>
        /// 获得指定窗口的信息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern uint GetWindowLong(IntPtr hwnd, int index);

        /// <summary>
        /// 设置指定窗口信息
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="index"></param>
        /// <param name="newStyle"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hwnd, int index, uint newStyle);

        /// <summary>
        /// 设置指定窗口位置
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="hwndInsertAfter"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="flags"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetWindowPos(IntPtr hwnd, IntPtr hwndInsertAfter, int x, int y, int width, int height,
            uint flags);

        #endregion
    }
}