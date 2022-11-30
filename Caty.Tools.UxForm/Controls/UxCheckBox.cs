using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls;

[DefaultEvent("CheckedChangeEvent")]
public partial class UxCheckBox : UserControl
{
    [Description("选中改变事件"), Category("自定义")]
    public event EventHandler CheckedChangeEvent;

    [Description("字体"), Category("自定义")]
    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font = value;
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
            label1.ForeColor = value;
            _foreColor = value;
        }
    }

    private string _text = "复选框";

    [Description("文本"), Category("自定义")]
    public string TextValue
    {
        get => _text;
        set
        {
            label1.Text = value;
            _text = value;
        }
    }

    private bool _checked;

    [Description("是否选中"), Category("自定义")]
    public bool Checked
    {
        get => _checked;
        set
        {
            if (_checked == value) return;
            _checked = value;
            if (base.Enabled)
            {
                panel1.BackgroundImage = _checked ? Properties.Resources.checkbox1 : Properties.Resources.checkbox0;
            }
            else
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.checkbox10 : Properties.Resources.checkbox00;
            }

            CheckedChangeEvent(this, null);
        }
    }

    public new bool Enabled
    {
        get => base.Enabled;
        set
        {
            base.Enabled = value;
            if (value)
            {
                panel1.BackgroundImage = _checked ? Properties.Resources.checkbox1 : Properties.Resources.checkbox0;
            }
            else
            {
                panel1.BackgroundImage =
                    _checked ? Properties.Resources.checkbox10 : Properties.Resources.checkbox00;
            }
        }
    }

    public UxCheckBox()
    {
        InitializeComponent();
    }

    private void CheckBox_MouseDown(object sender, MouseEventArgs e)
    {
        Checked = !Checked;
    }
}