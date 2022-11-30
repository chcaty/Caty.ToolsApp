using Caty.Tools.Share.Repository.UxSpecification;
using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// 通用数据处理
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="TK"></typeparam>
    public interface IEfRepository<T, TK> where T : Entity<TK>, new()
    {
        /// <summary>
        /// 获取指定对像通过指定的唯一主键（如表中有多个唯一主键，请在实体设置中设置）
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ValueTask<T?> FindById(TK id);
        /// <summary>
        /// 插入指定数据到数据库
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        ValueTask<T> Insert(T t);
        /// <summary>
        /// 更新指定数据到数据库，唯一关键字必须填写。
        /// </summary>
        /// <param name="t"></param>
        void Update(T t);
        /// <summary>
        /// 从数据库中删除指定条件的数据
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="id"></param>
        void Delete(TK id);
        /// <summary>
        /// 根据条件查找指定数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        ValueTask<T?> FirstOrDefault(Expression<Func<T, bool>> predicate);
        /// <summary>
        /// 根据条件查找指定数据
        /// </summary>
        /// <param name="spec">规约条件</param>
        /// <returns></returns>
        ValueTask<T?> FirstOrDefault(ISpecification<T> spec);
        /// <summary>
        /// 根据条件查询指定数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> FindAsync(ISpecification<T> spec);
        /// <summary>
        /// 分页获取指定数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<IReadOnlyList<T>> FindAsync(IGridSpecification<T> spec);
        /// <summary>
        /// 根据条件查询指定数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IQueryable<T> QueryAsync(ISpecification<T> spec);
        /// <summary>
        /// 分页获取指定数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        IQueryable<T> QueryAsync(IGridSpecification<T> spec);
        /// <summary>
        /// 异步保存
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveAsync(CancellationToken cancellationToken = default);
        /// <summary>
        /// 根据条件获取数据总量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<int> Count(ISpecification<T> spec);
        /// <summary>
        /// 根据条件获取数据总量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        Task<int> Count(IGridSpecification<T> spec);
        /// <summary>
        /// 批量更新指定数据到数据库，唯一关键字必须填写。
        /// </summary>
        /// <param name="tList"></param>
        void Update(IEnumerable<T> tList);
        /// <summary>
        /// 插入指定数据到数据库
        /// </summary>
        /// <param name="tList"></param>
        /// <returns></returns>
        void Insert(IEnumerable<T> tList);
        /// <summary>
        /// 从数据库中删除指定条件的数据
        /// </summary>
        /// <typeparam name="TK"></typeparam>
        /// <param name="list"></param>
        void Delete(IEnumerable<T> list);
    }
}
