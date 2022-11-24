using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.UxSpecification
{
    /// <summary>
    /// IRootSpecification
    /// </summary>
    public interface IRootSpecification { }

    /// <summary>
    /// https://stackoverflow.com/questions/63082758/ef-core-specification-pattern-add-all-column-for-sorting-data-with-custom-specif
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ISpecification<T> : IRootSpecification
    {
        Expression<Func<T, bool>> Criteria { get; }
        IList<Expression<Func<T, object>>> Includes { get; }
        IList<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        IList<Expression<Func<T, object>>> ThenBy { get; }
        IList<Expression<Func<T, object>>> ThenByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }

        bool IsSatisfiedBy(T obj);
    }

    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGridSpecification<T> : IRootSpecification
    {
        IList<Expression<Func<T, bool>>> Criterias { get; }
        IList<Expression<Func<T, object>>> Includes { get; }
        IList<string> IncludeStrings { get; }
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
        Expression<Func<T, object>> GroupBy { get; }
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; set; }
    }
}
