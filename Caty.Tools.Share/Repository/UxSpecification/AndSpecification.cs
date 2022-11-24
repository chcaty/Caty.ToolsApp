using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.UxSpecification
{
    /// <summary>
    /// AndSpecification
    /// </summary>
    /// <typeparam name="T">要处理的执行的对象</typeparam>
    public class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        /// <summary>
        /// AndSpecification
        /// </summary>
        public Expression<Func<T, bool>> Criteria
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");
                var newExpr = Expression.Lambda<Func<T, bool>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.Criteria, objParam),
                        Expression.Invoke(_right.Criteria, objParam)
                    ),
                    objParam
                );
                return newExpr;
            }
        }

        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public IList<string> IncludeStrings { get; } = new List<string>();

        public Expression<Func<T, object>> OrderBy
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");
                if (_right.OrderBy == null && _left.OrderBy == null)
                {
                    return null;
                }

                if (_left.OrderBy == null && _right.OrderBy != null)
                {
                    return _right.OrderBy;
                }

                if (_right.OrderBy == null && _left.OrderBy != null)
                {
                    return _left.OrderBy;
                }

                var newExpr = Expression.Lambda<Func<T, object>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.OrderBy!, objParam),
                        Expression.Invoke(_right.OrderBy!, objParam)
                    ),
                    objParam
                );
                return newExpr;

            }
        }
        public IList<Expression<Func<T, object>>> ThenBy => _right.ThenBy;

        public Expression<Func<T, object>> OrderByDescending
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");
                if (_right.OrderByDescending == null && _left.OrderByDescending == null)
                {
                    return null;
                }

                if (_left.OrderByDescending == null && _right.OrderByDescending != null)
                {
                    return _right.OrderByDescending;
                }

                if (_right.OrderByDescending == null && _left.OrderByDescending != null)
                {
                    return _left.OrderByDescending;
                }

                var newExpr = Expression.Lambda<Func<T, object>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.OrderByDescending!, objParam),
                        Expression.Invoke(_right.OrderByDescending!, objParam)
                    ),
                    objParam
                );
                return newExpr;
            }
        }

        public IList<Expression<Func<T, object>>> ThenByDescending => _right.ThenByDescending;
        public Expression<Func<T, object>> GroupBy
        {
            get
            {
                var objParam = Expression.Parameter(typeof(T), "obj");
                if (_right.GroupBy == null && _left.GroupBy == null)
                {
                    return null;
                }

                if (_left.GroupBy == null && _right.GroupBy != null)
                {
                    return _right.GroupBy;
                }

                if (_right.GroupBy == null && _left.GroupBy != null)
                {
                    return _left.GroupBy;
                }

                var newExpr = Expression.Lambda<Func<T, object>>(
                    Expression.AndAlso(
                        Expression.Invoke(_left.GroupBy!, objParam),
                        Expression.Invoke(_right.GroupBy!, objParam)
                    ),
                    objParam
                );
                return newExpr;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public int Take => _right.Take;
        public int Skip => _right.Skip;
        public bool IsPagingEnabled => _right.IsPagingEnabled;
        public bool IsSatisfiedBy(T obj)
        {
            throw new NotImplementedException();
        }
    }
}
