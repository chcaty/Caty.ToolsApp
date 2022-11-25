using Caty.Tools.Model.Context;
using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Specification.Rss;
using Caty.Tools.Share.Repository.EfCore;

namespace Caty.Tools.Service.Rss
{
    internal class RssFeedService : IRssFeedService
    {
        private readonly IEfRepository<RssFeed, int> _repository;

        public RssFeedService(IUnitOfWorkEf<RssDbContext> unitOfWork)
        {
            _repository = unitOfWork.GetRepository<RssFeed, int>();
        }

        public async Task Add(RssFeed feed)
        {
            await _repository.Insert(feed);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
        }

        public async Task<RssFeed?> Detail(int id)
        {
            var feed = await _repository.FindById(id);
            return feed;
        }

        public async Task<RssFeed?> GetFeedBySourceId(int sourceId)
        {
            var feed = await _repository.FirstOrDefault(t=>t.SourceId == sourceId);
            return feed;
        }

        public async Task<IReadOnlyList<RssFeed>?> List()
        {
            var rssSources = await _repository.FindAsync(new RssFeedSpecification());
            return rssSources;
        }

        public async Task Update(RssFeed feed)
        {
            var rssFeed = await _repository.FindById(feed.Id);
            if (rssFeed == null) return;
            _repository.Update(feed);
            await _repository.SaveAsync();
        }
    }
}
