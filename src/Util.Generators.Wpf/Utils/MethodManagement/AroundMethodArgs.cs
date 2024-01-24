#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Util.Generators.Utils.MethodManagement
{
    /// <summary>
    /// 周围方法参数
    /// </summary>
    public sealed class AroundMethodArgs
    {
        /// <summary>
        /// 方法信息
        /// </summary>
        public MethodInfo? MethodInfo { get; internal set; }

        /// <summary>
        /// 实例
        /// </summary>
        public object? Instance { get; internal set; }

        /// <summary>
        /// 参数
        /// </summary>
        public object[]? Args { get; internal set; }

        /// <summary>
        /// 扩展参数
        /// </summary>
        public Dictionary<string, object>? ExtraArgs { get; internal set; }

        /// <summary>
        /// 调用程序
        /// </summary>
        internal Func<object>? Invoker { get; set; }

        /// <summary>
        /// 调用
        /// </summary>
        /// <returns></returns>
        public object Invoke()
        {
            return Invoker!();
        }
    }
}