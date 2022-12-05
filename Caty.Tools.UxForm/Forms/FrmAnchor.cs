using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Forms
{
    public partial class FrmAnchor : Form, IMessageFilter
    {
        private readonly bool _blnDown;
        public FrmAnchor(Control parentControl, Control childControl, Point? deviation = null)
        {
            InitializeComponent();
            Size = childControl.Size;
            HandleCreated += FrmDownBoard_HandleCreated;
            HandleDestroyed += FrmDownBoard_HandleDestroyed;
            Controls.Add(childControl);
            childControl.Dock = DockStyle.Fill;
            var p = parentControl.Parent.PointToScreen(parentControl.Location);
            int intX;
            int intY;
            if (p.Y + parentControl.Height + childControl.Height > Screen.PrimaryScreen.Bounds.Height)
            {
                intY = p.Y - childControl.Height - 1;
                _blnDown = false;
            }
            else
            {
                intY = p.Y + parentControl.Height + 1;
                _blnDown = true;
            }

            if (p.X + childControl.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                intX = Screen.PrimaryScreen.Bounds.Width - childControl.Width;

            }
            else
            {
                intX = p.X;
            }
            if (deviation.HasValue)
            {
                intX += deviation.Value.X;
                intY += deviation.Value.Y;
            }
            Location = new Point(intX, intY);
        }

        public FrmAnchor(Control parentControl, Size size, Point? deviation = null)
        {
            InitializeComponent();
            Size = size;
            HandleCreated += FrmDownBoard_HandleCreated;
            HandleDestroyed += FrmDownBoard_HandleDestroyed;

            var p = parentControl.Parent.PointToScreen(parentControl.Location);
            int intX;
            int intY;
            if (p.Y + parentControl.Height + size.Height > Screen.PrimaryScreen.Bounds.Height)
            {
                intY = p.Y - size.Height - 1;
                _blnDown = false;
            }
            else
            {
                intY = p.Y + parentControl.Height + 1;
                _blnDown = true;
            }

            if (p.X + size.Width > Screen.PrimaryScreen.Bounds.Width)
            {
                intX = Screen.PrimaryScreen.Bounds.Width - size.Width;

            }
            else
            {
                intX = p.X;
            }
            if (deviation.HasValue)
            {
                intX += deviation.Value.X;
                intY += deviation.Value.Y;
            }
            Location = new Point(intX, intY);
        }

        private void FrmDownBoard_HandleDestroyed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        private void FrmDownBoard_HandleCreated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        #region 无焦点窗体

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);
        private const int WM_ACTIVATE = 0x006;
        private const int WM_ACTIVATEAPP = 0x01C;
        private const int WM_NCACTIVATE = 0x086;
        private const int WA_INACTIVE = 0;
        private const int WM_MOUSEACTIVATE = 0x21;
        private const int MA_NOACTIVATE = 3;
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_MOUSEACTIVATE:
                    m.Result = new IntPtr(MA_NOACTIVATE);
                    return;
                case WM_NCACTIVATE:
                {
                    if (((int)m.WParam & 0xFFFF) != WA_INACTIVE)
                    {
                        SetActiveWindow(m.LParam != IntPtr.Zero ? m.LParam : IntPtr.Zero);
                    }

                    break;
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != 0x0201 || Visible == false)
                return false;
            var pt = PointToClient(MousePosition);
            Visible = ClientRectangle.Contains(pt);
            return false;
        }

        private void FrmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            timer1.Enabled = Visible;
            if (Visible)
            {
                ControlHelper.AnimateWindow(Handle, 100,
                    _blnDown ? ControlHelper.AW_VER_POSITIVE : ControlHelper.AW_VER_NEGATIVE);
            }
            else
            {
                if (_blnDown)
                    ControlHelper.AnimateWindow(Handle, 100, ControlHelper.AW_VER_NEGATIVE | ControlHelper.AW_HIDE);
                else
                {
                    ControlHelper.AnimateWindow(Handle, 100, ControlHelper.AW_VER_POSITIVE | ControlHelper.AW_HIDE);

                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (Owner == null) return;
            var frm = Owner;
            var _ptr = ControlHelper.GetForegroundWindow();
            if (_ptr != frm.Handle)
            {
                Hide();
            }
        }
    }
}
