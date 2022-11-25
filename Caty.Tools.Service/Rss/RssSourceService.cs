using Caty.Tools.Model.Context;
using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Specification.Rss;
using Caty.Tools.Share.Repository.EfCore;

namespace Caty.Tools.Service.Rss
{
    internal class RssSourceService : IRssSourceService
    {
        private readonly IEfRepository<RssSource, int> _repository;

        public RssSourceService(IUnitOfWorkEf<RssDbContext> unitOfWork)
        {
            _repository = unitOfWork.GetRepository<RssSource, int>();
        }

        public async Task Add(RssSource source)
        {
            await _repository.Insert(source);
            await _repository.SaveAsync();
        }

        public async Task Delete(int id)
        {
             _repository.Delete(id);
            await _repository.SaveAsync();
        }

        public async Task<RssSource?> Detail(int id)
        {
            var source = await _repository.FindById(id);
            return source;
        }

        public async Task<IReadOnlyList<RssSource>?> List(bool isEnabled)
        {
            var sources = await _repository.FindAsync(new RssSourceByIsEnabledSpecification(isEnabled));
            return sources;
        }

        public async Task Update(RssSource source)
        {
            var rssSource = await _repository.FindById(source.Id);
            if (rssSource == null) return;
            _repository.Update(source);
            await _repository.SaveAsync();
        }
    }
}
