using Microsoft.Extensions.Hosting;
using System;
using Util.Generators.Utils.OS;
using Util.Helpers;
using Util.Logging.Serilog;

namespace Util.Generators
{
    /// <summary>
    /// 程序入口
    /// </summary>
    class Program
    {
        /// <summary>
        /// 是否全局通知
        /// </summary>
        public static bool GlobalNotification { get; set; }

        /// <summary>
        /// 主入口
        /// </summary>
        /// <param name="args"></param>
        [STAThread]
        private static void Main(string[] args)
        {
            InitContainer();
            AppRun();
        }

        /// <summary>
        /// 初始化容器
        /// </summary>
        private static void InitContainer()
        {
            var host = Host.CreateDefaultBuilder()
                .AsBuild()
                .AddSerilog()
                .AddUtil()
                .Build();

            Ioc.SetServiceProviderAction(() => host.Services);
        }

        /// <summary>
        /// 运行
        /// </summary>
        private static void AppRun()
        {
            var process = OtherProcessChecker.ThereIsOtherTerminalProcess();
            if (process != null)
            {
                NativeMethods.ShowWindowAsync(process.MainWindowHandle, 1);
                NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                System.Environment.Exit(0);
                process.WaitForExit();
            }

            var app = new App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
