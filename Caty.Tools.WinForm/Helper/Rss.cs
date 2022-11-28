using Caty.Tools.Model.Rss;
using System.ServiceModel.Syndication;
using System.Xml;

namespace Caty.Tools.WinForm.Helper;

internal static class Rss
{
    public static RssFeed? GetRssFeed(string rssUri)
    {
        var sf = SyndicationFeed.Load(XmlReader.Create(rssUri));
        if (sf == null) return null;
        var feed = new RssFeed
        {
            Title = sf.Title?.Text,
            FeedCode = sf.Id,
            LastUpdatedTime = sf.LastUpdatedTime.DateTime,
            Description = sf.Description?.Text,

        };
        if (sf.Links.Count > 0)
        {
            feed.Link = $"{sf.Links[0].Uri}";
        }

        if (sf.Authors.Count > 0 && !string.IsNullOrEmpty(sf.Authors[0].Uri))
        {
            feed.Author = $"{sf.Authors[0].Uri}";
        }
        return feed;
    }

    public static List<RssFeed>? GetRssFeeds(IEnumerable<string> rssUris)
    {
        return (List<RssFeed>?)rssUris.Select(GetRssFeed);
    }

    public static List<RssItem>? GetRssItems(string rssUri)
    {
        var sf = SyndicationFeed.Load(XmlReader.Create(rssUri));
        var itemList = sf?.Items?.Select(it => new RssItem()
        {
            ItemId = it.Id,
            Author = it.Authors.Count == 0 ? "" : $"{it.Authors[0].Name}",
            AuthorLink = it.Authors.Count == 0 ? "" : $"{it.Authors[0].Uri}",
            AuthorEmail = it.Authors.Count == 0 ? "" : $"{it.Authors[0].Email}",
            Title = it.Title?.Text,
            LastUpdatedTime = it.LastUpdatedTime.DateTime,
            Summary = it.Summary?.Text,
            ContentLink = it.Links.Count == 0 ? "" : $"{it.Links[0].Uri}",
            PublishDate = it.PublishDate.DateTime
        }).ToList();
        return itemList;
    }
}