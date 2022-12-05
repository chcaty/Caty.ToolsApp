namespace Caty.Tools.UxForm.Controls.Menu;

public partial class UxMenuChildrenItem : UserControl, IMenuItem
{

    public event EventHandler SelectedItem;

    private MenuItemEntity _dataSource;

    public MenuItemEntity DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
            if (_dataSource != null)
            {
                lblTitle.Text = value.Text;
            }
        }
    }


    public UxMenuChildrenItem()
    {
        InitializeComponent();
        lblTitle.MouseDown += lblTitle_MouseDown;
    }

    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
        if (SelectedItem != null)
        {
            SelectedItem(this, null);
        }
    }

    public void SetStyle(Dictionary<string, object> styles)
    {
        var type = GetType();
        foreach (var style in styles)
        {
            var property = type.GetProperty(style.Key);
            if (property == null || !property.CanWrite) continue;
            try
            {
                property.SetValue(this, style.Value, null);
            }
            catch (Exception ex)
            {
                throw new Exception("菜单元素设置样式异常", ex);
            }
        }
    }

    public void SetSelectedStyle(bool isSelected)
    {

    }
}