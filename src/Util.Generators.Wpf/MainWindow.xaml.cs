using System.ComponentModel;
using System;
using System.Windows;
using Util.Generators.Utils;
using Util.Generators.Views;

namespace Util.Generators
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 内容呈现后
        /// </summary>
        /// <param name="e"></param>
        protected override void OnContentRendered(EventArgs e)
        {
            base.OnContentRendered(e);
            
            NonClientAreaContent = new MainNonClientArea();
            ControlMain.Content = new MainView();
        }

        /// <summary>
        /// 窗口关闭中
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            Alert.Confirm(App.Current!.Resources["App.ExitTip"].ToString()!,
                null,
                () => { e.Cancel = true; });
        }

        /// <summary>
        /// 窗口关闭后
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Environment.Exit(0);
        }
    }
}
