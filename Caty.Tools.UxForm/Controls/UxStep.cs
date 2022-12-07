using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace Caty.Tools.UxForm.Controls;

public partial class UxStep : UserControl
{
    [Description("步骤更改事件"), Category("自定义")]
    public event EventHandler? IndexChecked;

    /// 
    /// 步骤背景色
    /// 
    [Description("步骤背景色"), Category("自定义")]
    public Color StepBackColor { get; set; } = Color.FromArgb(100, 100, 100);

    /// 
    /// 步骤前景色
    /// 
    [Description("步骤前景色"), Category("自定义")]
    public Color StepForeColor { get; set; } = Color.FromArgb(255, 85, 51);

    /// 
    /// 步骤文字颜色
    /// 
    [Description("步骤文字景色"), Category("自定义")]
    public Color StepFontColor { get; set; } = Color.White;

    /// 
    /// 步骤宽度
    /// 
    [Description("步骤宽度景色"), Category("自定义")]
    public int StepWidth { get; set; } = 35;

    private string[] _steps = { "step1", "step2", "step3" };

    [Description("步骤"), Category("自定义")]
    public string[] Steps
    {
        get => _steps;
        set
        {
            if (_steps is not { Length: > 1 })
                return;
            _steps = value;
            Refresh();
        }
    }

    private int _stepIndex;

    [Description("步骤位置"), Category("自定义")]
    public int StepIndex
    {
        get => _stepIndex;
        set
        {
            if (_stepIndex >= Steps.Length)
                return;
            _stepIndex = value;
            Refresh();
            IndexChecked?.Invoke(this, null);
        }
    }

    public UxStep()
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

        if (_steps is not { Length: > 0 }) return;
        var sizeFirst = g.MeasureString(_steps[0], Font);
        var y = (Height - StepWidth - 10 - (int)sizeFirst.Height) / 2;
        if (y < 0)
            y = 0;

        var intTxtY = y + StepWidth + 10;
        var intLeft = 0;
        if (sizeFirst.Width > StepWidth)
        {
            intLeft = (int)(sizeFirst.Width - StepWidth) / 2 + 1;
        }

        var intRight = 0;
        var sizeEnd = g.MeasureString(_steps[^1], Font);
        if (sizeEnd.Width > StepWidth)
        {
            intRight = (int)(sizeEnd.Width - StepWidth) / 2 + 1;
        }
        var intSplitWidth = (Width - _steps.Length - (_steps.Length * StepWidth) - intRight) /
                            (_steps.Length - 1);
        if (intSplitWidth < 20)
            intSplitWidth = 20;

        for (var i = 0; i < _steps.Length; i++)
        {

            #region 画圆，横线

            g.FillEllipse(new SolidBrush(StepBackColor),
                new Rectangle(new Point(intLeft + i * (StepWidth + intSplitWidth), y),
                    new Size(StepWidth, StepWidth)));

            if (_stepIndex > i)
            {
                g.FillEllipse(new SolidBrush(StepForeColor),
                    new Rectangle(new Point(intLeft + i * (StepWidth + intSplitWidth) + 2, y + 2),
                        new Size(StepWidth - 4, StepWidth - 4)));

                if (i != _steps.Length - 1)
                {
                    if (_stepIndex == i + 1)
                    {
                        g.DrawLine(new Pen(StepForeColor, 2),
                            new Point(intLeft + i * (StepWidth + intSplitWidth) + StepWidth,
                                y + (StepWidth / 2)),
                            new Point((i + 1) * (StepWidth + intSplitWidth) - intSplitWidth / 2,
                                y + (StepWidth / 2)));
                        g.DrawLine(new Pen(StepBackColor, 2),
                            new Point(
                                intLeft + i * (StepWidth + intSplitWidth) + StepWidth + intSplitWidth / 2,
                                y + (StepWidth / 2)),
                            new Point((i + 1) * (StepWidth + intSplitWidth), y + (StepWidth / 2)));
                    }
                    else
                    {
                        g.DrawLine(new Pen(StepForeColor, 2),
                            new Point(intLeft + i * (StepWidth + intSplitWidth) + StepWidth,
                                y + (StepWidth / 2)),
                            new Point((i + 1) * (StepWidth + intSplitWidth), y + (StepWidth / 2)));
                    }
                }
            }
            else
            {
                if (i != _steps.Length - 1)
                {
                    g.DrawLine(new Pen(StepBackColor, 2),
                        new Point(intLeft + i * (StepWidth + intSplitWidth) + StepWidth,
                            y + (StepWidth / 2)),
                        new Point((i + 1) * (StepWidth + intSplitWidth), y + (StepWidth / 2)));
                }
            }

            var numSize = g.MeasureString((i + 1).ToString(), Font);
            g.DrawString((i + 1).ToString(), Font, new SolidBrush(StepFontColor),
                new Point(
                    intLeft + i * (StepWidth + intSplitWidth) + (StepWidth - (int)numSize.Width) / 2 + 1,
                    y + (StepWidth - (int)numSize.Height) / 2 + 1));

            #endregion

            var sizeTxt = g.MeasureString(_steps[i], Font);
            g.DrawString(_steps[i], Font, new SolidBrush(_stepIndex > i ? StepForeColor : StepBackColor),
                new Point(
                    intLeft + i * (StepWidth + intSplitWidth) + (StepWidth - (int)sizeTxt.Width) / 2 + 1,
                    intTxtY));
        }

    }
}