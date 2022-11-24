using System.Linq.Expressions;

namespace Caty.Tools.Share.Repository.UxSpecification
{
    /// <summary>
    /// IdentitySpecification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal sealed class IdentitySpecification<T> : Specification<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public override Expression<Func<T, bool>> Criteria => o => true;
    }
}
