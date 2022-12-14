using Microsoft.EntityFrameworkCore;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// 通用指定数据域数据操作
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TContext"></typeparam>
    /// <typeparam name="TK"></typeparam>
    public class GenericEfRepositoryImpl<TEntity, TK, TContext> : EfRepository<TEntity, TK>
        where TEntity : Entity<TK>, new()
        where TContext : DbContext
    {
        /// <summary>
        /// 初使实例化
        /// </summary>
        /// <param name="context"></param>
        public GenericEfRepositoryImpl(TContext context) : base(context) { }

        /// <summary>
        /// 异步保存应用到数据库
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }
    }
}
