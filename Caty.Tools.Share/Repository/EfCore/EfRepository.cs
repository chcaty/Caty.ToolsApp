using Caty.Tools.Share.Repository.UxSpecification;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.EfCore
{
    /// <summary>
    /// ef core 通用数据存储规范实例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    public class EfRepository<T, K> : IEfRepository<T, K> where T : Entity<K>, new()
    {
        /// <summary>
        /// 数据集
        /// </summary>
        protected readonly DbContext Context;

        /// <summary>
        /// 单数据表
        /// </summary>
        private readonly DbSet<T> _table;

        /// <summary>
        /// 实例化
        /// </summary>
        /// <param name="context"></param>
        public EfRepository(DbContext context)
        {
            Context = context ?? throw new ArgumentNullException(nameof(context));
            _table = Context.Set<T>();
        }

        /// <summary>
        /// 获取指定对像通过指定的唯一主键（如表中有多个唯一主键，请在实体设置中设置）
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual ValueTask<T?> FindById(K id)
        {
            _table.AsNoTracking();
            return _table.FindAsync(id);
        }

        /// <summary>
        /// 插入指定值对像数据
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual ValueTask<T> Insert(T t)
        {
            return new ValueTask<T>(_table.Add(t).Entity);
        }

        public ValueTask<T?> FirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return new ValueTask<T?>(_table.FirstOrDefaultAsync(predicate));
        }

        /// <summary>
        /// 查询返回默认条件数据
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public ValueTask<T?> FirstOrDefault(ISpecification<T> spec)
        {
            var speQueryResult = GetQuery(_table, spec);
            // 查询指定数据
            return new ValueTask<T?>(speQueryResult.FirstOrDefault());
        }

        /// <summary>
        /// 应用规约模式执行查询
        /// </summary>
        /// <param name="spec">规约条件</param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> FindAsync(ISpecification<T> spec)
        {
            var specificationResult = GetQuery(_table, spec);
            return await specificationResult.ToListAsync();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<IReadOnlyList<T>> FindAsync(IGridSpecification<T> spec)
        {
            var specificationResult = GetQuery(_table, spec);
            return await specificationResult.ToListAsync();
        }

        public IQueryable<T> QueryAsync(ISpecification<T> spec)
        {
            return GetQuery(_table, spec);
        }

        public IQueryable<T> QueryAsync(IGridSpecification<T> spec)
        {
            return GetQuery(_table, spec);
        }

        /// <summary>
        /// 更新指定对象
        /// </summary>
        /// <param name="t"></param>
        public virtual void Update(T t)
        {
            _table.Attach(t);
            Context.Entry(t).State = EntityState.Modified;
        }

        /// <summary>
        /// 删除指定唯一主键对应的值
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="id"></param>
        public virtual void Delete(K id)
        {
            _table.AsNoTracking();
            var existing = _table.Find(id);

            // 保证值存在
            if (existing != null)
                _table.Remove(existing);
        }

        /// <summary>
        /// 写入持久化
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual Task<int> SaveAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

        private static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criteria != null)
            {
                query = query.Where(specification.Criteria);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                var order = query.OrderBy(specification.OrderBy);
                if (specification.ThenBy != null)
                {
                    order = specification.ThenBy.Aggregate(order, (current, expression) => current.ThenBy(expression));
                }
                else if (specification.ThenByDescending != null)
                {
                    order = specification.ThenByDescending.Aggregate(order,
                        (current, expression) => current.ThenByDescending(expression));
                }
                query = order;

            }
            else if (specification.OrderByDescending != null)
            {
                var order = query.OrderByDescending(specification.OrderByDescending);
                if (specification.ThenBy != null)
                {
                    order = specification.ThenBy.Aggregate(order, (current, expression) => current.ThenBy(expression));
                }
                else if (specification.ThenByDescending != null)
                {
                    order = specification.ThenByDescending.Aggregate(order,
                        (current, expression) => current.ThenByDescending(expression));
                }
                query = order;
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip((specification.Skip - 1) * specification.Take)
                    .Take(specification.Take);
            }

            return query;
        }

        private static IQueryable<T> GetQuery(IQueryable<T> inputQuery,IGridSpecification<T> specification)
        {
            var query = inputQuery;

            if (specification.Criterias.Count > 0)
            {
                var expr = specification.Criterias.First();
                for (var i = 1; i < specification.Criterias.Count; i++)
                {
                    expr = expr.And(specification.Criterias[i]);
                }

                query = query.Where(expr);
            }

            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));
            query = specification.IncludeStrings.Aggregate(query, (current, include) => current.Include(include));

            if (specification.OrderBy != null)
            {
                query = query.OrderBy(specification.OrderBy);
            }
            else if (specification.OrderByDescending != null)
            {
                query = query.OrderByDescending(specification.OrderByDescending);
            }

            if (specification.GroupBy != null)
            {
                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            }

            if (specification.IsPagingEnabled)
            {
                query = query.Skip(specification.Skip - 1)
                    .Take(specification.Take);
            }

            return query;
        }

        /// <summary>
        /// 规约模式获取数据总量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<int> Count(ISpecification<T> spec)
        {
            var specificationResult = GetQuery(_table, spec);
            return await specificationResult.CountAsync();
        }

        /// <summary>
        /// 规约模式获取数据总量
        /// </summary>
        /// <param name="spec"></param>
        /// <returns></returns>
        public async Task<int> Count(IGridSpecification<T> spec)
        {
            var specificationResult = GetQuery(_table, spec);
            return await specificationResult.CountAsync();
        }

        /// <summary>
        /// 批量更新指定对象
        /// </summary>
        /// <param name="tList"></param>
        public void Update(IEnumerable<T> tList)
        {
            foreach (var t in tList)
            {
                _table.Attach(t);
                Context.Entry(t).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// 批量插入对象
        /// </summary>
        /// <param name="tList"></param>
        public async void Insert(IEnumerable<T> tList)
        {
            await _table.AddRangeAsync(tList);
        }

        /// <summary>
        /// 批量删除对象
        /// </summary>
        /// <param name="list"></param>
        public void Delete(IEnumerable<T> list)
        {
            _table.RemoveRange(list);
        }
    }
}
