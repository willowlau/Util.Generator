using System;
using System.Diagnostics;

namespace Util.Generators.Utils
{
    /// <summary>
    /// Process工具类
    /// </summary>
    public static class ProcessUtil
    {
        /// <summary>
        /// 判断是否在运行
        /// </summary>
        /// <param name="process"></param>
        /// <returns></returns>
        public static bool IsRunning(this Process process)
        {
            if (process == null)
                throw new ArgumentNullException(nameof(process));

            try
            {
                Process.GetProcessById(process.Id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 查找进程
        /// </summary>
        /// <param name="processName">进程名字</param>
        /// <returns></returns>
        public static Process? FindProcess(string processName)
        {
            Process?[] processes = Process.GetProcessesByName(processName);
            foreach (var process in processes)
            {
                if (string.Equals(process!.ProcessName, processName, StringComparison.OrdinalIgnoreCase))
                {
                    return process;
                }
            }

            return null;
        }
    }
}
