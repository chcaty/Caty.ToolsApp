using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Caty.Tools.UxForm.Controls;

public partial class UxProcessEllipse : UserControl
{
    [Description("值改变事件"), Category("自定义")]
    public event EventHandler? ValueChanged;

    private Color _backEllipseColor = Color.FromArgb(22, 160, 133);

    /// 
    /// 圆背景色
    /// 
    [Description("圆背景色"), Category("自定义")]
    public Color BackEllipseColor
    {
        get => _backEllipseColor;
        set
        {
            _backEllipseColor = value;
            Refresh();
        }
    }

    private Color _coreEllipseColor = Color.FromArgb(180, 180, 180);

    /// 
    /// 内圆颜色，ShowType=Ring 有效
    /// 
    [Description("内圆颜色，ShowType=Ring 有效"), Category("自定义")]
    public Color CoreEllipseColor
    {
        get => _coreEllipseColor;
        set
        {
            _coreEllipseColor = value;
            Refresh();
        }
    }

    private Color _valueColor = Color.FromArgb(255, 77, 59);

    [Description("值圆颜色"), Category("自定义")]
    public Color ValueColor
    {
        get => _valueColor;
        set
        {
            _valueColor = value;
            Refresh();
        }
    }

    private bool _isShowCoreEllipseBorder = true;

    /// 
    /// 内圆是否显示边框，ShowType=Ring 有效
    /// 
    [Description("内圆是否显示边框，ShowType=Ring 有效"), Category("自定义")]
    public bool IsShowCoreEllipseBorder
    {
        get => _isShowCoreEllipseBorder;
        set
        {
            _isShowCoreEllipseBorder = value;
            Refresh();
        }
    }

    private ValueType _valueType = ValueType.Percent;

    /// 
    /// 值文字类型
    /// 
    [Description("值文字类型"), Category("自定义")]
    public ValueType ValueType
    {
        get => _valueType;
        set
        {
            _valueType = value;
            Refresh();
        }
    }

    private int _valueWidth = 30;

    /// 
    /// 外圆值宽度
    /// 
    [Description("外圆值宽度，ShowType=Ring 有效"), Category("自定义")]
    public int ValueWidth
    {
        get => _valueWidth;
        set
        {
            if (value <= 0 || value > Math.Min(Width, Height))
                return;
            _valueWidth = value;
            Refresh();
        }
    }

    private int _valueMargin = 5;

    /// 
    /// 外圆值间距
    /// 
    [Description("外圆值间距"), Category("自定义")]
    public int ValueMargin
    {
        get => _valueMargin;
        set
        {
            if (value < 0 || _valueMargin >= _valueWidth)
                return;
            _valueMargin = value;
            Refresh();
        }
    }

    private int _maxValue = 100;

    /// 
    /// 最大值
    /// 
    [Description("最大值"), Category("自定义")]
    public int MaxValue
    {
        get => _maxValue;
        set
        {
            if (value > _value || value <= 0)
                return;
            _maxValue = value;
            Refresh();
        }
    }

    private int _value;

    /// 
    /// 当前值
    /// 
    [Description("当前值"), Category("自定义")]
    public int Value
    {
        get => _value;
        set
        {
            if (_maxValue < value || value <= 0)
                return;
            _value = value;
            ValueChanged?.Invoke(this, null);
            Refresh();
        }
    }

    private Font _font = new("Arial Unicode MS", 20);

    [Description("文字字体"), Category("自定义")]
    public override Font Font
    {
        get => _font;
        set
        {
            _font = value;
            Refresh();
        }
    }

    private Color _foreColor = Color.White;

    [Description("文字颜色"), Category("自定义")]
    public override Color ForeColor
    {
        get => _foreColor;
        set
        {
            _foreColor = value;
            Refresh();
        }
    }

    private ShowType _showType = ShowType.Ring;

    [Description("显示类型"), Category("自定义")]
    public ShowType ShowType
    {
        get => _showType;
        set
        {
            _showType = value;
            Refresh();
        }
    }

    public UxProcessEllipse()
    {
        InitializeComponent();
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        var g = e.Graphics;
        g.SmoothingMode = SmoothingMode.AntiAlias; //使绘图质量最高，即消除锯齿
        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        g.CompositingQuality = CompositingQuality.HighQuality;

        var intWidth = Math.Min(Size.Width, Size.Height);
        //底圆
        g.FillEllipse(new SolidBrush(_backEllipseColor),
            new Rectangle(new Point(0, 0), new Size(intWidth, intWidth)));
        if (_showType == ShowType.Ring)
        {
            //中心圆
            var intCore = intWidth - _valueWidth * 2;
            g.FillEllipse(new SolidBrush(_coreEllipseColor),
                new Rectangle(new Point(_valueWidth, _valueWidth), new Size(intCore, intCore)));
            //中心圆边框
            if (_isShowCoreEllipseBorder)
            {
                g.DrawEllipse(new Pen(_valueColor, 2),
                    new Rectangle(new Point(_valueWidth + 1, _valueWidth + 1),
                        new Size(intCore - 1, intCore - 1)));
            }

            if (_value <= 0 || _maxValue <= 0) return;
            var fltPercent = _value / (float)_maxValue;
            if (fltPercent > 1)
            {
                fltPercent = 1;
            }

            g.DrawArc(new Pen(_valueColor, _valueWidth - _valueMargin * 2),
                new RectangleF(
                    new Point(_valueWidth / 2 + _valueMargin / 4, _valueWidth / 2 + _valueMargin / 4),
                    new SizeF(intWidth - _valueWidth - _valueMargin / 2 + (_valueMargin == 0 ? 0 : 1),
                        intWidth - _valueWidth - _valueMargin / 2 + (_valueMargin == 0 ? 0 : 1))), -90,
                fltPercent * 360);

            var strValueText = _valueType == ValueType.Percent
                ? fltPercent.ToString("0%")
                : _value.ToString();
            var txtSize = g.MeasureString(strValueText, Font);
            g.DrawString(strValueText, Font, new SolidBrush(ForeColor),
                new PointF((intWidth - txtSize.Width) / 2 + 1, (intWidth - txtSize.Height) / 2 + 1));
        }
        else
        {
            if (_value <= 0 || _maxValue <= 0) return;
            var fltPercent = _value / (float)_maxValue;
            if (fltPercent > 1)
            {
                fltPercent = 1;
            }

            g.FillPie(new SolidBrush(_valueColor),
                new Rectangle(_valueMargin, _valueMargin, intWidth - _valueMargin * 2,
                    intWidth - _valueMargin * 2), -90, fltPercent * 360);

            var strValueText = _valueType == ValueType.Percent
                ? fltPercent.ToString("0%")
                : _value.ToString();
            var txtSize = g.MeasureString(strValueText, Font);
            g.DrawString(strValueText, Font, new SolidBrush(ForeColor),
                new PointF((intWidth - txtSize.Width) / 2 + 1, (intWidth - txtSize.Height) / 2 + 1));
        }

    }
}

public enum ValueType
{
    /// 
    /// 百分比
    /// 
    Percent,

    /// 
    /// 数值
    /// 
    Absolute
}

public enum ShowType
{
    /// 
    /// 圆环
    /// 
    Ring,

    /// 
    /// 扇形
    /// 
    Sector
}