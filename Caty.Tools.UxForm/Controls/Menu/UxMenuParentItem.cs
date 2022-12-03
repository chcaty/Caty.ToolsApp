using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.Menu;

[ToolboxItem(false)]
public partial class UxMenuParentItem : UserControl,IMenuItem
{

    public event EventHandler SelectedItem;
    private MenuItemEntity _mDataSource;

    public MenuItemEntity DataSource
    {
        get => _mDataSource;
        set
        {
            _mDataSource = value;
            if (value != null)
            {
                lblTitle.Text = value.Text;
            }
        }
    }

    public UxMenuParentItem()
    {
        InitializeComponent();
        lblTitle.MouseDown += lblTitle_MouseDown;
    }
    public void SetStyle(Dictionary<string, object> styles)
    {
        var type = this.GetType();
        foreach (var style in styles)
        {
            var pro = type.GetProperty(style.Key);
            if (pro == null || !pro.CanWrite) continue;
            try
            {
                pro.SetValue(this, style.Value, null);
            }
            catch (Exception ex)
            {
                throw new Exception("菜单元素设置样式异常", ex);
            }
        }
    }

    public void SetSelectedStyle(bool isSelected)
    {
        lblTitle.Image = isSelected ? Properties.Resources.sanjiao1 : Properties.Resources.sanjiao2;
    }

    private void lblTitle_MouseDown(object sender, MouseEventArgs e)
    {
        if (SelectedItem != null)
        {
            SelectedItem(this, e);
        }
    }
}