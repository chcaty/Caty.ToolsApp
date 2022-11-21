namespace Caty.ToolsApp.Model.Rss;

public class RssSource: BaseEntity
{
    /// <summary>
    /// Rss名称
    /// </summary>
    public string RssName { get; set; }

    /// <summary>
    /// Rss链接
    /// </summary>
    public string RssUrl { get; set; }

    /// <summary>
    /// Rss描述
    /// </summary>
    public string RssDescription { get; set; }

    /// <summary>
    /// Rss分类
    /// </summary>
    public string Category { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; }
}