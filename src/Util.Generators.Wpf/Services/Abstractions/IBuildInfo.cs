using Util.Dependency;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 程序信息
    /// </summary>
    public interface IBuildInfo : ISingletonDependency
    {
        /// <summary>
        /// 评论
        /// </summary>
        string LatestCommit { get; }

        /// <summary>
        /// 日期
        /// </summary>
        string DateTime { get; }

        /// <summary>
        /// 标记
        /// </summary>
        string LatestTag { get; }

        /// <summary>
        /// 版本号
        /// </summary>
        string Version { get; }
    }
}