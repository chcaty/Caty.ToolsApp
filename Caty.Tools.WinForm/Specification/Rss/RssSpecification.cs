using Caty.Tools.Model.Rss;
using Caty.Tools.Share.Repository.UxSpecification;
using System.Linq.Expressions;

namespace Caty.Tools.WinForm.Specification.Rss
{
    public class RssSourceByIsEnabledSpecification : Specification<RssSource>
    {
        public RssSourceByIsEnabledSpecification(bool isEnabled)
        {
            Criteria = x =>  x.IsEnabled == isEnabled;
        }

        public override Expression<Func<RssSource, bool>> Criteria { get; }
    }
}
