using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace Caty.Tools.UxForm.Controls;

public class UxWaveChart:UxControlBase
{
    /// <summary>
    /// The m wave actual width
    /// </summary>
    private int _waveActualWidth = 50;

    /// <summary>
    /// The m wave width
    /// </summary>
    private int _waveWidth = 50;

    /// <summary>
    /// Gets or sets the width of the wave.
    /// </summary>
    /// <value>The width of the wave.</value>
    [Description("波形宽度"), Category("自定义")]
    public int WaveWidth
    {
        get => _waveWidth;
        set
        {
            if (value <= 0)
                return;
            _waveWidth = value;
            ResetWaveCount();
            Refresh();
        }
    }

    /// <summary>
    /// The m sleep time
    /// </summary>
    private int _sleepTime = 1000;
    /// <summary>
    /// 波运行速度（运行时间间隔，毫秒）
    /// </summary>
    /// <value>The sleep time.</value>
    [Description("运行速度（运行时间间隔，毫秒）"), Category("自定义")]
    public int SleepTime
    {
        get => _sleepTime;
        set
        {
            if (value <= 0)
                return;
            _sleepTime = value;
            if (_timer == null) return;
            _timer.Enabled = false;
            _timer.Interval = value;
            _timer.Enabled = true;
        }
    }

    /// <summary>
    /// The m line tension
    /// </summary>
    private float _lineTension = 0.5f;
    /// <summary>
    /// 线弯曲程度
    /// </summary>
    /// <value>The line tension.</value>
    [Description("线弯曲程度(0-1）"), Category("自定义")]
    public float LineTension
    {
        get => _lineTension;
        set
        {
            if (value is not (>= 0 and <= 1))
            {
                return;
            }
            _lineTension = value;
            Refresh();
        }
    }

    /// <summary>
    /// The m line color
    /// </summary>
    private Color _lineColor = Color.FromArgb(150, 255, 77, 59);

    /// <summary>
    /// Gets or sets the color of the line.
    /// </summary>
    /// <value>The color of the line.</value>
    [Description("曲线颜色"), Category("自定义")]
    public Color LineColor
    {
        get => _lineColor;
        set
        {
            _lineColor = value;
            Refresh();

        }
    }

    /// <summary>
    /// The m grid line color
    /// </summary>
    private Color _gridLineColor = Color.FromArgb(50, 255, 77, 59);

    /// <summary>
    /// Gets or sets the color of the grid line.
    /// </summary>
    /// <value>The color of the grid line.</value>
    [Description("网格线颜色"), Category("自定义")]
    public Color GridLineColor
    {
        get => _gridLineColor;
        set
        {
            _gridLineColor = value;
            Refresh();
        }
    }

    /// <summary>
    /// The m grid line text color
    /// </summary>
    private Color _gridLineTextColor = Color.FromArgb(150, 255, 77, 59);

    /// <summary>
    /// Gets or sets the color of the grid line text.
    /// </summary>
    /// <value>The color of the grid line text.</value>
    [Description("网格文本颜色"), Category("自定义")]
    public Color GridLineTextColor
    {
        get => _gridLineTextColor;
        set
        {
            _gridLineTextColor = value;
            Refresh();
        }
    }

    /// <summary>
    /// 数据源，用以缓存所有需要显示的数据
    /// </summary>
    private readonly List<KeyValuePair<string, double>> _dataSource = new();
    /// <summary>
    /// 当前需要显示的数据
    /// </summary>
    private List<KeyValuePair<string, double>> _currentSource = new();
    /// <summary>
    /// The timer
    /// </summary>
    private readonly Timer _timer = new();
    /// <summary>
    /// 画图区域
    /// </summary>
    private Rectangle _drawRect;

    /// <summary>
    /// The m wave count
    /// </summary>
    private int _waveCount;
    /// <summary>
    /// Initializes a new instance of the <see cref="UxWaveChart" /> class.
    /// </summary>
    public UxWaveChart()
    {
        SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        SetStyle(ControlStyles.DoubleBuffer, true);
        SetStyle(ControlStyles.ResizeRedraw, true);
        SetStyle(ControlStyles.Selectable, true);
        SetStyle(ControlStyles.SupportsTransparentBackColor, true);
        SetStyle(ControlStyles.UserPaint, true);

        SizeChanged += UxWaveWithSource_SizeChanged;
        IsShowRect = true;
        RectColor = Color.FromArgb(232, 232, 232);
        FillColor = Color.FromArgb(50, 255, 77, 59);
        RectWidth = 1;
        CornerRadius = 10;
        IsRadius = true;
        Size = new Size(300, 200);

        _timer.Interval = _sleepTime;
        _timer.Tick += timer_Tick;
        VisibleChanged += UxWave_VisibleChanged;
    }


    /// <summary>
    /// 添加需要显示的数据
    /// </summary>
    /// <param name="key">名称</param>
    /// <param name="value">值</param>
    public void AddSource(string key, double value)
    {
        _dataSource.Add(new KeyValuePair<string, double>(key, value));
    }

