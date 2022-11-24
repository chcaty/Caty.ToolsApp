using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.UxSpecification
{
    /// <summary>
    /// Specification规约模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> Criteria { get; }
        public IList<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();
        public IList<string> IncludeStrings { get; } = new List<string>();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public IList<Expression<Func<T, object>>> ThenBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public IList<Expression<Func<T, object>>> ThenByDescending { get; private set; }
        public Expression<Func<T, object>> GroupBy { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public int Take { get; private set; }
        public int Skip { get; private set; }
        public bool IsPagingEnabled { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        protected void ApplyIncludeList(IEnumerable<Expression<Func<T, object>>> includes)
        {
            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeExpression"></param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includes"></param>
        protected void ApplyIncludeList(IEnumerable<string> includes)
        {
            foreach (var include in includes)
            {
                AddInclude(include);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="includeString"></param>
        protected void AddInclude(string includeString)
        {
            IncludeStrings.Add(includeString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByExpression"></param>
        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression) =>
            OrderBy = orderByExpression;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="thenByExpression"></param>
        protected void ApplyThenBy(IList<Expression<Func<T, object>>> thenByExpression) => ThenBy = thenByExpression;

        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression) =>
            OrderByDescending = orderByDescendingExpression;


        protected void ApplyThenByDescending(IList<Expression<Func<T, object>>> thenByDescendingExpression) => ThenByDescending = thenByDescendingExpression;

        protected void ApplyGroupBy(Expression<Func<T, object>> groupByExpression) =>
            GroupBy = groupByExpression;

        protected void ApplySorting(string sort)
        {
            this.ApplySorting(sort, nameof(ApplyOrderBy), nameof(ApplyOrderByDescending));
        }

        private Func<T, bool> _compiledExpression;
        private Func<T, bool> CompiledExpression
        {
            get { return _compiledExpression ??= Criteria.Compile(); }
        }

        //This specs will result in generating additional Where statement in Sql queries (1=1)

        /// <summary>
        /// (1=1)
        /// </summary>
        public static readonly Specification<T> All = new IdentitySpecification<T>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(T obj)
        {
            return CompiledExpression(obj);
        }
    }
}
