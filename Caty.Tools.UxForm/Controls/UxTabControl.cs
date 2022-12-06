using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxTabControl : TabControl
    {
        public UxTabControl()
        {
            SetStyles();
            Multiline = true;
            ItemSize = ItemSize with { Height = 50 };
            InitializeComponent();
        }

        private void SetStyles()
        {
            SetStyle(ControlStyles.UserPaint|ControlStyles.DoubleBuffer|ControlStyles.OptimizedDoubleBuffer|ControlStyles.AllPaintingInWmPaint|ControlStyles.ResizeRedraw|ControlStyles.SupportsTransparentBackColor, true);
            UpdateStyles();
        }

        private Color _backColor = Color.White;

        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DefaultValue(typeof(Color), "White")]
        public override Color BackColor
        {
            get { return _backColor; }
            set
            {
                _backColor = value;
                Invalidate(true);
            }
        }

        private Color _borderColor = Color.FromArgb(232, 232, 232);
        [DefaultValue(typeof(Color), "232, 232, 232")]
        [Description("TabContorl边框色")]
        public Color BorderColor
        {
            get { return _borderColor; }
            set
            {
                _borderColor = value;
                Invalidate(true);
            }
        }

        private Color _headSelectedBackColor = Color.FromArgb(255, 85, 51);

        [DefaultValue(typeof(Color), "255, 85, 51")]
        [Description("TabPage头部选中后的背景颜色")]
        public Color HeadSelectedBackColor
        {
            get { return _headSelectedBackColor; }
            set { _headSelectedBackColor = value; }
        }

        private Color _headSelectedBorderColor = Color.FromArgb(232, 232, 232);

        [DefaultValue(typeof(Color), "232, 232, 232")]
        [Description("TabPage头部选中后的边框颜色")]
        public Color HeadSelectedBorderColor
        {
            get { return _headSelectedBorderColor; }
            set { _headSelectedBorderColor = value; }
        }

        private Color _headerBackColor = Color.White;

        [DefaultValue(typeof(Color), "White")]
        [Description("TabPage头部默认背景颜色")]
        public Color HeaderBackColor
        {
            get { return _headerBackColor; }
            set { _headerBackColor = value; }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (DesignMode == true)
            {
                LinearGradientBrush backBrush = new LinearGradientBrush(Bounds, SystemColors.ControlLightLight,
                    SystemColors.ControlLight, LinearGradientMode.Vertical);
                pevent.Graphics.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
            else
            {
                this.PaintTransparentBackground(pevent.Graphics, this.ClientRectangle);
            }
        }

        protected void PaintTransparentBackground(Graphics g, Rectangle rect)
        {
            if (Parent != null)
            {
                rect.Offset(Location);
                PaintEventArgs e = new PaintEventArgs(g, rect);
                GraphicsState state = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    g.TranslateTransform((float)-this.Location.X, (float)-this.Location.Y);
                    InvokePaintBackground(this.Parent, e);
                    InvokePaint(Parent, e);
                }
                finally
                {
                    g.Restore(state);
                    rect.Offset(-this.Location.X, -this.Location.Y);
                    using (SolidBrush brush = new SolidBrush(_backColor))
                    {
                        rect.Inflate(1,1);
                        g.FillRectangle(brush, rect);
                    }
                }
            }
            else
            {
                System.Drawing.Drawing2D.LinearGradientBrush backBrush = new System.Drawing.Drawing2D.LinearGradientBrush(this.Bounds, SystemColors.ControlLightLight, SystemColors.ControlLight, System.Drawing.Drawing2D.LinearGradientMode.Vertical);
                g.FillRectangle(backBrush, this.Bounds);
                backBrush.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.PaintTransparentBackground(e.Graphics,this.ClientRectangle);
            this.PaintAllTheTabs(e);
            this.PaintTheTabPageBorder(e);
            this.PaintTheSelectedTab(e);
        }

        private void PaintAllTheTabs(System.Windows.Forms.PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                for (int i = 0; i < this.TabCount; i++)
                {
                    this.PaintTab(e, i);
                }
            }
        }

        private void PaintTab(PaintEventArgs e, int index)
        {
            GraphicsPath path = this.GetTabPath(index);
            this.PaintTabBackground(e.Graphics, index, path);
            this.PaintTabBorder(e.Graphics, index, path);
            this.PaintTabText(e.Graphics, index);
            this.PaintTabImage(e.Graphics, index);
        }

        private void PaintTabBackground(Graphics g, int index, GraphicsPath path)
        {
            Rectangle rectangle = this.GetTabRect(index);
            Brush buttonBrush = new LinearGradientBrush(rectangle, _headerBackColor,_headerBackColor, LinearGradientMode.Vertical);
            g.FillPath(buttonBrush, path);
        }

        private void PaintTabBorder(Graphics g, int index, GraphicsPath path)
        {
            Pen borderPen = new Pen(_borderColor);
            if (index == this.SelectedIndex)
            {
                borderPen = new Pen(_headSelectedBorderColor);
            }

            g.DrawPath(borderPen, path);
            borderPen.Dispose();
        }

        private void PaintTabImage(Graphics g, int index)
        {
            Image tabImage = null;
            if (this.TabPages[index].ImageIndex > -1 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageIndex];
            }
            else if (this.TabPages[index].ImageKey.Trim().Length > 0 && this.ImageList != null)
            {
                tabImage = this.ImageList.Images[this.TabPages[index].ImageKey];
            }

            if (tabImage != null)
            {
                Rectangle rect  = this.GetTabRect(index);
                g.DrawImage(tabImage, rect.Right - rect.Height - 4, 4, rect.Height-2, rect.Height -2);
            }
        }

        private void PaintTabText(Graphics g, int index)
        {
            string tabText = this.TabPages[index].Text;
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Near;
            format.LineAlignment = StringAlignment.Center;
            format.Trimming = StringTrimming.EllipsisCharacter;

            Brush foreBrush = null;

            if (this.TabPages[index].Enabled == false)
            {
                foreBrush = SystemBrushes.ControlDark;
            }
            else
            {
                foreBrush = SystemBrushes.ControlText;
            }

            Font tabFont = this.Font;
            if (index == SelectedIndex)
            {
                if (TabPages[index].Enabled != false)
                {
                    foreBrush = new SolidBrush(_headSelectedBackColor);
                }
            }
            Rectangle rectangle = this.GetTabRect(index);

            var txtSize = ControlHelper.GetStringWidth(tabText, g, tabFont);
            Rectangle rect = new Rectangle(rectangle.Left + (rectangle.Width - txtSize) / 2 - 1, rectangle.Top,
                rectangle.Width, rectangle.Height);
            g.DrawString(tabText, tabFont, foreBrush, rect, format);
        }

        private void PaintTheTabPageBorder(PaintEventArgs e)
        {
            if (this.TabCount > 0)
            {
                Rectangle borderRectangle = this.TabPages[0].Bounds;
                Rectangle rect = new Rectangle(borderRectangle.X - 2, borderRectangle.Y - 1, borderRectangle.Width + 5,
                    borderRectangle.Height + 2);
                ControlPaint.DrawBorder(e.Graphics, rect, this.BackColor, ButtonBorderStyle.Solid);
            }
        }

        private void PaintTheSelectedTab(PaintEventArgs e)
        {
            if (this.SelectedIndex == -1)
            {
                return;
            }

            Rectangle selectedRectangle;
            int selectedRectRight = 0;
            selectedRectangle = this.GetTabRect(this.SelectedIndex);
            selectedRectRight = selectedRectangle.Right;
            e.Graphics.DrawLine(new Pen(_headSelectedBackColor), selectedRectangle.Left, selectedRectangle.Bottom + 1,
                selectedRectRight, selectedRectangle.Bottom + 1);
        }

        private GraphicsPath GetTabPath(int index)
        {
            GraphicsPath path = new GraphicsPath();
            path.Reset();

            Rectangle rectangle = this.GetTabRect(index);
            path.AddLine(rectangle.Left, rectangle.Top, rectangle.Left, rectangle.Bottom + 1);
            path.AddLine(rectangle.Left, rectangle.Top, rectangle.Right, rectangle.Top);
            path.AddLine(rectangle.Right, rectangle.Top, rectangle.Right, rectangle.Bottom + 1);
            path.AddLine(rectangle.Right, rectangle.Bottom+1, rectangle.Left, rectangle.Bottom + 1);
            return path;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        private const int WM_SETFONT = 0x30;
        private const int WM_FONTCHANGE = 0x1d;

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            OnFontChanged(EventArgs.Empty);
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            IntPtr hFont = Font.ToHfont();
            SendMessage(this.Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(this.Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            this.UpdateStyles();
        }
    }
}
