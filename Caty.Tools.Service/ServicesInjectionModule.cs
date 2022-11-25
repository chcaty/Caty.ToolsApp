using Caty.Tools.Share;
using Caty.Tools.Share.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Caty.Tools.Service
{
    public static class ServicesInjectionModule
    {
        /// <summary>
        /// 注册业务服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddUxServicesModule(this IServiceCollection services)
        {
            services.LoadAssemblyService(typeof(INormalService));
        }
    }
}
