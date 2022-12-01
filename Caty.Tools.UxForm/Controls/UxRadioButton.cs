using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls;

[DefaultEvent("CheckedChangeEvent")]
public partial class UxRadioButton : UserControl
{
    [Description("选中改变事件"), Category("自定义")]
    public event EventHandler CheckedChangeEvent;

    private Font _font = new("微软雅黑", 12);

    [Description("字体"), Category("自定义")]
    public new Font Font
    {
        get => _font;
        set
        {
            _font = value;
            label1.Font = value;
        }
    }

    private Color _foreColor = Color.FromArgb(62, 62, 62);

    [Description("字体颜色"), Category("自定义")]
    public new Color ForeColor
    {
        get => _foreColor;
        set
        {
            _foreColor = value;
            label1.ForeColor = value;
        }
    }

    private string _text = "单选按钮";

    [Description("文本"), Category("自定义")]
    public string TextValue
    {
        get => _text;
        set
        {
            _text = value;
            label1.Text = value;
        }
    }

    private bool _checked;

    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked == value) return;
            _checked = value;
            if (base.Enabled)
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.radioButton1 : Properties.Resources.radioButton0;
            }
            else
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.radioButton10 : Properties.Resources.radioButton00;
            }

            setCheck(value);
            if (CheckedChangeEvent != null)
            {
                CheckedChangeEvent(this, null);
            }
        }
    }

    [Description("分组名称"), Category("自定义")] public string GroupName { get; set; }

    public new bool Enabled
    {
        get => base.Enabled;
        set
        {
            base.Enabled = value;
            if (value)
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.radioButton1 : Properties.Resources.radioButton0;
            }
            else
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.radioButton10 : Properties.Resources.radioButton00;
            }
        }
    }

    public UxRadioButton()
    {
        InitializeComponent();
    }

    private void setCheck(bool value)
    {
        if (!value) return;
        if (Parent == null) return;
        foreach (Control control in Parent.Controls)
        {
            if (control is not UxRadioButton radio || control == this) continue;
            if (GroupName != radio.GroupName || !radio.Checked) continue;
            radio.Checked = true;
            return;
        }
    }

    private void Radio_MouseDown(object sender, MouseEventArgs e)
    {
        Checked = true;
    }

    private void UxRadioButton_Load(object sender, EventArgs e)
    {
        if (Parent == null || !_checked) return;
        foreach (Control control in Parent.Controls)
        {
            if (control is not UxRadioButton radio || control == this) continue;
            if (GroupName != radio.GroupName || !radio.Checked) continue;
            Checked = false;
            return;
        }
    }
}