using Caty.Tools.Share.Repository;

namespace Caty.Tools.Model.Rss;

public class RssFeed : Entity<int>
{
    /// <summary>
    /// 网站标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 网站首页地址
    /// </summary>
    public string? Link { get; set; }

    /// <summary>
    /// 作者
    /// </summary>
    public string? Author { get; set; }

    /// <summary>
    /// 描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime LastUpdatedTime { get; set; }

    /// <summary>
    /// 生成器
    /// </summary>
    public string? Generator { get; set; }

    /// <summary>
    /// 聚合UUId
    /// </summary>
    public string? FeedCode { get; set; }

    /// <summary>
    /// Rss源Id
    /// </summary>
    public int SourceId { get; set; }

    /// <summary>
    /// 消息列表
    /// </summary>
    public virtual ICollection<RssItem> Items { get; set; }
}