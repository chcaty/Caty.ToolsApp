using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.KeyBoard;

public partial class UxKeyBoardNum : UserControl
{
    /// <summary>
    /// 是否使用自定义的事件来接收按键，当为true时将不再向系统发送按键请求
    /// </summary>
    [Description("是否使用自定义的事件来接收按键，当为true时将不再向系统发送按键请求"), Category("自定义")]
    public bool UseCustomEvent { get; set; } = false;

    [Description("数字点击事件"), Category("自定义")]
    public event EventHandler? NumClick;
    [Description("删除点击事件"), Category("自定义")]
    public event EventHandler? BackspaceClick;
    [Description("回车点击事件"), Category("自定义")]
    public event EventHandler? EnterClick;
    public UxKeyBoardNum()
    {
        InitializeComponent();
    }
    private void Num_MouseDown(object sender, MouseEventArgs e)
    {
        NumClick?.Invoke(sender, e);
        if (UseCustomEvent)
            return;
        if (sender is not Label lbl) return;
        SendKeys.Send(lbl.Tag.ToString());
    }

    private void Backspace_MouseDown(object sender, MouseEventArgs e)
    {
        BackspaceClick?.Invoke(sender, e);
        if (UseCustomEvent)
            return;
        SendKeys.Send("{BACKSPACE}");
    }

    private void Enter_MouseDown(object sender, MouseEventArgs e)
    {
        EnterClick?.Invoke(sender, e);
        if (UseCustomEvent)
            return;
        SendKeys.Send("{ENTER}");
    }
}