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
            get => _backColor;
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
            get => _borderColor;
            set
            {
                _borderColor = value;
                Invalidate(true);
            }
        }

        [DefaultValue(typeof(Color), "255, 85, 51")]
        [Description("TabPage头部选中后的背景颜色")]
        public Color HeadSelectedBackColor { get; set; } = Color.FromArgb(255, 85, 51);

        [DefaultValue(typeof(Color), "232, 232, 232")]
        [Description("TabPage头部选中后的边框颜色")]
        public Color HeadSelectedBorderColor { get; set; } = Color.FromArgb(232, 232, 232);

        [DefaultValue(typeof(Color), "White")]
        [Description("TabPage头部默认背景颜色")]
        public Color HeaderBackColor { get; set; } = Color.White;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (DesignMode)
            {
                var backBrush = new LinearGradientBrush(Bounds, SystemColors.ControlLightLight,
                    SystemColors.ControlLight, LinearGradientMode.Vertical);
                pevent.Graphics.FillRectangle(backBrush, Bounds);
                backBrush.Dispose();
            }
            else
            {
                PaintTransparentBackground(pevent.Graphics, ClientRectangle);
            }
        }

        protected void PaintTransparentBackground(Graphics g, Rectangle rect)
        {
            if (Parent != null)
            {
                rect.Offset(Location);
                var e = new PaintEventArgs(g, rect);
                var state = g.Save();
                g.SmoothingMode = SmoothingMode.HighSpeed;
                try
                {
                    g.TranslateTransform(-Location.X, -Location.Y);
                    InvokePaintBackground(Parent, e);
                    InvokePaint(Parent, e);
                }
                finally
                {
                    g.Restore(state);
                    rect.Offset(-Location.X, -Location.Y);
                    using var brush = new SolidBrush(_backColor);
                    rect.Inflate(1,1);
                    g.FillRectangle(brush, rect);
                }
            }
            else
            {
                var backBrush = new LinearGradientBrush(Bounds, SystemColors.ControlLightLight, SystemColors.ControlLight, LinearGradientMode.Vertical);
                g.FillRectangle(backBrush, Bounds);
                backBrush.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            PaintTransparentBackground(e.Graphics,ClientRectangle);
            PaintAllTheTabs(e);
            PaintTheTabPageBorder(e);
            PaintTheSelectedTab(e);
        }

        private void PaintAllTheTabs(PaintEventArgs e)
        {
            if (TabCount <= 0) return;
            for (var i = 0; i < TabCount; i++)
            {
                PaintTab(e, i);
            }
        }

        private void PaintTab(PaintEventArgs e, int index)
        {
            var path = GetTabPath(index);
            PaintTabBackground(e.Graphics, index, path);
            PaintTabBorder(e.Graphics, index, path);
            PaintTabText(e.Graphics, index);
            PaintTabImage(e.Graphics, index);
        }

        private void PaintTabBackground(Graphics g, int index, GraphicsPath path)
        {
            var rectangle = GetTabRect(index);
            Brush buttonBrush = new LinearGradientBrush(rectangle, HeaderBackColor,HeaderBackColor, LinearGradientMode.Vertical);
            g.FillPath(buttonBrush, path);
        }

        private void PaintTabBorder(Graphics g, int index, GraphicsPath path)
        {
            var borderPen = new Pen(_borderColor);
            if (index == SelectedIndex)
            {
                borderPen = new Pen(HeadSelectedBorderColor);
            }

            g.DrawPath(borderPen, path);
            borderPen.Dispose();
        }

        private void PaintTabImage(Graphics g, int index)
        {
            Image? tabImage = null;
            if (TabPages[index].ImageIndex > -1 && ImageList != null)
            {
                tabImage = ImageList.Images[TabPages[index].ImageIndex];
            }
            else if (TabPages[index].ImageKey.Trim().Length > 0 && ImageList != null)
            {
                tabImage = ImageList.Images[TabPages[index].ImageKey];
            }

            if (tabImage == null) return;
            var rect  = GetTabRect(index);
            g.DrawImage(tabImage, rect.Right - rect.Height - 4, 4, rect.Height-2, rect.Height -2);
        }

        private void PaintTabText(Graphics g, int index)
        {
            var tabText = TabPages[index].Text;
            var format = new StringFormat
            {
                Alignment = StringAlignment.Near,
                LineAlignment = StringAlignment.Center,
                Trimming = StringTrimming.EllipsisCharacter
            };
            var foreBrush = TabPages[index].Enabled == false ? SystemBrushes.ControlDark : SystemBrushes.ControlText;
            var tabFont = Font;
            if (index == SelectedIndex)
            {
                if (TabPages[index].Enabled)
                {
                    foreBrush = new SolidBrush(HeadSelectedBackColor);
                }
            }
            var rectangle = GetTabRect(index);

            var txtSize = ControlHelper.GetStringWidth(tabText, g, tabFont);
            var rect = rectangle with { X = rectangle.Left + (rectangle.Width - txtSize) / 2 - 1, Y = rectangle.Top };
            g.DrawString(tabText, tabFont, foreBrush, rect, format);
        }

        private void PaintTheTabPageBorder(PaintEventArgs e)
        {
            if (TabCount <= 0) return;
            var borderRectangle = TabPages[0].Bounds;
            var rect = new Rectangle(borderRectangle.X - 2, borderRectangle.Y - 1, borderRectangle.Width + 5,
                borderRectangle.Height + 2);
            ControlPaint.DrawBorder(e.Graphics, rect, BackColor, ButtonBorderStyle.Solid);
        }

        private void PaintTheSelectedTab(PaintEventArgs e)
        {
            if (SelectedIndex == -1)
            {
                return;
            }

            var selectedRectangle = GetTabRect(SelectedIndex);
            var selectedRectRight = selectedRectangle.Right;
            e.Graphics.DrawLine(new Pen(HeadSelectedBackColor), selectedRectangle.Left, selectedRectangle.Bottom + 1,
                selectedRectRight, selectedRectangle.Bottom + 1);
        }

        private GraphicsPath GetTabPath(int index)
        {
            GraphicsPath path = new();
            path.Reset();

            var rectangle = GetTabRect(index);
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
            var hFont = Font.ToHfont();
            SendMessage(Handle, WM_SETFONT, hFont, (IntPtr)(-1));
            SendMessage(Handle, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
            UpdateStyles();
        }
    }
}
