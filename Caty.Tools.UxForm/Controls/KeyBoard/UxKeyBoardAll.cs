using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.KeyBoard;

[DefaultEvent("KeyDown")]
public partial class UxKeyBoardAll : UserControl
{
    private KeyBoardCharType _charType = KeyBoardCharType.Char;

    [Description("默认显示样式"), Category("自定义")]
    public KeyBoardCharType CharType
    {
        get => _charType;
        set
        {
            _charType = value;
            if (value == KeyBoardCharType.Char)
            {
                if (label37.Text.ToLower() == "abc.")
                {
                    CharOrNum();
                }
            }
            else
            {
                if (label37.Text.ToLower() == "?123")
                {
                    CharOrNum();
                }
            }
        }
    }

    [Description("按键点击事件"), Category("自定义")]
    public event EventHandler? KeyClick;

    [Description("回车点击事件"), Category("自定义")]
    public event EventHandler? EnterClick;

    [Description("删除点击事件"), Category("自定义")]
    public event EventHandler? BackspaceClick;

    [Description("收起点击事件"), Category("自定义")]
    public event EventHandler? RetractClick;

    public UxKeyBoardAll()
    {
        InitializeComponent();
    }

    private void KeyDown_MouseDown(object sender, MouseEventArgs e)
    {
        if (sender is not Label lbl)
        {
            return;
        }
        if (string.IsNullOrEmpty(lbl.Text))
        {
            return;
        }

        switch (lbl.Text)
        {
            case "大写":
                ChangeCase(true);
                lbl.Text = @"小写";
                break;
            case "小写":
                ChangeCase(false);
                lbl.Text = @"大写";
                break;
            case "?123":
            case "abc.":
                CharOrNum();
                break;
            case "空格":
                SendKeys.Send(" ");
                break;
            case "删除":
            {
                SendKeys.Send("{BACKSPACE}");
                BackspaceClick?.Invoke(sender, e);
                break;
            }
            case "回车":
            {
                SendKeys.Send("{ENTER}");
                EnterClick?.Invoke(sender, e);
                break;
            }
            case "收起":
            {
                RetractClick?.Invoke(sender, e);
                break;
            }
            default:
                SendKeys.Send(lbl.Text);
                break;
        }

        KeyClick?.Invoke(sender, e);
    }

    private void ChangeCase(bool bln)
    {
        foreach (Control item in tableLayoutPanel2.Controls)
        {
            if (item is not Panel) continue;
            foreach (Control pc in item.Controls)
            {
                if (pc is not Label) continue;
                if (pc.Text == @"abc.")
                    break;
                pc.Text = bln ? pc.Text.ToUpper() : pc.Text.ToLower();
                break;
            }
        }
    }

    private void CharOrNum()
    {
        foreach (Control item in tableLayoutPanel2.Controls)
        {
            if (item is not Panel) continue;
            foreach (Control pc in item.Controls)
            {
                if (pc is not Label) continue;
                var strTag = pc.Text;
                pc.Text = pc.Tag.ToString();
                pc.Tag = strTag;
                break;
            }
        }
    }
}