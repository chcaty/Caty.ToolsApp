using System.Runtime.InteropServices;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Forms
{
    /// <summary>
    /// 停靠窗体
    /// </summary>
    /// <seealso cref="Form" />
    /// <seealso cref="IMessageFilter" />
    public partial class FrmAnchor : Form, IMessageFilter
    {

        /// <summary>
        /// The m parent control
        /// </summary>
        private readonly Control _parentControl;

        /// <summary>
        /// The m size
        /// </summary>
        private readonly Size _size;

        /// <summary>
        /// The m deviation
        /// </summary>
        private readonly Point? _deviation;

        /// <summary>
        /// The m is not focus
        /// </summary>
        private readonly bool _isNotFocus = true;

        #region 构造函数

        /// <summary>
        /// 功能描述:构造函数
        /// 作　　者:HZH
        /// 创建日期:2019-02-27 11:49:08
        /// 任务编号:POS
        /// </summary>
        /// <param name="parentControl">父控件</param>
        /// <param name="childControl">子控件</param>
        /// <param name="deviation">偏移</param>
        /// <param name="isNotFocus">是否无焦点窗体</param>
        public FrmAnchor(Control parentControl, Control childControl, Point? deviation = null, bool isNotFocus = true)
        {
            _isNotFocus = isNotFocus;
            _parentControl = parentControl;
            InitializeComponent();
            Size = childControl.Size;
            HandleCreated += FrmDownBoard_HandleCreated;
            HandleDestroyed += FrmDownBoard_HandleDestroyed;
            Controls.Add(childControl);
            childControl.Dock = DockStyle.Fill;

            _size = childControl.Size;
            _deviation = deviation;

            if (parentControl.FindForm() != null)
            {
                var frmP = parentControl.FindForm();
                if (!frmP.IsDisposed)
                {
                    frmP.LocationChanged += FrmP_LocationChanged;
                }
            }

            parentControl.LocationChanged += FrmP_LocationChanged;
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="FrmAnchor" /> class.
        /// </summary>
        /// <param name="parentControl">The parent control.</param>
        /// <param name="size">The size.</param>
        /// <param name="deviation">The deviation.</param>
        /// <param name="isNotFocus">if set to <c>true</c> [is not focus].</param>
        public FrmAnchor(Control parentControl, Size size, Point? deviation = null, bool isNotFocus = true)
        {
            _isNotFocus = isNotFocus;
            _parentControl = parentControl;
            InitializeComponent();
            Size = size;
            HandleCreated += FrmDownBoard_HandleCreated;
            HandleDestroyed += FrmDownBoard_HandleDestroyed;

            _size = size;
            _deviation = deviation;
        }

        /// <summary>
        /// Handles the LocationChanged event of the frmP control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmP_LocationChanged(object sender, EventArgs e)
        {
            Hide();
        }

        #endregion

        /// <summary>
        /// Handles the HandleDestroyed event of the FrmDownBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmDownBoard_HandleDestroyed(object sender, EventArgs e)
        {
            Application.RemoveMessageFilter(this);
        }

        /// <summary>
        /// Handles the HandleCreated event of the FrmDownBoard control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmDownBoard_HandleCreated(object sender, EventArgs e)
        {
            Application.AddMessageFilter(this);
        }

        #region 无焦点窗体

        /// <summary>
        /// Sets the active window.
        /// </summary>
        /// <param name="handle">The handle.</param>
        /// <returns>IntPtr.</returns>
        [DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);

        /// <summary>
        /// The wm activate
        /// </summary>
        private const int WM_ACTIVATE = 0x006;

        /// <summary>
        /// The wm activateapp
        /// </summary>
        private const int WM_ACTIVATEAPP = 0x01C;

        /// <summary>
        /// The wm ncactivate
        /// </summary>
        private const int WM_NCACTIVATE = 0x086;

        /// <summary>
        /// The wa inactive
        /// </summary>
        private const int WA_INACTIVE = 0;

        /// <summary>
        /// The wm mouseactivate
        /// </summary>
        private const int WM_MOUSEACTIVATE = 0x21;

        /// <summary>
        /// The ma noactivate
        /// </summary>
        private const int MA_NOACTIVATE = 3;

        /// <summary>
        /// WNDs the proc.
        /// </summary>
        /// <param name="m">要处理的 Windows <see cref="T:System.Windows.Forms.Message" />。</param>
        protected override void WndProc(ref Message m)
        {
            if (_isNotFocus)
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
            }

            base.WndProc(ref m);
        }

        #endregion

        /// <summary>
        /// 在调度消息之前将其筛选出来。
        /// </summary>
        /// <param name="m">要调度的消息。无法修改此消息。</param>
        /// <returns>如果筛选消息并禁止消息被调度，则为 true；如果允许消息继续到达下一个筛选器或控件，则为 false。</returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (m.Msg != 0x0201 || Visible == false)
                return false;
            var pt = PointToClient(MousePosition);
            Visible = ClientRectangle.Contains(pt);
            return false;
        }

        /// <summary>
        /// Handles the VisibleChanged event of the FrmAnchor control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void FrmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            timer1.Enabled = Visible;
            if (!Visible) return;
            var currentScreen = Screen.FromControl(_parentControl);
            var p = _parentControl.Parent.PointToScreen(_parentControl.Location);
            int intY;
            if (p.Y + _parentControl.Height + _size.Height > currentScreen.Bounds.Height)
            {
                intY = p.Y - _size.Height - 1;
            }
            else
            {
                intY = p.Y + _parentControl.Height + 1;
            }


            int intX;
            if (p.X + _size.Width > currentScreen.Bounds.Width)
            {
                intX = currentScreen.Bounds.Width - _size.Width;

            }
            else
            {
                intX = p.X;
            }

            if (_deviation.HasValue)
            {
                intX += _deviation.Value.X;
                intY += _deviation.Value.Y;
            }

            Location = ControlHelper.GetScreenLocation(currentScreen, intX, intY);
        }

        /// <summary>
        /// Handles the Tick event of the timer1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (Owner == null) return;
            var frm = Owner;
            var ptr = ControlHelper.GetForegroundWindow();
            if (ptr != frm.Handle && ptr != Handle)
            {
                Hide();
            }
        }
    }
}
