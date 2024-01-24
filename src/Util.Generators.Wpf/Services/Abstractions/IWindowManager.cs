using System.Windows;
using Util.Dependency;

namespace Util.Generators.Services.Abstractions
{
    /// <summary>
    /// 窗口管理
    /// </summary>
    public interface IWindowManager : ISingletonDependency
    {
        /// <summary>
        /// 主窗口
        /// </summary>
        Window MainWindow { get; }

        /// <summary>
        /// 创建窗口
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        Window CreateWindow(string? windowName, params object[] args);

        /// <summary>
        /// 显示窗口
        /// </summary>
        /// <param name="windowName"></param>
        void Show(string? windowName);

        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="windowName"></param>
        void ShowDialog(string? windowName);
    }
}