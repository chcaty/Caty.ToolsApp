namespace Caty.Tools.UxForm.Controls.List;

/// <summary>
/// 列表实体
/// </summary>
[Serializable]
public class ListEntity
{
    /// <summary>
    /// 编码，唯一值
    /// </summary>
    /// <value>The identifier.</value>
    public string ID { get; set; }
    /// <summary>
    /// 大标题
    /// </summary>
    /// <value>The title.</value>
    public string Title { get; set; }
    /// <summary>
    /// 右侧更多按钮
    /// </summary>
    /// <value><c>true</c> if [show more BTN]; otherwise, <c>false</c>.</value>
    public bool ShowMoreBtn { get; set; }
    /// <summary>
    /// 右侧副标题
    /// </summary>
    /// <value>The title2.</value>
    public string SubTitle { get; set; }
    /// <summary>
    /// 关联的数据源
    /// </summary>
    /// <value>The source.</value>
    public object Source { get; set; }
}