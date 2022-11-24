using System.Linq.Expressions;
using System.Reflection;

namespace Caty.Tools.Share.Repository.UxSpecification
{
    /// <summary>
    /// Specification Extensions
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// And
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ISpecification<T> And<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        /// <summary>
        /// Or
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static ISpecification<T> Or<T>(this ISpecification<T> left, ISpecification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        /// <summary>
        /// NOT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inner"></param>
        /// <returns></returns>
        public static ISpecification<T> Negate<T>(this ISpecification<T> inner)
        {
            return new NotSpecification<T>(inner);
        }

        /// <summary>
        /// ApplySorting
        /// </summary>
        /// <param name="rootSpecification"></param>
        /// <param name="sort"></param>
        /// <param name="orderByDescendingMethodName"></param>
        /// <param name="groupByMethodName"></param>
        public static void ApplySorting(this IRootSpecification rootSpecification,
            string sort,
            string orderByDescendingMethodName,
            string groupByMethodName)
        {
            if (string.IsNullOrEmpty(sort)) return;

            const string descendingSuffix = "Desc";

            var descending = sort.EndsWith(descendingSuffix, StringComparison.Ordinal);
            var propertyName = sort.Substring(0, 1).ToUpperInvariant() +
                               sort.Substring(1, sort.Length - 1 - (descending ? descendingSuffix.Length : 0));

            var specificationType = rootSpecification.GetType().BaseType;
            var targetType = specificationType?.GenericTypeArguments[0];
            var property = targetType!.GetRuntimeProperty(propertyName) ??
                           throw new InvalidOperationException($"Because the property {propertyName} does not exist it cannot be sorted.");

            var lambdaParamX = Expression.Parameter(targetType, "x");

            var propertyReturningExpression = Expression.Lambda(
                Expression.Convert(
                    Expression.Property(lambdaParamX, property),
                    typeof(object)),
                lambdaParamX);

            if (descending)
            {
                specificationType?.GetMethod(
                        orderByDescendingMethodName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(rootSpecification, new object[] { propertyReturningExpression });
            }
            else
            {
                specificationType?.GetMethod(
                        groupByMethodName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    ?.Invoke(rootSpecification, new object[] { propertyReturningExpression });
            }
        }

        public static IEnumerable<T> ApplyPaging<T>(this IEnumerable<T> t, int pageIndex, int pageSize)
        {
            return t.Skip((pageIndex - 1) * pageSize).Take(pageSize);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> a, Expression<Func<T, bool>> b)
        {
            var p = a.Parameters[0];

            var visitor = new SubstExpressionVisitor();
            visitor.Subst[b.Parameters[0]] = p;

            Expression body = Expression.And(a.Body, visitor.Visit(b.Body));
            return Expression.Lambda<Func<T, bool>>(body, p);
        }

        private class SubstExpressionVisitor : ExpressionVisitor
        {
            public readonly Dictionary<Expression, Expression> Subst = new();

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return Subst.TryGetValue(node, out var newValue) ? newValue : node;
            }
        }
    }
}
