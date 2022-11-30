using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Caty.Tools.UxForm.Controls;

public partial class UxControlBase : UserControl
{
    /// <summary>
    /// 是否圆角
    /// </summary>
    [Description("是否圆角"), Category("自定义")]
    public bool IsRadius { get; set; } = false;

    /// <summary>
    /// 圆角角度
    /// </summary>
    [Description("圆角角度"), Category("自定义")]
    public int CornerRadius { get; set; } = 24;

    /// <summary>
    /// 是否显示边框
    /// </summary>
    [Description("是否显示边框"), Category("自定义")]
    public bool IsShowRect { get; set; } = false;

    /// <summary>
    /// 边框颜色
    /// </summary>
    [Description("边框颜色"), Category("自定义")]
    public Color RectColor { get; set; }= Color.FromArgb(220, 220, 220);

    /// <summary>
    /// 边框宽度
    /// </summary>
    [Description("边框宽度"), Category("自定义")]
    public int RectWidth { get; set; } = 1;

    /// <summary>
    /// 当使用边框时填充颜色，当值为背景色或透明色或空值则不填充
    /// </summary>
    [Description("当使用边框时填充颜色，当值为背景色或透明色或空值则不填充"), Category("自定义")]
    public Color FillColor { get; set; } = Color.Transparent;

    public UxControlBase()
    {
        InitializeComponent();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        if (Visible)
        {
            if (IsRadius)
            {
                SetWindowRegion();
            }

            if (IsShowRect)
            {
                var rectColor = RectColor;
                var pen = new Pen(rectColor, RectWidth);
                var graphicsPath = new GraphicsPath();
                graphicsPath.AddArc(0, 0, CornerRadius, CornerRadius, 180, 90);
                graphicsPath.AddArc(ClientRectangle.Width - CornerRadius - 1, 0, CornerRadius, CornerRadius, 270,
                    90);
                graphicsPath.AddArc(ClientRectangle.Width - CornerRadius - 1,
                    ClientRectangle.Height - CornerRadius - 1, CornerRadius, CornerRadius, 0, 90);
                graphicsPath.AddArc(0, ClientRectangle.Height - CornerRadius - 1, CornerRadius, CornerRadius, 90,
                    90);
                graphicsPath.CloseFigure();
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                if (FillColor != Color.Empty && FillColor != Color.Transparent && FillColor != BackColor)
                {
                    e.Graphics.FillPath(new SolidBrush(FillColor), graphicsPath);
                }
                e.Graphics.DrawPath(pen, graphicsPath);
            }
        }
        base.OnPaint(e);
    }

    private void SetWindowRegion()
    {
        var rect = new Rectangle(-1, -1, Width + 1, Height);
        var path = GetRoundedRectPath(rect, CornerRadius);
        Region = new Region(path);
    }

    private static GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
    {
        var locationRect = new Rectangle(rect.Location, new Size(radius, radius));
        var path = new GraphicsPath();
        // 左上角
        path.AddArc(locationRect, 180, 90); 
        locationRect.X = rect.Right - radius;
        // 右上角
        path.AddArc(locationRect, 270, 90);
        locationRect.Y = rect.Bottom - radius;
        locationRect.Width += 1;
        locationRect.Height += 1;
        // 右下角
        path.AddArc(locationRect,360,90);
        locationRect.X = rect.Left;
        // 左下角
        path.AddArc(locationRect, 90, 90);
        path.CloseFigure();
        return path;
    }
}