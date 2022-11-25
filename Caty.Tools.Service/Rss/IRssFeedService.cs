using Caty.Tools.Model.Rss;
using Caty.Tools.Share.Service;

namespace Caty.Tools.Service.Rss
{
    public interface IRssFeedService: INormalService
    {
        public Task Add(RssFeed feed);

        public Task Update(RssFeed feed);

        public Task Delete(int id);

        public Task<IReadOnlyList<RssFeed>?> List();

        public Task<RssFeed?> Detail(int id);

        public Task<RssFeed?> GetFeedBySourceId(int sourceId);
    }
}
