using Caty.Tools.Model.Rss;
using Caty.Tools.Share.Repository.UxSpecification;
using System.Linq.Expressions;

namespace Caty.Tools.Service.Specification.Rss
{
    public class RssSourceByIsEnabledSpecification : Specification<RssSource>
    {
        public RssSourceByIsEnabledSpecification(bool isEnabled)
        {
            Criteria = x => x.IsEnabled == isEnabled;
        }

        public override Expression<Func<RssSource, bool>> Criteria { get; }
    }

    public class RssFeedSpecification : Specification<RssFeed>
    {
        public RssFeedSpecification()
        {
            Criteria = x => true;
        }

        public override Expression<Func<RssFeed, bool>> Criteria { get; }
    }

    public class RssItemSpecification : Specification<RssItem>
    {
        public RssItemSpecification(int feedId)
        {
            Criteria = x => x.FeedId == feedId;
            ApplyOrderBy(x => x.PublishDate);
        }

        public override Expression<Func<RssItem, bool>> Criteria { get; }
    }
}
