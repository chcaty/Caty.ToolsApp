using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace Caty.Tools.UxForm.Controls
{
    public class UxWave:Control
    {
        /// <summary>
        /// Occurs when [on painted].
        /// </summary>
        public event PaintEventHandler? OnPainted;

        /// <summary>
        /// Gets or sets the color of the wave.
        /// </summary>
        /// <value>The color of the wave.</value>
        [Description("波纹颜色"), Category("自定义")]
        public Color WaveColor { get; set; } = Color.FromArgb(255, 77, 59);

        /// <summary>
        /// The m wave width
        /// </summary>
        private int _waveWidth = 200;
        /// <summary>
        /// 为方便计算，强制使用10的倍数
        /// </summary>
        /// <value>The width of the wave.</value>
        [Description("波纹宽度（为方便计算，强制使用10的倍数）"), Category("自定义")]
        public int WaveWidth
        {
            get => _waveWidth;
            set
            {
                _waveWidth = value;
                _waveWidth = _waveWidth / 10 * 10;
                _intLeftX = value * -1;
            }
        }

        /// <summary>
        /// 波高
        /// </summary>
        /// <value>The height of the wave.</value>
        [Description("波高"), Category("自定义")]
        public int WaveHeight { get; set; } = 30;

        /// <summary>
        /// The m wave sleep
        /// </summary>
        private int _waveSleep = 50;
        /// <summary>
        /// 波运行速度（运行时间间隔，毫秒）
        /// </summary>
        /// <value>The wave sleep.</value>
        [Description("波运行速度（运行时间间隔，毫秒）"), Category("自定义")]
        public int WaveSleep
        {
            get => _waveSleep;
            set
            {
                if (value <= 0)
                    return;
                _waveSleep = value;
                if (_timer == null) return;
                _timer.Enabled = false;
                _timer.Interval = value;
                _timer.Enabled = true;
            }
        }

        /// <summary>
        /// The timer
        /// </summary>
        private readonly Timer _timer = new();
        /// <summary>
        /// The int left x
        /// </summary>
        private int _intLeftX = -200;
        /// <summary>
        /// Initializes a new instance of the <see cref="UCWave" /> class.
        /// </summary>
        public UxWave()
        {
            Size = new Size(600, 100);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            _timer.Interval = _waveSleep;
            _timer.Tick += timer_Tick;
            VisibleChanged += UxWave_VisibleChanged;
        }

        /// <summary>
        /// Handles the VisibleChanged event of the UCWave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UxWave_VisibleChanged(object sender, EventArgs e)
        {
            _timer.Enabled = Visible;
        }

        /// <summary>
        /// Handles the Tick event of the timer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void timer_Tick(object sender, EventArgs e)
        {
            _intLeftX -= 10;
            if (_intLeftX == _waveWidth * -2)
                _intLeftX = _waveWidth * -1;
            Refresh();
        }
        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            var g = e.Graphics;
            g.SetGDIHigh();
            var lst1 = new List<Point>();
            var lst2 = new List<Point>();
            var intX = _intLeftX;
            while (true)
            {
                lst1.Add(new Point(intX, 1));
                lst1.Add(new Point(intX + _waveWidth / 2, WaveHeight));

                lst2.Add(new Point(intX + _waveWidth / 2, 1));
                lst2.Add(new Point(intX + _waveWidth / 2 + _waveWidth / 2, WaveHeight));
                intX += _waveWidth;
                if (intX > Width + _waveWidth)
                    break;
            }

            var path1 = new GraphicsPath();
            path1.AddCurve(lst1.ToArray(), 0.5F);
            path1.AddLine(Width + 1, -1, Width + 1, Height);
            path1.AddLine(Width + 1, Height, -1, Height);
            path1.AddLine(-1, Height, -1, -1);

            var path2 = new GraphicsPath();
            path2.AddCurve(lst2.ToArray(), 0.5F);
            path2.AddLine(Width + 1, -1, Width + 1, Height);
            path2.AddLine(Width + 1, Height, -1, Height);
            path2.AddLine(-1, Height, -1, -1);

            g.FillPath(new SolidBrush(Color.FromArgb(220, WaveColor.R, WaveColor.G, WaveColor.B)), path1);
            g.FillPath(new SolidBrush(Color.FromArgb(220, WaveColor.R, WaveColor.G, WaveColor.B)), path2);

            OnPainted?.Invoke(this, e);
        }
    }
}
