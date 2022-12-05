using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Forms
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design",
        typeof(IDesigner))]
    public partial class FrmBase : Form
    {
        [Description("定义的热键列表"), Category("自定义")]
        public Dictionary<int, string> HotKeys { get; set; }

        public delegate bool HotKeyEventHandler(string strHotKey);

        /// <summary>
        /// 热键事件
        /// </summary>
        [Description("热键事件"), Category("自定义")]
        public event HotKeyEventHandler HotKeyDown;

        #region 字段属性

        /// <summary>
        /// 是否显示蒙版窗体
        /// </summary>
        [Description("是否显示蒙版窗体")]
        public bool IsShowMaskDialog { get; set; }

        /// <summary>
        /// 边框宽度
        /// </summary>
        [Description("边框宽度")]
        public int BorderStyleSize { get; set; }

        /// <summary>
        /// 边框颜色
        /// </summary>
        [Description("边框颜色")]
        public Color BorderStyleColor { get; set; }

        /// <summary>
        /// 边框样式
        /// </summary>
        [Description("边框样式")]
        public ButtonBorderStyle BorderStyleType { get; set; }

        /// <summary>
        /// 边框圆角
        /// </summary>
        [Description("边框圆角")]
        public int RegionRadius { get; set; } = 10;

        /// <summary>
        /// 是否显示自定义绘制内容
        /// </summary>
        [Description("是否显示自定义绘制内容")]
        public bool IsShowRegion { get; set; }

        /// <summary>
        /// 是否显示重绘边框
        /// </summary>
        [Description("是否显示重绘边框")]
        public bool Redraw { get; set; }

        /// <summary>
        /// 是否全屏
        /// </summary>
        [Description("是否全屏")]
        public bool IsFullSize { get; set; } = true;

        /// <summary>
        /// 失去焦点自动关闭
        /// </summary>
        [Description("失去焦点自动关闭")]
        public bool IsLoseFocusClose { get; set; }

        #endregion

        private static bool IsDesignMode
        {
            get
            {
                var returnFlag = false;
                if (LicenseManager.UsageMode == LicenseUsageMode.Designtime)
                    returnFlag = true;
                else if (Process.GetCurrentProcess().ProcessName == "devenv")
                    returnFlag = true;
                return returnFlag;
            }
        }

        public FrmBase()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            KeyDown += FrmBase_KeyDown;
            FormClosing += FrmBase_FormClosing;
        }

        private void FrmBase_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (IsLoseFocusClose)
            {
                MouseHook.OnMouseActivity -= Hook_OnMouseActivity;
            }
        }


        private void FrmBase_Load(object sender, EventArgs e)
        {
            if (!IsDesignMode)
            {
                if (IsFullSize)
                    SetFullSize();
            }

            if (IsLoseFocusClose)
            {
                MouseHook.OnMouseActivity += Hook_OnMouseActivity;
            }
        }


        private void Hook_OnMouseActivity(object sender, MouseEventArgs e)
        {
            try
            {
                if (!IsLoseFocusClose || e.Clicks <= 0) return;
                if (e.Button is not (MouseButtons.Left or MouseButtons.Right)) return;
                if (IsDisposed) return;
                if (!ClientRectangle.Contains(PointToClient(e.Location)))
                {
                    Close();
                }
            }
            catch
            {
            }
        }


        /// <summary>
        /// 全屏
        /// </summary>
        public void SetFullSize()
        {
            FormBorderStyle = FormBorderStyle.None;

            WindowState = FormWindowState.Maximized;
        }

        protected virtual void DoEsc()
        {
            Close();
        }

        protected virtual void DoEnter()
        {
        }

        /// <summary>
        /// 设置重绘区域
        /// </summary>
        public void SetWindowRegion()
        {
            var rect = new Rectangle(-1, -1, Width + 1, Height);
            var path = GetRoundedRectPath(rect, RegionRadius);
            Region = new Region(path);
        }

        /// <summary>
        /// 获取重绘区域
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            var rect2 = new Rectangle(rect.Location, new Size(radius, radius));
            var graphicsPath = new GraphicsPath();
            graphicsPath.AddArc(rect2, 180f, 90f);
            rect2.X = rect.Right - radius;
            graphicsPath.AddArc(rect2, 270f, 90f);
            rect2.Y = rect.Bottom - radius;
            rect2.Width += 1;
            rect2.Height += 1;
            graphicsPath.AddArc(rect2, 360f, 90f);
            rect2.X = rect.Left;
            graphicsPath.AddArc(rect2, 90f, 90f);
            graphicsPath.CloseFigure();
            return graphicsPath;
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            try
            {
                if (!IsShowMaskDialog || owner == null) return base.ShowDialog(owner);
                var frmOwner = (Control)owner;
                var frmTransparent = new FrmTransparent
                {
                    Width = frmOwner.Width,
                    Height = frmOwner.Height
                };
                var location = frmOwner.PointToScreen(new Point(0, 0));
                frmTransparent.Location = location;
                frmTransparent.FrmChild = this;
                frmTransparent.IsShowMaskDialog = false;
                return frmTransparent.ShowDialog(owner);

            }
            catch (NullReferenceException)
            {
                return DialogResult.None;
            }
        }

        public new DialogResult ShowDialog()
        {
            return base.ShowDialog();
        }

        /// <summary>
        /// 关闭时发生
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            (Owner as FrmTransparent)?.Close();
        }

        /// <summary>
        /// 快捷键
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int num = 256;
            const int num2 = 260;
            bool result;
            if (msg.Msg == num | msg.Msg == num2)
            {
                if (keyData == (Keys)262259)
                {
                    result = true;
                    return result;
                }

                if (keyData != Keys.Enter)
                {
                    if (keyData == Keys.Escape)
                    {
                        DoEsc();
                    }
                }
                else
                {
                    DoEnter();
                }
            }

            result = false;
            return result ? result : base.ProcessCmdKey(ref msg, keyData);
        }

        protected void FrmBase_KeyDown(object sender, KeyEventArgs e)
        {
            if (HotKeyDown == null || HotKeys == null) return;
            var blnCtrl = false;
            var blnAlt = false;
            var blnShift = false;
            if (e.Control)
                blnCtrl = true;
            if (e.Alt)
                blnAlt = true;
            if (e.Shift)
                blnShift = true;
            if (!HotKeys.ContainsKey(e.KeyValue)) return;
            var strKey = string.Empty;
            if (blnCtrl)
            {
                strKey += "Ctrl+";
            }

            if (blnAlt)
            {
                strKey += "Alt+";
            }

            if (blnShift)
            {
                strKey += "Shift+";
            }

            strKey += HotKeys[e.KeyValue];

            if (!HotKeyDown(strKey)) return;
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// 重绘事件
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsShowRegion)
            {
                SetWindowRegion();
            }

            base.OnPaint(e);
            if (Redraw)
            {
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, BorderStyleColor, BorderStyleSize,
                    BorderStyleType, BorderStyleColor, BorderStyleSize, BorderStyleType,
                    BorderStyleColor, BorderStyleSize, BorderStyleType, BorderStyleColor,
                    BorderStyleSize, BorderStyleType);
            }
        }
    }
}
