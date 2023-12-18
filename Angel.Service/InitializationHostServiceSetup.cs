using Microsoft.Extensions.DependencyInjection;

namespace Angel.Service
{
    public static class InitializationHostServiceSetup
    {
        /// <summary>
        /// 应用初始化服务注入
        /// </summary>
        /// <param name="services"></param>
        public static void AddInitializationHostServiceSetup(this IServiceCollection services)
        {
            if (services is null)
            {
                ArgumentNullException.ThrowIfNull(nameof(services));
            }
            services.AddHostedService<StartApplicationService>();
        }
    }
}
