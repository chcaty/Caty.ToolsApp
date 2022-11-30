using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls;

public sealed partial class UxButtonImage : UxButtonBase
{
    private string _btnText = "自定义按钮";

    /// <summary>
    /// 按钮文字
    /// </summary>
    [Description("按钮文字"), Category("自定义")]
    public new string BtnText
    {
        get => _btnText;
        set
        {
            _btnText = value;
            lbl.Text = @"    " + value;
            lbl.Refresh();
        }
    }

    /// <summary>
    /// 图片
    /// </summary>
    [Description("图片"), Category("自定义")]
    public Image Image
    {
        get => imageList1.Images[0];
        set
        {
            imageList1.Images.Clear();
            imageList1.Images.Add(value);
            lbl.ImageIndex = 0;

        }

    }

    public UxButtonImage()
    {
        InitializeComponent();
        BtnForeColor = ForeColor = Color.FromArgb(102, 102, 102);
        BtnFont = new Font("微软雅黑", 17);
        BtnText = "    自定义按钮";
    }
}