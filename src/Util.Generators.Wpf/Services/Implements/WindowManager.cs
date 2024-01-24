using System;
using System.Windows;
using Util.Generators.Services.Abstractions;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 窗口管理
    /// </summary>
    public class WindowManager : IWindowManager
    {
        /// <summary>
        /// 主窗口
        /// </summary>
        public Window MainWindow => Application.Current!.MainWindow!;

        /// <summary>
        /// 创建窗口
        /// </summary>
        /// <param name="windowName"></param>
        /// <param name="args"></param>
        /// <exception cref="Exception">窗口未找到</exception>
        /// <returns></returns>
        public Window CreateWindow(string? windowName, params object[] args)
        {
            if (Type.GetType("Generators.Views.Windows." + windowName + "Window") is Type type
                &&
                Activator.CreateInstance(type, args) is Window win)
            {
                win.Owner = Application.Current?.MainWindow;
                Application.Current!.MainWindow!.Closed += (s, e) =>
                {
                    try
                    {
                        win.Close();
                    }
                    catch
                    {
                    }
                };

                return win;
            }
            else
            {
                throw new Exception("Window not found");
            }
        }

        /// <summary>
        /// 非模态窗口
        /// </summary>
        /// <param name="windowName"></param>
        public void Show(string? windowName)
        {
            CreateWindow(windowName).Show();
        }

        /// <summary>
        /// 模态窗口
        /// </summary>
        /// <param name="windowName"></param>
        public void ShowDialog(string? windowName)
        {
            CreateWindow(windowName).ShowDialog();
        }
    }
}