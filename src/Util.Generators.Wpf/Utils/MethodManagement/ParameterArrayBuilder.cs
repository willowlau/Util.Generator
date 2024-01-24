#nullable enable
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Util.Generators.Utils.MethodManagement
{
    /// <summary>
    /// 参数列表构造器
    /// </summary>
    public static class ParameterArrayBuilder
    {
        /// <summary>
        /// 根据参数请求列表,获取参数
        /// </summary>
        /// <param name="extraArgs">额外的,指定参数名的参数</param>
        /// <param name="parameterInfos">参数列表</param>
        /// <exception cref="System.ArgumentNullException">参数为空</exception>
        /// <returns>应得的参数</returns>
        public static object?[] BuildArgs(
            Dictionary<string, object> extraArgs,
            ParameterInfo[] parameterInfos)
        {
            if (extraArgs is null)
            {
                throw new System.ArgumentNullException(nameof(extraArgs));
            }

            if (parameterInfos is null)
            {
                throw new ArgumentNullException(nameof(parameterInfos));
            }

            var args = new List<object?>();
            foreach (var p in parameterInfos)
            {
                if (extraArgs.TryGetValue(p.Name!, out var value))
                {
                    args.Add(value);
                }
                // else if (lake.TryGet(p.Name!, out var byNameValue))
                // {
                //     args.Add(byNameValue);
                // }
                else if (TryGet(p.ParameterType, out var byTypeValue))
                {
                    args.Add(byTypeValue);
                }
                else
                {
                    args.Add(p.DefaultValue);
                }
            }

            return args.ToArray();
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static bool TryGet(Type type, out object value)
        {
            value = Util.Helpers.Ioc.Create(type);

            return true;
        }
    }
}