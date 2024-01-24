using System.Diagnostics;

namespace Util.Generators.Utils.OS
{
    /// <summary>
    /// 其它进程检查者
    /// </summary>
    public static class OtherProcessChecker
    {
        /// <summary>
        /// 显示模式
        /// </summary>
        private const int SW_SHOWNOMAL = 1;

        /// <summary>
        /// 标记
        /// </summary>
        private const string TAG = nameof(OtherProcessChecker);

        /// <summary>
        /// 查找其它进程
        /// </summary>
        /// <returns></returns>
        private static Process? FindOtherProcess()
        {
            var currentProcess = Process.GetCurrentProcess();
            Process?[] processes = Process.GetProcessesByName(currentProcess.ProcessName);
            foreach (var process in processes)
            {
                if (process!.Id != currentProcess.Id)
                {
                    return process;
                }
            }

            return null;
        }

        /// <summary>
        /// 找到其它进程并显示
        /// </summary>
        /// <returns></returns>
        public static bool Do()
        {
            var process = ThereIsOtherTerminalProcess();
            if (process != null)
            {
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, SW_SHOWNOMAL);
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
            }

            return process == null;
        }

        /// <summary>
        /// 查找其它进程
        /// </summary>
        /// <returns></returns>
        public static Process? ThereIsOtherTerminalProcess()
        {
            return FindOtherProcess();
        }
    }
}