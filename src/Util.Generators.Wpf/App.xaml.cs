using HandyControl.Properties.Langs;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Threading;
using Util.Generators.Utils;
using Util.Generators.Utils.Loader;

namespace Util.Generators
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// App加载者
        /// </summary>
        internal AbstractAppLoader AppLoader { get; }

        /// <summary>
        /// 获取当前的应用
        /// </summary>
        public new static App? Current { get; private set; }

        /// <summary>
        /// 构造应用
        /// </summary>
        public App()
        {
            Current = this;
            Thread.CurrentThread.Name = "Util.Generators Thread";
            AppLoader = new GeneralAppLoader();
        }

        /// <summary>
        /// APP启动
        /// </summary>
        /// <param name="e"></param>
        protected override async void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            await AppLoader.LoadAsync();
        }

        /// <summary>
        /// APP退出
        /// </summary>
        /// <param name="e"></param>
        protected override void OnExit(ExitEventArgs e)
        {
            AppUnloader.Unload();
            base.OnExit(e);
        }

        /// <summary>
        /// 异步更新
        /// </summary>
        public static void DoEvents()
        {
            DispatcherFrame frame = new DispatcherFrame();
            Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrames), frame);
            try
            {
                Dispatcher.PushFrame(frame);
            }
            catch (InvalidOperationException)
            {
            }
        }

        /// <summary>
        /// 退出框架
        /// </summary>
        /// <param name="frame"></param>
        /// <returns></returns>
        private static object? ExitFrames(object frame)
        {
            ((DispatcherFrame)frame).Continue = false;
            return null;
        }
    }
}
