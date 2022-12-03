namespace Caty.Tools.UxForm.Controls.Menu;

internal interface IMenuItem
{
    event EventHandler SelectedItem;
    MenuItemEntity DataSource { get; set; }

    /// <summary>
    /// 设置样式
    /// </summary>
    /// <param name="styles">key:属性名称,value:属性值</param>
    void SetStyle(Dictionary<string, object> styles);

    /// <summary>
    /// 设置选中样式
    /// </summary>
    /// <param name="isSelected">是否选中</param>
    void SetSelectedStyle(bool isSelected);
}