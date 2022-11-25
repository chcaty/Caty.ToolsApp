using Caty.Tools.Share.Repository;

namespace Caty.Tools.Model.Rss;

public class RssSource : Entity<int>
{
    /// <summary>
    /// Rss名称
    /// </summary>
    public string RssName { get; set; } = string.Empty;

    /// <summary>
    /// Rss链接
    /// </summary>
    public string RssUrl { get; set; } = string.Empty;

    /// <summary>
    /// Rss描述
    /// </summary>
    public string RssDescription { get; set; } = string.Empty;

    /// <summary>
    /// Rss分类
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; }
}