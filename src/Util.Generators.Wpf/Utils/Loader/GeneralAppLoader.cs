using System;
using HandyControl.Tools;
using Microsoft.Extensions.Logging;
using Util.Generators.Enums;
using Util.Generators.Services.Abstractions;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// App加载
    /// </summary>
    public class GeneralAppLoader : AbstractAppLoader
    {
        /// <summary>
        /// 打印信息
        /// </summary>
        /// <param name="buildInfo"></param>
        [Step(1)]
        private void PrintInformation(IBuildInfo buildInfo)
        {
            Logger.LogInformation("======================");
            Logger.LogInformation($"Installed as {Environment.CurrentDirectory}");
            // Logger.LogInformation($"Run as {(Self.HaveAdminPermission ? "Administrator" : "Normal user")}");
            Logger.LogInformation($"Windows version: {Environment.OSVersion.Version}, Clr version: {Environment.Version}");
            // Logger.LogInformation($"Commit: {buildInfo.LatestCommit}");
            Logger.LogInformation("======================");
        }

        /// <summary>
        /// 初始化HandyControl
        /// </summary>
        [Step(2)]
        private void InitHandyControl()
        {
            App.Current?.Dispatcher.Invoke(() =>
            {
                var lang = "zh-CN";
                ConfigHelper.Instance.SetLang(lang!.Equals("en-Us") ? "en" : lang);
            });
        }

        /// <summary>
        /// 初始化错误句柄
        /// </summary>
        [Step(3)]
        private void InitErrorHandlerSystem()
        {
#if !DEBUG
            App.Current!.DispatcherUnhandledException += FatalHandler.Current_DispatcherUnhandledException;
#endif
        }

        /// <summary>
        /// 初始化语言
        /// </summary>
        [Step(4)]
        private void InitializeLanguageSystem()
        {
        }

        /// <summary>
        /// 初始化语言
        /// </summary>
        /// <param name="themeManager"></param>
        [Step(5)]
        private void InitializeThemeSystem(IThemeManager themeManager)
        {
            themeManager.ThemeMode = ThemeMode.Light;
            themeManager.Reload();
        }

        /// <summary>
        /// 初始化工具
        /// </summary>
        [Step(6)]
        private void InitUtilities()
        {
        }

        /// <summary>
        /// 显示指导页面
        /// </summary>
        [Step(7)]
        private void DisplayGuide()
        {
        }
    }
}