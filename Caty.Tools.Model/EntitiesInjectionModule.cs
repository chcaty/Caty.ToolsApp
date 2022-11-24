using Caty.Tools.Model.Context;
using Caty.Tools.Share.Repository.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caty.Tools.Model
{
    public static class EntitiesInjectionModule
    {
        /// <summary>
        /// 数据库加载注入
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static void AddApplicationEntitiesModule(this IServiceCollection services, IConfiguration configuration)
        {
            // 常规通用数据库连接
            var connectionDict = new Dictionary<DatabaseConnectionName, string>
            {
                { DatabaseConnectionName.Default, configuration.GetConnectionString("Default") },
                { DatabaseConnectionName.Connection1, configuration.GetConnectionString("dbConnection1") }
            };

            // 一般数据库操作
            services.TryAddTransient<IDictionary<DatabaseConnectionName, string>>(x => connectionDict);

            services.AddDbContextPool<RssDbContext>(
                options => options
                    .UseSqlite(configuration.GetConnectionString("Default"),
                        providerOptions => providerOptions.CommandTimeout(30).UseRelationalNulls())
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll)
                    .EnableDetailedErrors()
                    .EnableSensitiveDataLogging().EnableServiceProviderCaching(),
                64);
        }
    }
}
