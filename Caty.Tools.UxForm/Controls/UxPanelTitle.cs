using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls;

public partial class UxPanelTitle : UxControlBase
{
    [Description("边框颜色"), Category("自定义")]
    public Color BorderColor
    {
        get => RectColor;
        set
        {
            RectColor = value;
            lblTitle.BackColor = value;
        }
    }
    
    [Description("面板标题"), Category("自定义")]
    public string Title
    {
        get => lblTitle.Text;
        set => lblTitle.Text = value;
    }
    public UxPanelTitle()
    {
        InitializeComponent();
    }
}