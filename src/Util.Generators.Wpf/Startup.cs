using System;
using Util.Infrastructure;

namespace Util.Generators
{
    /// <summary>
    /// 注册相关服务信息
    /// </summary>
    public class Startup : IServiceRegistrar
    {
        /// <inheritdoc />
        public int OrderId => 1000;

        /// <inheritdoc />
        public bool Enabled => true;

        /// <inheritdoc />
        public Action Register(ServiceContext serviceContext)
        {
            serviceContext.HostBuilder.ConfigureServices((context, services) =>
            {

            });
            return null!;
        }
    }
}
