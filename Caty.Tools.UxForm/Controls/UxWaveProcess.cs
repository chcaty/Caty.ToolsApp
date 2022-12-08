using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxWaveProcess : UxControlBase
    {
        /// <summary>
        /// The m is rectangle
        /// </summary>
        private bool _isRectangle;
        /// <summary>
        /// Gets or sets a value indicating whether this instance is rectangle.
        /// </summary>
        /// <value><c>true</c> if this instance is rectangle; otherwise, <c>false</c>.</value>
        [Description("是否矩形"), Category("自定义")]
        public bool IsRectangle
        {
            get => _isRectangle;
            set
            {
                _isRectangle = value;
                CornerRadius = value ? 10 : Math.Min(Width, Height);
            }
        }

        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        [Description("值变更事件"), Category("自定义")]
        public event EventHandler? ValueChanged;
        /// <summary>
        /// The m value
        /// </summary>
        private int _value;
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
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
                uxWave1.Height = (int)(_value / (double)_maxValue * Height) + uxWave1.WaveHeight;
                Refresh();
            }
        }

        /// <summary>
        /// The m maximum value
        /// </summary>
        private int _maxValue = 100;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
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

        /// <summary>
        /// Gets or sets the color of the value.
        /// </summary>
        /// <value>The color of the value.</value>
        [Description("值颜色"), Category("自定义")]
        public Color ValueColor
        {
            get => uxWave1.WaveColor;
            set => uxWave1.WaveColor = value;
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        /// <value>The width of the rect.</value>
        [Description("边框宽度"), Category("自定义")]
        public override int RectWidth
        {
            get => base.RectWidth;
            set => base.RectWidth = value < 4 ? 4 : value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UxWaveProcess" /> class.
        /// </summary>
        public UxWaveProcess()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            IsRadius = true;
            IsShowRect = false;
            RectWidth = 4;
            RectColor = Color.White;
            ForeColor = Color.White;
            uxWave1.Height = (int)(_value / (double)_maxValue * Height) + uxWave1.WaveHeight;
            SizeChanged += UxProcessWave_SizeChanged;
            uxWave1.OnPainted += UxWave1_Painted;
            CornerRadius = Math.Min(Width, Height);
        }

        /// <summary>
        /// Handles the Painted event of the ucWave1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        private void UxWave1_Painted(object sender, PaintEventArgs e)
        {
            e.Graphics.SetGDIHigh();
            if (IsShowRect)
            {
                if (_isRectangle)
                {
                    var rectColor = RectColor;
                    var pen = new Pen(rectColor, RectWidth);
                    var clientRectangle = new Rectangle(0, uxWave1.Height - Height, Width, Height);
                    var graphicsPath = new GraphicsPath();
                    graphicsPath.AddArc(clientRectangle.X, clientRectangle.Y, 10, 10, 180f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - 10 - 1, clientRectangle.Y, 10, 10, 270f, 90f);
                    graphicsPath.AddArc(clientRectangle.Width - 10 - 1, clientRectangle.Bottom - 10 - 1, 10, 10, 0f, 90f);
                    graphicsPath.AddArc(clientRectangle.X, clientRectangle.Bottom - 10 - 1, 10, 10, 90f, 90f);
                    graphicsPath.CloseFigure();
                    e.Graphics.DrawPath(pen, graphicsPath);
                }
                else
                {
                    var solidBrush = new SolidBrush(RectColor);
                    e.Graphics.DrawEllipse(new Pen(solidBrush, RectWidth), new Rectangle(0, uxWave1.Height - Height, Width, Height));
                }
            }

            if (!_isRectangle)
            {
                //这里曲线救国，因为设置了控件区域导致的毛边，通过画一个没有毛边的圆遮挡
                var solidBrush1 = new SolidBrush(RectColor);
                e.Graphics.DrawEllipse(new Pen(solidBrush1, 2), new Rectangle(-1, uxWave1.Height - Height - 1, Width + 2, Height + 2));
            }
            var strValue = (_value / (double)_maxValue).ToString("0.%");
            var sizeF = e.Graphics.MeasureString(strValue, Font);
            e.Graphics.DrawString(strValue, Font, new SolidBrush(ForeColor), new PointF((Width - sizeF.Width) / 2, (uxWave1.Height - Height) + (Height - sizeF.Height) / 2));
        }

        /// <summary>
        /// Handles the SizeChanged event of the UCProcessWave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void UxProcessWave_SizeChanged(object sender, EventArgs e)
        {
            if (_isRectangle) return;
            CornerRadius = Math.Min(Width, Height);
            if (Width != Height)
            {
                Size = new Size(Math.Min(Width, Height), Math.Min(Width, Height));
            }
        }

        /// <summary>
        /// Handles the <see cref="E:Paint" /> event.
        /// </summary>
        /// <param name="e">The <see cref="PaintEventArgs" /> instance containing the event data.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetGDIHigh();
            if (!_isRectangle)
            {
                //这里曲线救国，因为设置了控件区域导致的毛边，通过画一个没有毛边的圆遮挡
                var solidBrush = new SolidBrush(RectColor);
                e.Graphics.DrawEllipse(new Pen(solidBrush, 2), new Rectangle(-1, -1, Width + 2, Height + 2));
            }
            var strValue = (_value / (double)_maxValue).ToString("0.%");
            var sizeF = e.Graphics.MeasureString(strValue, Font);
            e.Graphics.DrawString(strValue, Font, new SolidBrush(ForeColor), new PointF((Width - sizeF.Width) / 2, (Height - sizeF.Height) / 2 + 1));

        }
    }
}
