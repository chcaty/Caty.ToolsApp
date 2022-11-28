using Caty.Tools.Model.Context;
using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Specification.Rss;
using Caty.Tools.Share.Repository.EfCore;

namespace Caty.Tools.Service.Rss
{
    internal class RssItemService : IRssItemService
    {
        private readonly IEfRepository<RssItem, int> _repository;

        public RssItemService(IUnitOfWorkEf<RssDbContext> unitOfWork)
        {
            _repository = unitOfWork.GetRepository<RssItem, int>();
        }

        public async Task Add(RssItem item)
        {
            await _repository.Insert(item);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
            _repository.Delete(id);
            await _repository.SaveAsync();
        }

        public async Task<RssItem?> Detail(int id)
        {
            var item = await _repository.FindById(id);
            return item;
        }

        public async Task<IReadOnlyList<RssItem>?> List(int feedId)
        {
            var items = await _repository.FindAsync(new RssItemSpecification(feedId));
            return items;
        }

        public async Task Update(RssItem item)
        {
            var rssItem = await _repository.FindById(item.Id);
            if (rssItem == null) return;
            _repository.Update(item);
            await _repository.SaveAsync();
        }

        public async Task Add(List<RssItem> item)
        {
            _repository.Insert(item);
            await _repository.SaveAsync();
        }

        public async Task<bool> CheckRepeat(int feedId, string? url)
        {
            var repeat = await _repository.FirstOrDefault(t=>t.FeedId == feedId && t.ContentLink== url);
            return repeat != null;
        }
    }
}
