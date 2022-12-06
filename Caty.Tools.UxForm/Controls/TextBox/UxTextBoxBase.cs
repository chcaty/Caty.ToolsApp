using System.ComponentModel;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.TextBox;

public partial class UxTextBoxBase : System.Windows.Forms.TextBox
{
    private bool _blnFocus;

    private string _promptText = string.Empty;

    private TextInputType _inputType = TextInputType.NotControl;

    private string _oldValue = string.Empty;

    /// <summary>
    /// 水印文字
    /// </summary>
    [Description("水印文字"), Category("自定义")]
    public string PromptText
    {
        get => _promptText;
        set
        {
            _promptText = value;
            OnPaint(null);
        }
    }

    [Description("水印字体"), Category("自定义")]
    public Font PromptFont { get; set; } = new Font("微软雅黑", 15f, FontStyle.Regular, GraphicsUnit.Pixel);

    [Description("水印颜色"), Category("自定义")] public Color PromptColor { get; set; } = Color.Gray;

    public Rectangle MyRectangle { get; set; }

    public string OldText { get; set; }

    [Description("获取或设置一个值，该值指示文本框中的文本输入类型。")]
    public TextInputType InputType
    {
        get => _inputType;
        set
        {
            _inputType = value;
            if (value != TextInputType.NotControl)
            {
                TextChanged -= TextBoxEx_TextChanged;
                TextChanged += TextBoxEx_TextChanged;
            }
            else
            {
                TextChanged -= TextBoxEx_TextChanged;
            }
        }
    }

    /// <summary>
    /// 获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。
    /// </summary>
    [Description("获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。")]
    public string RegexPattern { get; set; } = "";

    /// <summary>
    /// 当InputType为数字类型时，能输入的最大值
    /// </summary>
    [Description("当InputType为数字类型时，能输入的最大值。")]
    public decimal MaxValue { get; set; } = 1000000m;

    /// <summary>
    /// 当InputType为数字类型时，能输入的最小值
    /// </summary>
    [Description("当InputType为数字类型时，能输入的最小值。")]
    public decimal MinValue { get; set; } = -1000000m;

    /// <summary>
    /// 当InputType为数字类型时，能输入的最小值
    /// </summary>
    [Description("当InputType为数字类型时，小数位数。")]
    public int DecLength { get; set; } = 2;

    public UxTextBoxBase()
    {
        InitializeComponent();
        GotFocus += UxTextBox_GotFocus;
        MouseUp += UxTextBox_MouseUp;
        KeyPress += UxTextBox_KeyPress;
    }

    private static void UxTextBox_KeyPress(object sender, KeyPressEventArgs e)
    {
        //以下代码  取消按下回车或esc的“叮”声
        if (e.KeyChar == Convert.ToChar(13) || e.KeyChar == Convert.ToChar(27))
        {
            e.Handled = true;
        }
    }

    private void UxTextBox_MouseUp(object sender, MouseEventArgs e)
    {
        if (!_blnFocus) return;
        SelectAll();
        _blnFocus = false;
    }

    private void UxTextBox_GotFocus(object sender, EventArgs e)
    {
        _blnFocus = true;
        SelectAll();
    }

    private void TextBoxEx_TextChanged(object sender, EventArgs e)
    {
        if (Text == "")
        {
            _oldValue = Text;
        }
        else if (_oldValue != Text)
        {
            if (!ControlHelper.CheckInputType(Text, _inputType, MaxValue, MinValue, DecLength, RegexPattern))
            {
                var num = SelectionStart;
                if (_oldValue.Length < Text.Length)
                {
                    num--;
                }
                else
                {
                    num++;
                }

                TextChanged -= TextBoxEx_TextChanged;
                Text = _oldValue;
                TextChanged += TextBoxEx_TextChanged;
                if (num < 0)
                {
                    num = 0;
                }

                SelectionStart = num;
            }
            else
            {
                _oldValue = Text;
            }
        }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        if (!string.IsNullOrEmpty(Text) || string.IsNullOrEmpty(_promptText)) return;
        if (e != null) return;
        using var graphics = Graphics.FromHwnd(Handle);
        if (Text.Length != 0 || string.IsNullOrEmpty(PromptText)) return;
        var textFormatFlags = TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter;
        if (RightToLeft == RightToLeft.Yes)
        {
            textFormatFlags |= (TextFormatFlags.Right | TextFormatFlags.RightToLeft);
        }

        TextRenderer.DrawText(graphics, PromptText, PromptFont, ClientRectangle, PromptColor, textFormatFlags);
    }

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg is 15 or 7 or 8)
        {
            OnPaint(null);
        }
    }

    protected override void OnTextChanged(EventArgs e)
    {
        base.OnTextChanged(e);
        Invalidate();
    }
}