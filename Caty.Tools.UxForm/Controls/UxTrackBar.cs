using System.ComponentModel;
using System.Globalization;
using Caty.Tools.UxForm.Forms;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    [DefaultEvent("ValueChanged")]
    public class UxTrackBar : Control
    {
        /// <summary>
        /// Occurs when [value changed].
        /// </summary>
        [Description("值改变事件"), Category("自定义")]
        public event EventHandler ValueChanged;

        /// <summary>
        /// Gets or sets the decimal digits.
        /// </summary>
        /// <value>The decimal digits.</value>
        [Description("值小数精确位数"), Category("自定义")]
        public int DecimalDigits { get; set; }


        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        [Description("线宽度"), Category("自定义")]
        public float LineWidth { get; set; } = 10;

        /// <summary>
        /// The minimum value
        /// </summary>
        private float _minValue;

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>The minimum value.</value>
        [Description("最小值"), Category("自定义")]
        public float MinValue
        {
            get => _minValue;
            set
            {
                if (_minValue > _value)
                    return;
                _minValue = value;
                Refresh();
            }
        }

        /// <summary>
        /// The maximum value
        /// </summary>
        private float _maxValue = 100;

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        /// <value>The maximum value.</value>
        [Description("最大值"), Category("自定义")]
        public float MaxValue
        {
            get => _maxValue;
            set
            {
                if (value < _value)
                    return;
                _maxValue = value;
                Refresh();
            }
        }

        /// <summary>
        /// The m value
        /// </summary>
        private float _value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Description("值"), Category("自定义")]
        public float Value
        {
            get => _value;
            set
            {
                if (value > _maxValue || value < _minValue)
                    return;
                var v = (float)Math.Round(value, DecimalDigits);
                if (_value == v)
                    return;
                _value = v;
                Refresh();
                ValueChanged?.Invoke(this, null);
            }
        }

        /// <summary>
        /// The m line color
        /// </summary>
        private Color _lineColor = Color.FromArgb(228, 231, 237);

        /// <summary>
        /// Gets or sets the color of the line.
        /// </summary>
        /// <value>The color of the line.</value>
        [Description("线颜色"), Category("自定义")]
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
        /// The m value color
        /// </summary>
        private Color _valueColor = Color.FromArgb(255, 77, 59);

        /// <summary>
        /// Gets or sets the color of the value.
        /// </summary>
        /// <value>The color of the value.</value>
        [Description("值颜色"), Category("自定义")]
        public Color ValueColor
        {
            get => _valueColor;
            set
            {
                _valueColor = value;
                Refresh();
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is show tips.
        /// </summary>
        /// <value><c>true</c> if this instance is show tips; otherwise, <c>false</c>.</value>
        [Description("点击滑动时是否显示数值提示"), Category("自定义")]
        public bool IsShowTips { get; set; } = true;

        /// <summary>
        /// Gets or sets the tips format.
        /// </summary>
        /// <value>The tips format.</value>
        [Description("显示数值提示的格式化形式"), Category("自定义")]
        public string TipsFormat { get; set; }

        /// <summary>
        /// The m line rectangle
        /// </summary>
        private RectangleF _lineRectangle;

        /// <summary>
        /// The m track rectangle
        /// </summary>
        private RectangleF _trackRectangle;

        /// <summary>
        /// Initializes a new instance of the <see cref="UxTrackBar" /> class.
        /// </summary>
        public UxTrackBar()
        {
            Size = new Size(250, 30);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            MouseDown += UxTrackBar_MouseDown;
            MouseMove += UxTrackBar_MouseMove;
            MouseUp += UxTrackBar_MouseUp;

        }



        /// <summary>
        /// The BLN down
        /// </summary>
        private bool _blnDown;

        /// <summary>
        /// Handles the MouseDown event of the UCTrackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void UxTrackBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (!_lineRectangle.Contains(e.Location) && !_trackRectangle.Contains(e.Location)) return;
            _blnDown = true;
            Value = _minValue + (e.Location.X / (float)Width) * (_maxValue - _minValue);
            ShowTips();
        }

        /// <summary>
        /// Handles the MouseMove event of the UCTrackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void UxTrackBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_blnDown) return;
            Value = _minValue + (e.Location.X / (float)Width) * (_maxValue - _minValue);
            ShowTips();
        }

        /// <summary>
        /// Handles the MouseUp event of the UCTrackBar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="MouseEventArgs" /> instance containing the event data.</param>
        private void UxTrackBar_MouseUp(object sender, MouseEventArgs e)
        {
            _blnDown = false;

            if (_frmTips is not { IsDisposed: false }) return;
            _frmTips.Close();
            _frmTips = null;
        }

        /// <summary>
        /// The FRM tips
        /// </summary>
        private FrmAnchorTips _frmTips;

        /// <summary>
        /// Shows the tips.
        /// </summary>
        private void ShowTips()
        {
            if (!IsShowTips) return;
            var strValue = Value.ToString(CultureInfo.InvariantCulture);
            if (!string.IsNullOrEmpty(TipsFormat))
            {
                try
                {
                    strValue = Value.ToString(TipsFormat);
                }
                catch
                {
                    // ignored
                }
            }

            var p = PointToScreen(new Point((int)_trackRectangle.X, (int)_trackRectangle.Y));

            if (_frmTips == null || _frmTips.IsDisposed || !_frmTips.Visible)
            {
                _frmTips = FrmAnchorTips.ShowTips(
                    new Rectangle(p.X, p.Y, (int)_trackRectangle.Width, (int)_trackRectangle.Height), strValue,
                    AnchorTipsLocation.Top, ValueColor, autoCloseTime: -1);
            }
            else
            {
                _frmTips.RectControl = new Rectangle(p.X, p.Y, (int)_trackRectangle.Width, (int)_trackRectangle.Height);
                _frmTips.Message = strValue;
            }
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
            _lineRectangle = new RectangleF(LineWidth, (Size.Height - LineWidth) / 2, Size.Width - LineWidth * 2,
                LineWidth);
            var pathLine = _lineRectangle.CreateRoundedRectanglePath(5);
            g.FillPath(new SolidBrush(_lineColor), pathLine);


            var valueLine =
                new RectangleF(LineWidth, (Size.Height - LineWidth) / 2,
                        ((_value - _minValue) / (_maxValue - _minValue)) * _lineRectangle.Width, LineWidth)
                    .CreateRoundedRectanglePath(5);
            g.FillPath(new SolidBrush(_valueColor), valueLine);

            _trackRectangle =
                new RectangleF(
                    _lineRectangle.Left - LineWidth +
                    (((_value - _minValue) / (_maxValue - _minValue)) * (Size.Width - LineWidth * 2)),
                    (Size.Height - LineWidth * 2) / 2, LineWidth * 2, LineWidth * 2);
            g.FillEllipse(new SolidBrush(_valueColor), _trackRectangle);
            g.FillEllipse(Brushes.White,
                new RectangleF(_trackRectangle.X + _trackRectangle.Width / 4,
                    _trackRectangle.Y + _trackRectangle.Height / 4, _trackRectangle.Width / 2,
                    _trackRectangle.Height / 2));
        }
    }
}
