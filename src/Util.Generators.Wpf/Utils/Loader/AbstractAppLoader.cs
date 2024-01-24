using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Util.Generators.Utils.MethodManagement;

namespace Util.Generators.Utils.Loader
{
    /// <summary>
    /// 抽象App加载者
    /// </summary>
    public abstract class AbstractAppLoader
    {
        /// <summary>
        /// 完成事件
        /// </summary>
        public event EventHandler<StepFinishedEventArgs> StepFinished = null!;

        /// <summary>
        /// 成功事件
        /// </summary>
        public event EventHandler Succeeded = null!;

        /// <summary>
        /// 失败事件
        /// </summary>
        public event EventHandler<AppLoaderFailedEventArgs> Failed = null!;

        /// <summary>
        /// 步骤集合
        /// </summary>
        private readonly IEnumerable<MethodInfo> _stepMethods;

        /// <summary>
        /// 日志
        /// </summary>
        protected ILogger<AbstractAppLoader> Logger { get; }

        /// <summary>
        /// 构造函数
        /// </summary>
        internal AbstractAppLoader()
        {
            Logger = Util.Helpers.Ioc.Create<ILogger<AbstractAppLoader>>();
            _stepMethods = FindStepMethods();
        }

        /// <summary>
        /// 查找方法的级别
        /// </summary>
        private const BindingFlags FindingFlags = BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance;

        /// <summary>
        /// 获取步骤
        /// </summary>
        /// <returns></returns>
        protected virtual IEnumerable<MethodInfo> FindStepMethods()
        {
            return from method in GetType().GetMethods(FindingFlags)
                   where method.GetCustomAttribute<StepAttribute>() != null
                   orderby method.GetCustomAttribute<StepAttribute>()!.Step ascending
                   select method;
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        /// <returns></returns>
        public async Task LoadAsync()
        {
            await Task.Run(Load);
        }

        /// <summary>
        /// 加载方法
        /// </summary>
        public void Load()
        {
            for (int i = 0; i < _stepMethods.Count(); i++)
            {
                try
                {
                    // Logger.LogInformation($"Loading: {_stepMethods.ElementAt(i).Name}()");
                    var methodProxy = new MethodProxy(this, _stepMethods.ElementAt(i));
                    methodProxy.Invoke();
                    StepFinished?.Invoke(this, new StepFinishedEventArgs((uint)i, (uint)_stepMethods.Count()));
                }
                catch (Exception e)
                {
                    OnError("Uncaught error", new AppLoadingException(_stepMethods.ElementAt(i).Name, e));
                    return;
                }
            }

            // Logger.LogInformation("Done!");
            Succeeded?.Invoke(this, EventArgs.Empty);
        }

        /// <summary>
        /// 错误方法
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="e"></param>
        protected virtual void OnError(string msg, AppLoadingException e)
        {
            Logger.LogError($"can't load application: {msg}");
            Logger.LogError($"message: {e.InnerException}");
            Logger.LogError($"source: {e.InnerException?.Source}");
            Logger.LogError($"stack_trace: {e.InnerException!.StackTrace}");
            Thread.Sleep(200); //至少等待日志被写入到文件中
            Failed?.Invoke(this, new AppLoaderFailedEventArgs(e));
        }
    }
}