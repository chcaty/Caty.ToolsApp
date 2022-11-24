using Caty.Tools.Share.Repository.EfCore;
using Microsoft.EntityFrameworkCore;

namespace Caty.Tools.Model.Context
{
    public class RssDbContextFactory:DesignTimeDbContextFactoryBase<RssDbContext>
    {
        protected override RssDbContext CreateNewInstance(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RssDbContext>(new DbContextOptions<RssDbContext>());
            // 使用的数据库类型
            optionsBuilder.UseSqlite(connectionString, ops => ops.UseRelationalNulls());
            var options = optionsBuilder.Options;
            return new RssDbContext(options);
        }
    }
}
