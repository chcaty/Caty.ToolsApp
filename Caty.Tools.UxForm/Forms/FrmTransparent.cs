using System.Reflection;
using System.Runtime.InteropServices;

namespace Caty.Tools.UxForm.Forms;

public partial class FrmTransparent : FrmBase
{
    private const int WM_ACTIVATE = 6;

    private const int WM_ACTIVATEAPP = 28;

    private const int WM_NCACTIVATE = 134;

    private const int WA_INACTIVE = 0;

    private const int WM_MOUSEACTIVATE = 33;

    private const int MA_NOACTIVATE = 3;

    public FrmBase? FrmChild { get; set; }

    public FrmTransparent()
    {
        InitializeComponent();

        SetStyle(ControlStyles.UserPaint, true);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);

        var method = GetType().GetMethod("SetStyle", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
        method?.Invoke(this, BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new object[]
        {
            ControlStyles.Selectable,
            false
        }, Application.CurrentCulture);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);
        ShowInTaskbar = false;
        ShowIcon = true;
    }
    [DllImport("user32.dll")]
    private static extern IntPtr SetActiveWindow(IntPtr handle);

    protected override void WndProc(ref Message m)
    {
        if (m.Msg == 33)
        {
            m.Result = new IntPtr(3);
        }
        else
        {
            switch (m.Msg)
            {
                case 134:
                {
                    if (((int)m.WParam & 65535) != 0)
                    {
                        SetActiveWindow(m.LParam != IntPtr.Zero ? m.LParam : IntPtr.Zero);
                    }

                    break;
                }
                case 2000:
                    break;
            }

            base.WndProc(ref m);
        }
    }

    private void FrmTransparent_Load(object sender, EventArgs e)
    {
        if (FrmChild == null) return;
        FrmChild.IsShowMaskDialog = false;
        var dia = FrmChild.ShowDialog(this);
        DialogResult = dia;
    }
}