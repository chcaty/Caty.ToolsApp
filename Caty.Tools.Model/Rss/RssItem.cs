using Caty.Tools.Share.Repository;
using System.Text.Json.Serialization;

namespace Caty.Tools.Model.Rss;

public class RssItem : Entity<int>
{
    /// <summary>
    /// 消息作者
    /// </summary>
    public string Author { get; set; } = string.Empty;

    /// <summary>
    /// 作者主页地址
    /// </summary>
    public string AuthorLink { get; set; } = string.Empty;

    /// <summary>
    /// 作者邮箱
    /// </summary>
    public string AuthorEmail { get; set; } = string.Empty;

    /// <summary>
    /// 消息标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 消息摘要
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// 最后更新时间
    /// </summary>
    public DateTime LastUpdatedTime { get; set; }

    /// <summary>
    /// 消息Id
    /// </summary>
    public string ItemId { get; set; } = string.Empty;

    /// <summary>
    /// 消息地址
    /// </summary>
    public string ContentLink { get; set; } = string.Empty;

    public DateTime PublishDate { get; set; }

    /// <summary>
    /// 聚合Id
    /// </summary>
    public int FeedId { get; set; }

    /// <summary>
    /// 所属聚合
    /// </summary>
    [JsonIgnore]
    public virtual RssFeed Feed { get; set; }
}