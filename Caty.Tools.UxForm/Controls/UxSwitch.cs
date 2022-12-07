using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls;

[DefaultEvent("CheckedChanged")]
public partial class UxSwitch : UserControl
{
    [Description("选中改变事件"), Category("自定义")]
    public event EventHandler? CheckedChanged;

    private Color _trueColor = Color.FromArgb(34, 163, 169);

    [Description("选中时颜色"), Category("自定义")]
    public Color TrueColor
    {
        get => _trueColor;
        set
        {
            _trueColor = value;
            Refresh();
        }
    }

    private Color _falseColor = Color.FromArgb(111, 122, 126);

    [Description("没有选中时颜色"), Category("自定义")]
    public Color FalseColor
    {
        get => _falseColor;
        set
        {
            _falseColor = value;
            Refresh();
        }
    }

    private bool _checked;

    [Description("是否选中"), Category("自定义")]
    public bool Checked
    {
        get => _checked;
        set
        {
            _checked = value;
            Refresh();
            CheckedChanged?.Invoke(this, null);
        }
    }

    private string[] _texts;

    [Description("文本值，当选中或没有选中时显示，必须是长度为2的数组"), Category("自定义")]
    public string[] Texts
    {
        get => _texts;
        set
        {
            _texts = value;
            Refresh();
        }
    }

    private SwitchType _switchType = SwitchType.Ellipse;

    [Description("显示类型"), Category("自定义")]
    public SwitchType SwitchType
    {
        get => _switchType;
        set
        {
            _switchType = value;
            Refresh();
        }
    }

    public override Font Font
    {
        get => base.Font;
        set
        {
            base.Font = value;
            Refresh();
        }
    }


