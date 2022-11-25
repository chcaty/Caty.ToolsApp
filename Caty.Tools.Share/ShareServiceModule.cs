using Caty.Tools.Share.Repository.EfCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

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

        /// <summary>
        /// 加载应用程序中指定类型到注册的服务中，注意：请每一个要注册的服务保持一个且唯一
        /// </summary>
        /// <param name="services">注入服务</param>
        /// <param name="type">库中的任意类型</param>
        /// <param name="lifetime">生命周期，默认为ServiceLifetime.Transient</param>
        /// <returns></returns>
        public static IServiceCollection LoadAssemblyService(this IServiceCollection services, Type type, ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            var ass = Assembly.GetCallingAssembly();
            var fullName = $"{type.Namespace}.{type.Name}";
            var types = ass.GetTypes().Where(t => t.IsClass && t.GetInterfaces().Any(i => $"{i.Namespace}.{i.Name}" == fullName)).ToArray();

            foreach (var item in types)
            {
                var inters = item.GetInterfaces().Where(i => $"{i.Namespace}.{i.Name}" == fullName || i.GetInterfaces().Any(ii => $"{ii.Namespace}.{ii.Name}" == fullName)).ToList();
                if (!inters.Any())
                    continue;

                foreach (var inter in inters.Where(inter => inter != null).Where(inter => !inter.ContainsGenericParameters))
                {
                    switch (lifetime)
                    {
                        case ServiceLifetime.Singleton:
                            services.TryAddTransient(inter, item);
                            break;
                        case ServiceLifetime.Scoped:
                            services.TryAddScoped(inter, item);
                            break;
                        case ServiceLifetime.Transient:
                            services.TryAddTransient(inter, item);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(lifetime), lifetime, null);
                    }
                }
            }
            return services;
        }
    }
}
