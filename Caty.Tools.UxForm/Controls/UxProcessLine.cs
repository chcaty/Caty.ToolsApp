using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls;

public partial class UxProcessLine : UserControl
{
    [Description("值变更事件"), Category("自定义")]
    public event EventHandler? ValueChanged;

    [Description("当前属性"), Category("自定义")]
    public int Value
    {
        get => uxProcessLine1.Value;
        set
        {
            uxProcessLine1.Value = value;
            Refresh();
        }
    }

    [Description("最大值"), Category("自定义")]
    public int MaxValue
    {
        get => uxProcessLine1.MaxValue;
        set
        {
            uxProcessLine1.MaxValue = value;
            Refresh();
        }
    }

    [Description("值进度条颜色"), Category("自定义")]
    public Color ValueColor
    {
        get => uxProcessLine1.ValueColor;
        set
        {
            uxProcessLine1.ValueColor = value;
            Refresh();
        }
    }


    [Description("值背景色"), Category("自定义")]
    public Color ValueBackGroundColor
    {
        get => uxProcessLine1.ValueBackGroundColor;
        set
        {
            uxProcessLine1.ValueBackGroundColor = value;
            Refresh();
        }
    }


    [Description("边框颜色"), Category("自定义")]
    public Color BorderColor
    {
        get => uxProcessLine1.BorderColor;
        set
        {
            uxProcessLine1.BorderColor = value;
            Refresh();
        }
    }

    [Description("值字体"), Category("自定义")]
    public override Font Font
    {
        get => uxProcessLine1.Font;
        set
        {
            uxProcessLine1.Font = value;
            Refresh();
        }
    }

    [Description("值块颜色"), Category("自定义")]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set
        {
            base.ForeColor = value;
            Refresh();
        }
    }

    public UxProcessLine()
    {
        InitializeComponent();
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
        uxProcessLine1.ValueChanged += uxProcessLine1_ValueChanged;
    }

    private void uxProcessLine1_ValueChanged(object sender, EventArgs e)
    {
        ValueChanged?.Invoke(this, e);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        e.Graphics.SetGDIHigh();
        var fltIndex = uxProcessLine1.Value / (float)uxProcessLine1.MaxValue;

        var x = (int)(fltIndex * uxProcessLine1.Width + uxProcessLine1.Location.X - 15) - 2;
        var path = new GraphicsPath();
        var rect = new Rectangle(x, 1, 30, 20);
        const int cornerRadius = 2;
        path.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
        path.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
        path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
        path.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
        path.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2,
            cornerRadius * 2, cornerRadius * 2, 0, 90);
        path.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.Right - cornerRadius * 2 - 5,
            rect.Bottom); //下
        path.AddLine(rect.Right - cornerRadius * 2 - 5, 21, x + 15, uxProcessLine1.Location.Y);
        path.AddLine(x + 15, uxProcessLine1.Location.Y, rect.X + cornerRadius * 2 + 5, 21);
        path.AddLine(rect.X + cornerRadius * 2 + 5, 20, rect.X + cornerRadius * 2, rect.Bottom); //下
        path.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
        path.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2); //上
        path.CloseFigure();

        e.Graphics.FillPath(new SolidBrush(ForeColor), path);

        var strValue = (Value / (float)MaxValue).ToString("0%");
        var sizeF = e.Graphics.MeasureString(strValue, Font);
        e.Graphics.DrawString(strValue, Font, new SolidBrush(Color.White),
            new PointF(x + (30 - sizeF.Width) / 2 + 1, (20 - sizeF.Height) / 2 + 1));
    }
}