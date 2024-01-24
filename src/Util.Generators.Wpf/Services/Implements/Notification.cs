using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using HandyControl.Controls;
using Util.Generators.Services.Abstractions;

namespace Util.Generators.Services.Implements
{
    /// <summary>
    /// 通知管理
    /// </summary>
    public sealed class Notification : INotification
    {
        /// <summary>
        /// Token状态检查间隔时间 毫秒
        /// </summary>
        private const int StateCheckInterval = 300;

        /// <summary>
        /// Token
        /// </summary>
        public string? Token { get; set; }

        /// <summary>
        /// 等待加载Token
        /// </summary>
        /// <returns></returns>
        private Task WaitForTokenLoaded()
        {
            return Task.Run(() =>
            {
                while (Token == null)
                {
                    Thread.Sleep(StateCheckInterval);
                }
            });
        }

        /// <summary>
        /// 消息
        /// </summary>
        /// <param name="msg"></param>
        public async void Info(string msg)
        {
            await WaitForTokenLoaded();
            Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Program.GlobalNotification)
                {
                    Growl.InfoGlobal(ParseMsg(msg));
                }
                else
                {
                    Growl.Info(ParseMsg(msg), Token);
                }
            });
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="msg"></param>
        public async void Warn(string msg)
        {
            await WaitForTokenLoaded();
            Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Program.GlobalNotification)
                    Growl.WarningGlobal(ParseMsg(msg));
                else
                    Growl.Warning(ParseMsg(msg), Token);
            });
        }

        /// <summary>
        /// 询问
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public Task<bool> Ask(string msg)
        {
            return Task.Run(() =>
            {
                WaitForTokenLoaded();
                bool? result = null;
                Application.Current?.Dispatcher.Invoke(() =>
                {
                    Growl.Ask(ParseMsg(msg), (_result) =>
                    {
                        result = _result;
                        return true;
                    }, Token);
                });
                while (result == null)
                {
                    Thread.Sleep(StateCheckInterval);
                }

                return result == true;
            });
        }

        /// <summary>
        /// 成功
        /// </summary>
        /// <param name="msg"></param>
        public async void Success(string msg)
        {
            await WaitForTokenLoaded();
            Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Program.GlobalNotification)
                    Growl.SuccessGlobal(ParseMsg(msg));
                else
                    Growl.Success(ParseMsg(msg), Token);
            });
        }

        /// <summary>
        /// 失败
        /// </summary>
        /// <param name="msg"></param>
        public async void Error(string msg)
        {
            await WaitForTokenLoaded();
            Application.Current?.Dispatcher.Invoke(() =>
            {
                if (Program.GlobalNotification)
                    Growl.ErrorGlobal(ParseMsg(msg));
                else
                    Growl.Error(ParseMsg(msg), Token);
            });
        }

        /// <summary>
        /// 解析语言代码到内容
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private string ParseMsg(string msg)
        {
            return Application.Current?.Resources[msg]?.ToString() ?? msg;
        }
    }
}