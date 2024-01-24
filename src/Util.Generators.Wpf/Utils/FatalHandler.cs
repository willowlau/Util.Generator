using System;
using System.Linq;
using System.Windows.Threading;
using Microsoft.Extensions.Logging;
using Util.Helpers;

namespace Util.Generators.Utils
{
    /// <summary>
    /// 失败处理
    /// </summary>
    static class FatalHandler
    {
        /// <summary>
        /// 阻止列表异常源
        /// </summary>
        private static readonly string[] BlockListForExceptionSource =
        {
            "PresentationCore"
        };

        /// <summary>
        /// 阻塞
        /// </summary>
        private static bool _blocked = false;

        /// <summary>
        /// 调度程序未处理异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            if (_blocked) return;
            _blocked = false; // 不阻塞
            var logger = Ioc.Create<ILogger<App>>();
            var src = e.Exception.Source;
            if (BlockListForExceptionSource.Contains(src))
            {
                logger.LogError($"PresentationCore Error, {e.Exception}");
                return;
            }

            var n = System.Environment.NewLine;
            var extra =
                $"{Constants.APP_NAME} Exception {DateTime.Now:yyyy/MM/dd HH:mm:ss}{n}{n}" +
                $"Exception:{n}{e.Exception.ToString()}{n}{n}" +
                $"Message:{n}{e.Exception.Message}{n}{n}" +
                $"Source:{n}{e.Exception.Source}{n}{n}" +
                $"Inner:{n}{e.Exception.InnerException?.ToString() ?? "None"}{n}";

            try
            {
                logger.LogError(extra);
            }
            catch
            {
                // ignored
            }

            ShowErrorToUser();
            e.Handled = true;
            AppUnloader.Unload();
            // App.Current?.Shutdown(1);
        }

        /// <summary>
        /// 显示错误信息到用户
        /// </summary>
        private static void ShowErrorToUser()
        {
            switch (System.Threading.Thread.CurrentThread.CurrentCulture.Name)
            {
                case "zh-CN":
                case "zh-TW":
                case "zh-SG":
                case "zh-HK":
                    Alert.Error($"一个未知的错误的发生了,将logs文件夹压缩并发送给开发者以解决问题{System.Environment.NewLine}" +
                                $"出问题不发logs,开发者永远不可能解决你遇到的问题{System.Environment.NewLine}" +
                                $"邮件: iterm@163.com",
                        $"{Constants.APP_NAME} 错误");
                    break;
                default:
                    Alert.Error(
                        $"{Constants.APP_NAME} was failed on running{System.Environment.NewLine}" +
                        $"Please compress the logs folder and send it to iterm@163.com",
                        "Unknow Exception");
                    break;
            }
        }
    }
}