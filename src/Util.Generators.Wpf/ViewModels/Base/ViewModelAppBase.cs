using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using HandyControl.Controls;
using Microsoft.Extensions.Logging;
using Serilog;
using Util.Generators.Config;
using Util.Generators.Services.Abstractions;
using Util.Generators.Views;
using Util.Helpers;

// ReSharper disable InconsistentNaming

namespace Util.Generators.ViewModels.Base
{
    /// <summary>
    /// 视图基类
    /// </summary>
    public class ViewModelAppBase : ViewModelBase
    {
        /// <summary>
        /// 显示窗口对话框命令
        /// </summary>
        public ICommand ShowWindowDialog
        {
            get => _showWindowDialog;
            set => Set(ref _showWindowDialog, value);
        }

        private ICommand _showWindowDialog = null!;

        /// <summary>
        /// 显示窗口命令
        /// </summary>
        public ICommand ShowWindow
        {
            get => _showWindow;
            set => Set(ref _showWindow, value);
        }

        private ICommand _showWindow = null!;

        /// <summary>
        /// 打开文件或目录命令
        /// </summary>
        public ICommand OpenUrl
        {
            get => _openUrl;
            set => Set(ref _openUrl, value);
        }

        private ICommand _openUrl = null!;

        /// <summary>
        /// 打开网址命令
        /// </summary>
        public ICommand OpenGoUrl
        {
            get => _openGoUrl;
            set => Set(ref _openGoUrl, value);
        }

        private ICommand _openGoUrl = null!;

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled
        {
            get => _enabled;
            set => Set(ref _enabled, value);
        }

        private bool _enabled;

        /// <summary>
        /// 通知管理
        /// </summary>
        protected readonly INotification _notification;
        /// <summary>
        /// 消息总线
        /// </summary>
        protected readonly IMessageBus _messageBus;

        /// <summary>
        /// 日志记录者
        /// </summary>
        private readonly ILogger<ViewModelAppBase> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        public ViewModelAppBase()
        {
            _messageBus = Ioc.Create<IMessageBus>();

            _notification = Ioc.Create<INotification>();
            _notification.Token = "GeneratorMessageToken";

            _logger = Ioc.Create<ILogger<ViewModelAppBase>>();

            Enabled = true;
            OpenUrl = OpenUrlCommand;
            OpenGoUrl = OpenGoUrlCommand;
            ShowWindowDialog = ShowWindowDialogCommand;
            ShowWindow = ShowWindowCommand;

        }

        /// <summary>
        /// 初始化命令
        /// </summary>
        public AsyncRelayCommand<object> LoadedCmd => new(async obj =>
        {
            await Loaded();
            await Loaded(obj);
        });

        /// <summary>
        /// 初始化虚方法
        /// </summary>
        protected virtual async Task Loaded()
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 初始化虚方法
        /// </summary>
        protected virtual async Task Loaded(object? obj)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// 窗口卸载命令
        /// </summary>
        public AsyncRelayCommand UnLoadedCmd => new(Unloaded);

        /// <summary>
        /// 窗口卸载虚方法
        /// </summary>
        protected virtual async Task Unloaded()
        {
            await Task.CompletedTask;
        }
        
        /// <summary>
        /// 语言资源
        /// </summary>
        protected string Lang(string key)
        {
            return App.Current?.Resources[key]?.ToString() ?? key;
        }

        private static readonly RelayCommand<object> OpenGoUrlCommand;
        private static readonly RelayCommand<object> OpenUrlCommand;
        private static readonly RelayCommand<object> ShowWindowDialogCommand;
        private static readonly RelayCommand<object> ShowWindowCommand;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static ViewModelAppBase()
        {
            OpenUrlCommand = new RelayCommand<object>(_OpenUrl);
            OpenGoUrlCommand = new RelayCommand<object>(_OpenGoUrl);
            ShowWindowDialogCommand = new RelayCommand<object>(_ShowWindowDialog);
            ShowWindowCommand = new RelayCommand<object>(_ShowWindow);
        }

        /// <summary>
        /// 打开网址方法
        /// </summary>
        /// <param name="param"></param>
        private static void _OpenGoUrl(object? param)
        {
            var goPre = App.Current?.Resources["UrlGoPrefix"] as string;
            try
            {
                _OpenUrl(goPre + param);
            }
            catch (Exception e)
            {
                Log.ForContext<ViewModelAppBase>().Error($"can not open url {param}", e);
            }
        }

        /// <summary>
        /// 打开文件或目录方法
        /// </summary>
        /// <param name="param"></param>
        private static void _OpenUrl(object? param)
        {
            var url = param as string;
            if (url == null) return;
            //https://brockallen.com/2016/09/24/process-start-for-urls-on-net-core/
            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// 显示窗口对话框方法
        /// </summary>
        /// <param name="param"></param>
        private static void _ShowWindowDialog(object? param)
        {
            Ioc.Create<IWindowManager>()?.ShowDialog(param?.ToString());
        }

        /// <summary>
        /// 显示窗口方法
        /// </summary>
        /// <param name="param"></param>
        private static void _ShowWindow(object? param)
        {
            Ioc.Create<IWindowManager>()?.Show(param?.ToString());
        }

        /// <summary>
        /// 通用加载窗口对象
        /// </summary>
        private Dialog? _dlg;

        /// <summary>
        /// 动画加载
        /// </summary>
        /// <param name="action"></param>
        protected async Task AnimateLoad(Action action)
        {
            App.Current?.Dispatcher.Invoke(async () =>
            {
                _dlg?.Dispatcher.Invoke(() => _dlg?.Close());
                _dlg = Dialog.Show<LoadingMask>(MessageToken.DialogContainer);

                action.Invoke();

                await Task.Delay(100);
                _dlg?.Dispatcher.Invoke(() => _dlg?.Close());
            });

            await Task.CompletedTask;
        }

        /// <summary>
        /// 动画加载
        /// </summary>
        /// <param name="fun"></param>
        protected async Task AnimateLoad(Func<Task> fun)
        {
            App.Current?.Dispatcher.Invoke(async () =>
            {
                _dlg?.Dispatcher.Invoke(() => _dlg?.Close());
                _dlg = Dialog.Show<LoadingMask>(MessageToken.DialogContainer);

                await fun.Invoke();

                await Task.Delay(100);
                _dlg?.Dispatcher.Invoke(() => _dlg?.Close());
            });

            await Task.CompletedTask;
        }
    }
}