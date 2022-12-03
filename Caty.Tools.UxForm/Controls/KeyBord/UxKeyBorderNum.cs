using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.KeyBord;

public partial class UxKeyBorderNum : UserControl
{
    /// <summary>
    /// 是否使用自定义的事件来接收按键，当为true时将不再向系统发送按键请求
    /// </summary>
    [Description("是否使用自定义的事件来接收按键，当为true时将不再向系统发送按键请求"), Category("自定义")]
    public bool UseCustomEvent { get; set; } = false;

    [Description("数字点击事件"), Category("自定义")]
    public event EventHandler NumClick;
    [Description("删除点击事件"), Category("自定义")]
    public event EventHandler BackspaceClick;
    [Description("回车点击事件"), Category("自定义")]
    public event EventHandler EnterClick;
    public UxKeyBorderNum()
    {
        InitializeComponent();
    }
    private void Num_MouseDown(object sender, MouseEventArgs e)
    {
        if (NumClick != null)
        {
            NumClick(sender, e);
        }
        if (UseCustomEvent)
            return;
        var lbl = sender as Label;
        SendKeys.Send(lbl.Tag.ToString());
    }

    private void Backspace_MouseDown(object sender, MouseEventArgs e)
    {
        if (BackspaceClick != null)
        {
            BackspaceClick(sender, e);
        }
        if (UseCustomEvent)
            return;
        SendKeys.Send("{BACKSPACE}");
    }

    private void Enter_MouseDown(object sender, MouseEventArgs e)
    {
        if (EnterClick != null)
        {
            EnterClick(sender, e);
        }
        if (UseCustomEvent)
            return;
        SendKeys.Send("{ENTER}");
    }
}