using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls;

[DefaultEvent("BtnClick")]
public partial class UxButtonBase : UxControlBase
{
    /// <summary>
    /// 是否显示角标
    /// </summary>
    [Description("是否显示角标"), Category("自定义")]
    public bool IsShowTips
    {
        get => lblTips.Visible;
        set => lblTips.Visible = value;
    }

    /// <summary>
    /// 角标文字
    /// </summary>
    [Description("角标文字"), Category("自定义")]
    public string TipsText
    {
        get => lblTips.Text;
        set => lblTips.Text = value;
    }

    private Color _btnBackColor = Color.White;
    [Description("按钮背景色"), Category("自定义")]
    public Color BtnBackColor
    {
        get => _btnBackColor;
        set
        {
            _btnBackColor = value;
            BackColor = value;

        }
    }

    private Color _btnForeColor = Color.Black;
    [Description("按钮字体颜色"), Category("自定义")]
    public Color BtnForeColor
    {
        get => _btnForeColor;
        set
        {
            _btnForeColor = value;
            lbl.ForeColor = value;

        }
    }

    private Font _btnFont = new("微软雅黑", 12F, FontStyle.Regular, GraphicsUnit.Point, 134); 
    [Description("按钮字体"), Category("自定义")]
    public Font BtnFont
    {
        get => _btnFont;
        set
        {
            _btnFont = value;
            lbl.Font = value;
        }
    }

    /// <summary>
    /// 按钮点击事件
    /// </summary>
    [Description("按钮点击事件"), Category("自定义")]
    public event EventHandler BtnClick;

    private string _btnText;
    [Description("按钮文字"), Category("自定义")]
    public string BtnText
    {
        get => _btnText;
        set
        {
            _btnText = value;
            lbl.Text = value;

        }
    }

    public UxButtonBase()
    {
        InitializeComponent();
        TabStop = false;
    }

    private void lbl_MouseDown(object sender, MouseEventArgs e)
    {
        BtnClick(this, e);
    }
}