    public UxSwitch()
    {
        InitializeComponent();
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);
        MouseDown += UxSwitch_MouseDown;
    }

    private void UxSwitch_MouseDown(object sender, MouseEventArgs e)
    {
        Checked = !Checked;
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SetGDIHigh();
        switch (_switchType)
        {
            case SwitchType.Ellipse:
            {
                var fillColor = _checked ? _trueColor : _falseColor;
                var path = new GraphicsPath();
                path.AddLine(new Point(Height / 2, 1), new Point(Width - Height / 2, 1));
                path.AddArc(new Rectangle(Width - Height - 1, 1, Height - 2, Height - 2), -90, 180);
                path.AddLine(new Point(Width - Height / 2, Height - 1),
                    new Point(Height / 2, Height - 1));
                path.AddArc(new Rectangle(1, 1, Height - 2, Height - 2), 90, 180);
                g.FillPath(new SolidBrush(fillColor), path);

                var strText = string.Empty;
                if (_texts is { Length: 2 })
                {
                    strText = _checked ? _texts[0] : _texts[1];
                }

                if (_checked)
                {
                    g.FillEllipse(Brushes.White,
                        new Rectangle(Width - Height - 1 + 2, 1 + 2, Height - 2 - 4,
                            Height - 2 - 4));
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2),
                            new Rectangle((Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2,
                                (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                                (Height - 2 - 4) / 2));
                    }
                    else
                    {
                        var sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        var intTextY = (Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point((Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    g.FillEllipse(Brushes.White, new Rectangle(1 + 2, 1 + 2, Height - 2 - 4, Height - 2 - 4));
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2),
                            new Rectangle(Width - 2 - (Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2,
                                (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                                (Height - 2 - 4) / 2));
                    }
                    else
                    {
                        var sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        var intTextY = (Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White,
                            new Point(
                                Width - 2 - (Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2 -
                                (int)sizeF.Width / 2, intTextY));
                    }
                }

                break;
            }
            case SwitchType.Quadrilateral:
            {
                var fillColor = _checked ? _trueColor : _falseColor;
                var path = new GraphicsPath();
                const int intRadius = 5;
                path.AddArc(0, 0, intRadius, intRadius, 180f, 90f);
                path.AddArc(Width - intRadius - 1, 0, intRadius, intRadius, 270f, 90f);
                path.AddArc(Width - intRadius - 1, Height - intRadius - 1, intRadius, intRadius, 0f, 90f);
                path.AddArc(0, Height - intRadius - 1, intRadius, intRadius, 90f, 90f);

                g.FillPath(new SolidBrush(fillColor), path);

                var strText = string.Empty;
                if (_texts is { Length: 2 })
                {
                    strText = _checked ? _texts[0] : _texts[1];
                }

                if (_checked)
                {
                    var path2 = new GraphicsPath();
                    path2.AddArc(Width - Height - 1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(Width - 1 - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(Width - 1 - 2 - intRadius, Height - 2 - intRadius - 1, intRadius, intRadius,
                        0f, 90f);
                    path2.AddArc(Width - Height - 1 + 2, Height - 2 - intRadius - 1, intRadius,
                        intRadius, 90f, 90f);
                    g.FillPath(Brushes.White, path2);

                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2),
                            new Rectangle((Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2,
                                (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                                (Height - 2 - 4) / 2));
                    }
                    else
                    {
                        var sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        var intTextY = (Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White, new Point((Height - 2 - 4) / 2, intTextY));
                    }
                }
                else
                {
                    var path2 = new GraphicsPath();
                    path2.AddArc(1 + 2, 1 + 2, intRadius, intRadius, 180f, 90f);
                    path2.AddArc(Height - 2 - intRadius, 1 + 2, intRadius, intRadius, 270f, 90f);
                    path2.AddArc(Height - 2 - intRadius, Height - 2 - intRadius - 1, intRadius, intRadius, 0f,
                        90f);
                    path2.AddArc(1 + 2, Height - 2 - intRadius - 1, intRadius, intRadius, 90f, 90f);
                    g.FillPath(Brushes.White, path2);
                    
                    if (string.IsNullOrEmpty(strText))
                    {
                        g.DrawEllipse(new Pen(Color.White, 2),
                            new Rectangle(Width - 2 - (Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2,
                                (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                                (Height - 2 - 4) / 2));
                    }
                    else
                    {
                        var sizeF = g.MeasureString(strText.Replace(" ", "A"), Font);
                        var intTextY = (Height - (int)sizeF.Height) / 2 + 2;
                        g.DrawString(strText, Font, Brushes.White,
                            new Point(
                                Width - 2 - (Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2 -
                                (int)sizeF.Width / 2, intTextY));
                    }
                }

                break;
            }
            default:
            {
                var fillColor = _checked ? _trueColor : _falseColor;
                var intLineHeight = (Height - 2 - 4) / 2;

                var path = new GraphicsPath();
                path.AddLine(new Point(Height / 2, (Height - intLineHeight) / 2),
                    new Point(Width - Height / 2, (Height - intLineHeight) / 2));
                path.AddArc(
                    new Rectangle(Width - Height / 2 - intLineHeight - 1, (Height - intLineHeight) / 2,
                        intLineHeight, intLineHeight), -90, 180);
                path.AddLine(new Point(Width - Height / 2, (Height - intLineHeight) / 2 + intLineHeight),
                    new Point(Width - Height / 2, (Height - intLineHeight) / 2 + intLineHeight));
                path.AddArc(
                    new Rectangle(Height / 2, (Height - intLineHeight) / 2, intLineHeight, intLineHeight), 90,
                    180);
                g.FillPath(new SolidBrush(fillColor), path);

                if (_checked)
                {
                    g.FillEllipse(new SolidBrush(fillColor),
                        new Rectangle(Width - Height - 1 + 2, 1 + 2, Height - 2 - 4,
                            Height - 2 - 4));
                    g.FillEllipse(Brushes.White,
                        new Rectangle(Width - 2 - (Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2 - 4,
                            (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                            (Height - 2 - 4) / 2));
                }
                else
                {
                    g.FillEllipse(new SolidBrush(fillColor),
                        new Rectangle(1 + 2, 1 + 2, Height - 2 - 4, Height - 2 - 4));
                    g.FillEllipse(Brushes.White,
                        new Rectangle((Height - 2 - 4) / 2 - ((Height - 2 - 4) / 2) / 2 + 4,
                            (Height - 2 - (Height - 2 - 4) / 2) / 2 + 1, (Height - 2 - 4) / 2,
                            (Height - 2 - 4) / 2));
                }

                break;
            }
        }
    }

}

public enum SwitchType
{
    /// 
    /// 椭圆
    /// 
    Ellipse,

    /// 
    /// 四边形
    /// 
    Quadrilateral,

    /// 
    /// 横线
    /// 
    Line
}