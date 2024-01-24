using System;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows;

namespace Util.Generators.Utils
{
    /// <summary>
    /// 程序本身
    /// </summary>
    public static class Self
    {
        /// <summary>
        /// 获取当前版本
        /// </summary>
        public static Version? Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        /// <summary>
        /// 判断是否拥有管理员权限
        /// </summary>
        public static bool HaveAdminPermission
        {
            get
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        /// <summary>
        /// 判断是否有其它进程
        /// </summary>
        public static bool HaveOtherTerminalProcess => Process.GetProcessesByName("iTerm").Length > 1;

        /// <summary>
        /// 重启程序
        /// </summary>
        /// <param name="asAdmin"></param>
        public static void Restart(bool asAdmin)
        {
            Process.Start(System.Windows.Application.ResourceAssembly.Location, "--wait");
            Application.Current?.Dispatcher.Invoke(() => { Application.Current.Shutdown(0); });
        }
    }
}