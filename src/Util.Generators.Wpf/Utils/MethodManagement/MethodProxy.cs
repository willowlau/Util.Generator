#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Util.Generators.Utils.MethodManagement
{
    /// <summary>
    /// 方法代理器
    /// </summary>
    public sealed class MethodProxy
    {
        /// <summary>
        /// 执行方法的实例
        /// </summary>
        private readonly object _instance;

        /// <summary>
        /// 被代理的方法
        /// </summary>
        private readonly MethodInfo _method;

        /// <summary>
        /// 构建一个方法代理器实例
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="methodName"></param>
        public MethodProxy(object instance, string methodName)
        {
            _instance = instance;
            _method = FindMethodByName(methodName);
        }

        /// <summary>
        /// 构建一个方法代理器实例
        /// </summary>
        /// <param name="instance">实例</param>
        /// <param name="method">方法</param>
        public MethodProxy(object instance, MethodInfo method)
        {
            _instance = instance;
            _method = method ?? throw new ArgumentNullException(nameof(method));
        }

        /// <summary>
        /// 根据方法名称找到方法
        /// </summary>
        /// <param name="methodName"></param>
        /// <returns></returns>
        private MethodInfo FindMethodByName(string methodName)
        {
            var result = _instance.GetType().GetMethod(methodName,
                BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.NonPublic |
                BindingFlags.Default | BindingFlags.Static);
            if (result == null)
            {
                throw new InvalidOperationException($"Method {methodName} not found");
            }

            return result;
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="extraArgs"></param>
        /// <exception cref="InvalidOperationException">实例不存在</exception>
        /// <exception cref="TargetInvocationException">方法执行时抛出异常</exception>
        /// <returns></returns>
        public object Invoke(Dictionary<string, object>? extraArgs = null)
        {
            if (_instance == null) throw new InvalidOperationException("Please create instance before invoke method");

            object[] args = ParameterArrayBuilder.BuildArgs(
                extraArgs ?? new Dictionary<string, object>(),
                _method.GetParameters())!;

            object RawInvoker() => _method.Invoke(_instance, args)!;

            var aspects = GetAroundAspects();
            if (aspects.Any())
            {
                var aspectArgs = new AroundMethodArgs()
                {
                    MethodInfo = _method,
                    Instance = _instance,
                    Args = args,
                    ExtraArgs = extraArgs,
                    Invoker = RawInvoker
                };
                return aspects.ElementAt(0).Around(aspectArgs);
            }
            else
            {
                return RawInvoker();
            }
        }

        /// <summary>
        /// 获取所有环绕切面
        /// </summary>
        /// <returns></returns>
        public IEnumerable<AroundMethodAttribute> GetAroundAspects()
        {
            return _method.GetCustomAttributes<AroundMethodAttribute>();
        }

        /// <summary>
        /// 获取一个执行器
        /// </summary>
        /// <param name="extraArgs"></param>
        /// <returns></returns>
        public Func<object> GetInvoker(Dictionary<string, object>? extraArgs = null)
        {
            return () => Invoke(extraArgs);
        }
    }
}