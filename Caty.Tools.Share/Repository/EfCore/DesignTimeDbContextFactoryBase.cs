using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// 创建应用程序运行时工程实例
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public abstract class DesignTimeDbContextFactoryBase<TContext>
        : IDesignTimeDbContextFactory<TContext> where TContext : DbContext
    {
        /// <summary>
        /// 创建应用实例
        /// </summary>
        /// <param name="args">使用的参数</param>
        /// <returns></returns>
        public TContext CreateDbContext(string[] args)
        {
            return Create(Directory.GetCurrentDirectory(),
                Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT"));
        }

        /// <summary>
        /// 创建一个新连接对象
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        protected abstract TContext CreateNewInstance(string connectionString);

        /// <summary>
        /// 创建连接实例
        /// </summary>
        /// <returns></returns>
        public TContext Create()
        {
            // 获取默认环境的链接
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var basePath = AppContext.BaseDirectory;
            return Create(basePath, environmentName);
        }

        /// <summary>
        /// 创建连接操作
        /// </summary>
        /// <param name="basePath">默认路径地址</param>
        /// <param name="environmentName">迁移的环境配置</param>
        /// <returns></returns>
        private TContext Create(string basePath, string environmentName)
        {
            if (string.IsNullOrEmpty(basePath))
            {
                basePath = Directory.GetCurrentDirectory();
            }

            var builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile($"appsettings.{environmentName}.json", true)
                .AddJsonFile($"appsettings.json", true)
                .AddEnvironmentVariables();

            var config = builder.Build();

            // 默认连接
            var connstr = config.GetConnectionString("Default");
            if (string.IsNullOrWhiteSpace(connstr))
            {
                throw new InvalidOperationException(
                    $"Could not find a connection string named 'Default'.the args basePath:{basePath},environmentName:{environmentName}");
            }
            return CreateNewInstance(connstr);
        }

    }
}
