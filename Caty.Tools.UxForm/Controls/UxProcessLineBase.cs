using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls;

public class UxProcessLineBase : Control
{
    [Description("值变更事件"), Category("自定义")]
    public event EventHandler? ValueChanged;

    private int _value;

    [Description("当前属性"), Category("自定义")]
    public int Value
    {
        get => _value;
        set
        {
            if (value > _maxValue)
                _value = _maxValue;
            else if (value < 0)
                _value = 0;
            else
                _value = value;
            ValueChanged?.Invoke(this, null);
            Refresh();
        }
    }

    private int _maxValue = 100;

    [Description("最大值"), Category("自定义")]
    public int MaxValue
    {
        get => _maxValue;
        set
        {
            _maxValue = value < _value ? _value : value;
            Refresh();
        }
    }

    private Color _valueColor = Color.FromArgb(73, 119, 232);

    [Description("值进度条颜色"), Category("自定义")]
    public Color ValueColor
    {
        get => _valueColor;
        set
        {
            _valueColor = value;
            Refresh();
        }
    }

    private Color _valueBackGroundColor = Color.White;

    [Description("值背景色"), Category("自定义")]
    public Color ValueBackGroundColor
    {
        get => _valueBackGroundColor;
        set
        {
            _valueBackGroundColor = value;
            Refresh();
        }
    }

    private Color _borderColor = Color.FromArgb(192, 192, 192);

    [Description("边框颜色"), Category("自定义")]
    public Color BorderColor
    {
        get => _borderColor;
        set
        {
            _borderColor = value;
            Refresh();
        }
    }

    [Description("值字体"), Category("自定义")]
    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font = value;
            Refresh();
        }
    }

    [Description("值字体颜色"), Category("自定义")]
    public override Color ForeColor
    {
        get => base.ForeColor;
        set
        {
            base.ForeColor = value;
            Refresh();
        }
    }

    private ValueTextType _valueTextType = ValueTextType.Percent;

    [Description("值显示样式"), Category("自定义")]
    public ValueTextType ValueTextType
    {
        get => _valueTextType;
        set
        {
            _valueTextType = value;
            Refresh();
        }
    }

    public UxProcessLineBase()
    {
        Size = new Size(200, 15);
        ForeColor = Color.FromArgb(255, 77, 59);
        Font = new Font("Arial Unicode MS", 10);
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        Console.WriteLine(DateTime.Now);
        base.OnPaint(e);
        var g = e.Graphics;
        g.SetGDIHigh();

        Brush sb = new SolidBrush(_valueBackGroundColor);
        g.FillRectangle(sb,
            new Rectangle(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width - 3,
                ClientRectangle.Height - 2));
        var path1 = new Rectangle(ClientRectangle.X, ClientRectangle.Y + 1, ClientRectangle.Width - 3,
            ClientRectangle.Height - 4).CreateRoundedRectanglePath(3);
        g.DrawPath(new Pen(_borderColor, 1), path1);
        var lgb = new LinearGradientBrush(new Point(0, 0),
            new Point(0, ClientRectangle.Height - 3), _valueColor,
            Color.FromArgb(200, _valueColor.R, _valueColor.G, _valueColor.B));
        g.FillPath(lgb,
            new Rectangle(0, (ClientRectangle.Height - (ClientRectangle.Height - 3)) / 2,
                (ClientRectangle.Width - 3) * Value / _maxValue, ClientRectangle.Height - 4).CreateRoundedRectanglePath(3));
        var strValue = _valueTextType switch
        {
            ValueTextType.Percent => (Value / (float)_maxValue).ToString("0%"),
            ValueTextType.Absolute => Value + "/" + _maxValue,
            _ => string.Empty
        };
        if (string.IsNullOrEmpty(strValue)) return;
        var sizeF = g.MeasureString(strValue, Font);
        g.DrawString(strValue, Font, new SolidBrush(ForeColor),
            new PointF((Width - sizeF.Width) / 2, (Height - sizeF.Height) / 2 + 1));
    }

}

public enum ValueTextType
{
    None,

    /// 
    /// 百分比
    /// 
    Percent,

    /// 
    /// 数值
    /// 
    Absolute
}