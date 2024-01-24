using System.Diagnostics;
using System.Reflection;

namespace Util.Generators.Utils
{
    /// <summary>
    /// 版本帮助
    /// </summary>
    public class VersionHelper
    {
        /// <summary>
        /// 版本号与框架
        /// </summary>
        /// <returns></returns>
        public static string GetVersion()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location!);
#if NET5_0
            var netVersion = "NET 50";
#elif NET6_0
            var netVersion = "NET 60";
#elif NET7_0
            var netVersion = "NET 70";
#elif NET8_0
            var netVersion = "NET 80";
#endif
            return $"v {versionInfo.FileVersion} {netVersion}";
        }

        /// <summary>
        /// 数字版本号
        /// </summary>
        /// <returns></returns>
        public static string GetVersionNum()
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location!);
            return $"{versionInfo.ProductVersion}";
        }

        /// <summary>
        /// 获取版权字符
        /// </summary>
        /// <returns></returns>
        public static string? GetCopyright() =>
            FileVersionInfo.GetVersionInfo(Assembly.GetEntryAssembly()?.Location!).LegalCopyright;
    }
}