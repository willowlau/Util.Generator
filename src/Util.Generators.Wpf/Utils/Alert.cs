using System;
using System.Windows;
using Application = System.Windows.Application;
using MessageBox = HandyControl.Controls.MessageBox;

namespace Util.Generators.Utils
{
    /// <summary>
    /// 警告提示框
    /// </summary>
    public class Alert
    {
        /// <summary>
        /// 提示
        /// </summary>
        private static readonly string Tip = ParseMsg("Common.Tip");

        /// <summary>
        /// 提示信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fun"></param>
        public static void Info(string msg, Action? fun = null)
        {
            var msgResult = MessageBox.Info(ParseMsg(msg), Tip);
            if (msgResult != MessageBoxResult.OK) return;
            if (fun != null)
                Application.Current.Dispatcher.Invoke(fun);
        }

        /// <summary>
        /// 警告信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fun"></param>
        public static void Warn(string msg, Action? fun = null)
        {
            var msgResult = MessageBox.Warning(ParseMsg(msg), Tip);
            if (msgResult != MessageBoxResult.OK) return;
            if (fun != null)
                Application.Current.Dispatcher.Invoke(fun);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="msg"></param>
        public static void Error(string msg)
        {
            Error(msg, Tip, null);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tip"></param>
        public static void Error(string msg, string tip)
        {
            Error(msg, tip, null);
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="tip"></param>
        /// <param name="fun"></param>
        public static void Error(string msg, string tip, Action? fun)
        {
            var msgResult = MessageBox.Error(ParseMsg(msg), tip);
            if (msgResult != MessageBoxResult.OK) return;
            if (fun != null)
                Application.Current.Dispatcher.Invoke(fun);
        }

        /// <summary>
        /// 成功信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fun"></param>
        public static void Success(string msg, Action? fun = null)
        {
            var msgResult = MessageBox.Success(ParseMsg(msg), Tip);
            if (msgResult != MessageBoxResult.OK) return;
            if (fun != null)
                Application.Current.Dispatcher.Invoke(fun);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="fun"></param>
        public static void Fail(string msg, Action? fun = null)
        {
            var msgResult = MessageBox.Fatal(ParseMsg(msg), Tip);
            if (msgResult != MessageBoxResult.OK) return;
            if (fun != null)
                Application.Current.Dispatcher.Invoke(fun);
        }

        /// <summary>
        /// 失败信息
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="funOk">确认回调事件</param>
        /// <param name="funCancel">取消回调事件</param>
        public static void Confirm(string msg, Action? funOk = null, Action? funCancel = null)
        {
            var dialogResult = MessageBox.Ask(ParseMsg(msg), Tip);
            if (dialogResult == MessageBoxResult.OK)
            {
                if (funOk != null)
                    Application.Current.Dispatcher.Invoke(funOk);
            }
            else
            {
                if (funCancel != null)
                    Application.Current.Dispatcher.Invoke(funCancel);
            }
        }

        /// <summary>
        /// 解析消息
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private static string ParseMsg(string msg)
        {
            return Application.Current?.Resources[msg]?.ToString() ?? msg;
        }
    }
}