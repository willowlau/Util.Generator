using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Util.Infrastructure;
using Util.Reflections;

namespace Util.Generators.ViewModels.Base
{
    /// <summary>
    /// VM视图注册器
    /// </summary>
    public class ViewModelRegistrar : IServiceRegistrar
    {
        /// <summary>
        /// 获取标识
        /// </summary>
        public static int GetId() => 100;

        /// <summary>
        /// 标识
        /// </summary>
        public int OrderId => GetId();

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool Enabled => true;

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="serviceContext">服务上下文</param>
        public Action Register(ServiceContext serviceContext)
        {
            return () =>
            {
                serviceContext.HostBuilder.ConfigureServices((_, services) =>
                {
                    RegisterDependency<IViewModelBase>(services, serviceContext.TypeFinder,
                        ServiceLifetime.Singleton);
                });
            };
        }

        /// <summary>
        /// 注册依赖
        /// </summary>
        private void RegisterDependency<TDependencyInterface>(IServiceCollection services, ITypeFinder finder,
            ServiceLifetime lifetime)
        {
            var types = finder.Find<TDependencyInterface>();
            foreach (var type in types)
            {
                RegisterType(services, type, lifetime);
            }
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        private void RegisterType(IServiceCollection services, Type type, ServiceLifetime lifetime)
        {
            var descriptor = new ServiceDescriptor(type, type, lifetime);
            services.TryAdd(descriptor);
        }
    }
}