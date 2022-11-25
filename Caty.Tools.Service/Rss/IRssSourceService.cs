using Caty.Tools.Model.Rss;
using Caty.Tools.Share.Service;

namespace Caty.Tools.Service.Rss
{
    public interface IRssSourceService:INormalService
    {
        public Task Add(RssSource source);

        public Task Update(RssSource source);

        public Task Delete(int id);

        public Task<IReadOnlyList<RssSource>?> List(bool isEnabled);

        public Task<RssSource?> Detail(int id);

    }
}
