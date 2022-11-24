using Caty.Tools.Model.Rss;
using Microsoft.EntityFrameworkCore;

namespace Caty.Tools.Model.Context
{
    public class RssDbContext : DbContext
    {
        public RssDbContext(DbContextOptions<RssDbContext> options) : base(options) { }

        public DbSet<RssSource> RssSources { get; set; }
        public DbSet<RssFeed> RssFeeds { get; set; }
        public DbSet<RssItem> RssItems { get; set; }
    }
}
