using Caty.Tools.Model.Rss;
using Caty.Tools.Share.Service;

namespace Caty.Tools.Service.Rss
{
    public interface IRssItemService:INormalService
    {
        public Task Add(RssItem item);

        public Task Update(RssItem item);

        public Task Delete(int id);

        public Task<IReadOnlyList<RssItem>?> List();

        public Task<RssItem?> Detail(int id);
    }
}
