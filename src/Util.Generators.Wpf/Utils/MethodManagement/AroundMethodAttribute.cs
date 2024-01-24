using System;

namespace Util.Generators.Utils.MethodManagement
{
    /// <summary>
    /// 表示方法环绕切面的特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public abstract class AroundMethodAttribute : Attribute
    {
        /// <summary>
        /// 执行环绕的函数
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public abstract object Around(AroundMethodArgs args);
    }
}