    /// <summary>
    /// Handles the VisibleChanged event of the UCWave control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void UxWave_VisibleChanged(object sender, EventArgs e)
    {
        if (!DesignMode)
        {
            _timer.Enabled = Visible;
        }
    }

    /// <summary>
    /// Handles the Tick event of the timer control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void timer_Tick(object sender, EventArgs e)
    {
        _currentSource = GetCurrentList();
        _dataSource.RemoveAt(0);
        Refresh();
    }
    /// <summary>
    /// Handles the SizeChanged event of the UCWaveWithSource control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void UxWaveWithSource_SizeChanged(object sender, EventArgs e)
    {
        _drawRect = new Rectangle(60, 20, Width - 80, Height - 60);
        ResetWaveCount();
    }

    /// <summary>
    /// 引发 <see cref="E:System.Windows.Forms.Control.Paint" /> 事件。
    /// </summary>
    /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        var g = e.Graphics;
        g.SetGDIHigh();

        var intLineSplit = _drawRect.Height / 4;
        for (var i = 0; i <= 4; i++)
        {
            var pen = new Pen(new SolidBrush(_gridLineColor), 1);
            // pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
            g.DrawLine(pen, _drawRect.Left, _drawRect.Bottom - 1 - i * intLineSplit, _drawRect.Right, _drawRect.Bottom - 1 - i * intLineSplit);
        }

        if (_currentSource is not { Count: > 0 })
        {
            for (var i = 0; i <= 4; i++)
            {
                var strText = (100 / 4 * i).ToString();
                var numSize = g.MeasureString(strText, Font);
                g.DrawString(strText, Font, new SolidBrush(_gridLineTextColor), _drawRect.Left - numSize.Width - 1, _drawRect.Bottom - 1 - i * intLineSplit - (numSize.Height / 2));
            }
            return;
        }
        var lst1 = new List<Point>();
        var dblValue = _currentSource.Max(p => p.Value);
        var intValue = (int)dblValue;
        var intDivisor = ("1".PadRight(intValue.ToString().Length - 1, '0')).ToInt();
        if (intDivisor < 100)
            intDivisor = 100;
        var intTop = intValue;
        if (intValue % intDivisor != 0)
        {
            intTop = (intValue / intDivisor + 1) * intDivisor;
        }
        if (intTop == 0)
            intTop = 100;

        for (var i = 0; i <= 4; i++)
        {
            var strText = (intTop / 4 * i).ToString();
            var numSize = g.MeasureString(strText, Font);
            g.DrawString(strText, Font, new SolidBrush(_gridLineTextColor), _drawRect.Left - numSize.Width - 1, _drawRect.Bottom - 1 - i * intLineSplit - (numSize.Height / 2));
        }

        for (var i = 0; i < _currentSource.Count; i++)
        {
            var intEndX = i * _waveActualWidth + _drawRect.X;
            var intEndY = _drawRect.Bottom - 1 - (int)(_currentSource[i].Value / intTop * _drawRect.Height);
            lst1.Add(new Point(intEndX, intEndY));
            if (string.IsNullOrEmpty(_currentSource[i].Key)) continue;
            var numSize = g.MeasureString(_currentSource[i].Key, Font);
            var txtX = intEndX - (int)(numSize.Width / 2) + 1;
            g.DrawString(_currentSource[i].Key, Font, new SolidBrush(_gridLineTextColor), new PointF(txtX, _drawRect.Bottom + 5));
        }


        var path1 = new GraphicsPath();
        path1.AddCurve(lst1.ToArray(), _lineTension);
        g.DrawPath(new Pen(new SolidBrush(_lineColor), 1), path1);

    }
    /// <summary>
    /// 得到当前需要画图的数据
    /// </summary>
    /// <returns>List&lt;KeyValuePair&lt;System.String, System.Double&gt;&gt;.</returns>
    private List<KeyValuePair<string, double>> GetCurrentList()
    {
        if (_dataSource.Count < _waveCount)
        {
            var intCount = _waveCount - _dataSource.Count;
            for (var i = 0; i < intCount; i++)
            {
                _dataSource.Add(new KeyValuePair<string, double>("", 0));
            }
        }

        var lst = _dataSource.GetRange(0, _waveCount);
        if (lst.Count == 1)
            lst.Insert(0, new KeyValuePair<string, double>("", 0));
        return lst;
    }

    /// <summary>
    /// 计算需要显示的个数
    /// </summary>
    private void ResetWaveCount()
    {
        _waveCount = _drawRect.Width / _waveWidth;
        _waveActualWidth = _waveWidth + (_drawRect.Width % _waveWidth) / _waveCount;
        _waveCount++;
        if (_dataSource.Count >= _waveCount) return;
        var intCount = _waveCount - _dataSource.Count;
        for (var i = 0; i < intCount; i++)
        {
            _dataSource.Insert(0, new KeyValuePair<string, double>("", 0));
        }
    }
}