using System.Runtime.InteropServices;
using System.Text;
using Util.Generators.Services.Abstractions;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 程序信息
    /// </summary>
    public class BuildInfo : IBuildInfo
    {
        /// <summary>
        /// 文件
        /// </summary>
        private const string FILE_NAME = "./build.ini";

        /// <summary>
        /// 评论
        /// </summary>
        public string LatestCommit => Read("commit") ?? "unknown";

        /// <summary>
        /// 日期
        /// </summary>
        public string DateTime => Read("datetime") ?? "unknown";

        /// <summary>
        /// 标记
        /// </summary>
        public string LatestTag => Read("tag") ?? "unknown";

        /// <summary>
        /// 版本号
        /// </summary>
        public string Version => Read("version") ?? "unknown";

        /// <summary>
        /// 读取INI文件
        /// </summary>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="def"></param>
        /// <param name="retVal"></param>
        /// <param name="size"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        [DllImport("kernel32")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal,
            int size, string filePath);

        /// <summary>
        /// 读文件数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private static string? Read(string key)
        {
            const int BUFFER_SIZE = 255;
            var sb = new StringBuilder(BUFFER_SIZE);
            _ = GetPrivateProfileString("Build", key, "", sb, BUFFER_SIZE, FILE_NAME);
            return sb.ToString();
        }
    }
}