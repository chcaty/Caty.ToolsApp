using Microsoft.EntityFrameworkCore;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// 数据操作工作单元
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IUnitOfWorkEf<out TContext> : IDisposable where TContext : DbContext
    {
        /// <summary>
        ///  要操作的DbContext
        /// </summary>
        TContext Context { get; }

        /// <summary>
        /// 创建事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CreateTransaction(CancellationToken cancellationToken = default);

        /// <summary>
        /// 提交事务
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task Commit(CancellationToken cancellationToken = default);

        /// <summary>
        /// 回滚
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        ValueTask Rollback(CancellationToken cancellationToken = default);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// 创建工作单元中的应用DbSet
        /// </summary>
        /// <typeparam name="TEntity">当前表对应用引用类型数据</typeparam>
        /// <typeparam name="K">当前操作的类型中的主键字段</typeparam>
        /// <returns>返回这个类型的通用操作实例</returns>
        IEfRepository<TEntity, K> GetRepository<TEntity, K>() where TEntity : Entity<K>, new() where K : struct;
    }
}
