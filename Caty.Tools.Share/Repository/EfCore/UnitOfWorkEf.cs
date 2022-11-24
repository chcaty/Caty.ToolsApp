using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// 通用工作单元
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class UnitOfWorkEf<TContext> : IUnitOfWorkEf<TContext> where TContext : DbContext
    {
        private readonly TContext _context;
        private bool _disposed;
        private IDbContextTransaction _objTran;
        private IDictionary<string, object> _repositories;

        /// <summary>
        /// 实使化工作单元
        /// </summary>
        /// <param name="context"></param>
        public UnitOfWorkEf(TContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 当前数据库上下文对像
        /// </summary>
        public virtual TContext Context => _context;

        /// <summary>
        /// 事务提交
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task Commit(CancellationToken cancellationToken = default)
        {
            return _objTran != null ? _objTran.CommitAsync(cancellationToken) : Task.CompletedTask;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CreateTransaction(CancellationToken cancellationToken = default)
        {
            _objTran = await _context.Database.BeginTransactionAsync(cancellationToken);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
                if (disposing)
                {
                    _objTran?.Dispose();
                    _context?.Dispose();
                    _repositories?.Clear();

                    _objTran = null;
                    _repositories = null;
                }

            _disposed = true;
        }

        /// <summary>
        ///
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public ValueTask Rollback(CancellationToken cancellationToken = default)
        {
            _objTran?.RollbackAsync(cancellationToken);
            return _objTran.DisposeAsync();
        }

        /// <summary>
        /// 保存进数据库中
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// 创建通用实体对像执行操作
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="K"></typeparam>
        /// <returns></returns>
        public IEfRepository<TEntity, K> GetRepository<TEntity, K>() where TEntity : Entity<K>, new() where K : struct
        {
            // 默认8表
            _repositories ??= new Dictionary<string, object>(8);

            var type = typeof(TEntity).Name;
            // 如果存在就直接返回
            if (_repositories.ContainsKey(type))
                return (IEfRepository<TEntity, K>)_repositories[type];

            var repositoryType = typeof(EfRepository<,>);
            var typeArgs = new[] { typeof(TEntity), typeof(K) };
            var constructed = repositoryType.MakeGenericType(typeArgs);
            var repositoryInstance = Activator.CreateInstance(constructed, _context);

            if (repositoryInstance == null)
                throw new NullReferenceException($"创建指定类型通用EfRepository失败，创建类型是：{nameof(constructed)}");
            _repositories.Add(type, repositoryInstance);
            return (IEfRepository<TEntity, K>)_repositories[type];
        }
    }
}
