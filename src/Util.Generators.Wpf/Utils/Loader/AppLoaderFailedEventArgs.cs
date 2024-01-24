using System;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// App加载失败事件参数
    /// </summary>
    public class AppLoaderFailedEventArgs : EventArgs
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="e"></param>
        public AppLoaderFailedEventArgs(AppLoadingException e)
        {
            Exception = e;
        }

        /// <summary>
        /// 异常信息
        /// </summary>
        public AppLoadingException Exception { get; }
    }
}