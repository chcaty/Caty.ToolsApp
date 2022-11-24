using Caty.Tools.Share.Repository.EfCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Caty.Tools.Share
{
    public static class ShareServiceModule
    {
        /// <summary>
        /// 平台服务共用注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddShareServiceModule(this IServiceCollection services)
        {
            // ef 数据操作设置
            services.TryAddTransient(typeof(GenericEfRepositoryImpl<,,>));
            services.TryAddTransient(typeof(IUnitOfWorkEf<>), typeof(UnitOfWorkEf<>));
        }
    }
}
