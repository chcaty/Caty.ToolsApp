using System.ComponentModel;
using System.Drawing.Design;
using Caty.Tools.UxForm.UIEditor;

namespace Caty.Tools.UxForm.Controls;

public partial class UxButtonImage : UxButtonBase
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
    public virtual Image Image
    {
        get => imageList1.Images[0];
        set
        {
            imageList1.Images.Clear();
            imageList1.Images.Add(value);
            lbl.ImageIndex = 0;

        }
    }

    /// <summary>
    /// The image font icons
    /// </summary>
    private object imageFontIcons;
    /// <summary>
    /// Gets or sets the image font icons.
    /// </summary>
    /// <value>The image font icons.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
    [Editor(typeof(ImagePropertyEditor), typeof(UITypeEditor))]
    public object ImageFontIcons
    {
        get => imageFontIcons;
        set
        {
            if (value != null && value is not System.Drawing.Image) return;
            imageFontIcons = value;
            if (value != null)
            {
                Image = (Image)value;
            }
        }
    }

    /// <summary>
    /// 图片位置
    /// </summary>
    /// <value>The image align.</value>
    [Description("图片位置"), Category("自定义")]
    public virtual ContentAlignment ImageAlign
    {
        get => lbl.ImageAlign;
        set => lbl.ImageAlign = value;
    }
    /// <summary>
    /// 文字位置
    /// </summary>
    /// <value>The text align.</value>
    [Description("文字位置"), Category("自定义")]
    public virtual ContentAlignment TextAlign
    {
        get => lbl.TextAlign;
        set => lbl.TextAlign = value;
    }


    public UxButtonImage()
    {
        InitializeComponent();
        BtnForeColor = ForeColor = Color.FromArgb(102, 102, 102);
        BtnFont = new Font("微软雅黑", 17);
        BtnText = "    自定义按钮";
    }
}