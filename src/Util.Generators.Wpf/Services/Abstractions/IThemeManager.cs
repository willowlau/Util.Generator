using Util.Dependency;
using Util.Generators.Enums;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 主题管理
    /// </summary>
    public interface IThemeManager : ISingletonDependency
    {
        /// <summary>
        /// 主题模式
        /// </summary>
        ThemeMode ThemeMode { get; set; }

        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 重新加载
        /// </summary>
        void Reload();
    }
}