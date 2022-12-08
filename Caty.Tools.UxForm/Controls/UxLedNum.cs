using System.ComponentModel;
using System.Drawing.Drawing2D;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    public class UxLedNum : UserControl
    {
        /// <summary>
        /// The m draw rect
        /// </summary>
        private Rectangle _drawRect = Rectangle.Empty;

        /// <summary>
        /// The m nums
        /// </summary>
        private static readonly Dictionary<char, int[]> NumDic = new();

        /// <summary>
        /// Initializes static members of the <see cref="UxLedNum" /> class.
        /// </summary>
        static UxLedNum()
        {
            NumDic['0'] = new[] { 1, 2, 3, 4, 5, 6 };
            NumDic['1'] = new[] { 2, 3 };
            NumDic['2'] = new[] { 1, 2, 5, 4, 7 };
            NumDic['3'] = new[] { 1, 2, 7, 3, 4 };
            NumDic['4'] = new[] { 2, 3, 6, 7 };
            NumDic['5'] = new[] { 1, 6, 7, 3, 4 };
            NumDic['6'] = new[] { 1, 6, 5, 4, 3, 7 };
            NumDic['7'] = new[] { 1, 2, 3 };
            NumDic['8'] = new[] { 1, 2, 3, 4, 5, 6, 7 };
            NumDic['9'] = new[] { 1, 2, 3, 4, 7, 6 };
            NumDic['A'] = new[] { 1, 2, 3, 5, 6, 7 };
            NumDic['b'] = new[] { 3, 4, 5, 6, 7 };
            NumDic['C'] = new[] { 1, 6, 5, 4 };
            NumDic['c'] = new[] { 7, 5, 4 };
            NumDic['d'] = new[] { 2, 3, 4, 5, 7 };
            NumDic['E'] = new[] { 1, 4, 5, 6, 7 };
            NumDic['F'] = new[] { 1, 5, 6, 7 };
            NumDic['H'] = new[] { 2, 3, 5, 6, 7 };
            NumDic['h'] = new[] { 3, 5, 6, 7 };
            NumDic['J'] = new[] { 2, 3, 4 };
            NumDic['L'] = new[] { 4, 5, 6 };
            NumDic['o'] = new[] { 3, 4, 5, 7 };
            NumDic['P'] = new[] { 1, 2, 5, 6, 7 };
            NumDic['r'] = new[] { 5, 7 };
            NumDic['U'] = new[] { 2, 3, 4, 5, 6 };
            NumDic['-'] = new[] { 7 };
            NumDic[':'] = Array.Empty<int>();
            NumDic['.'] = Array.Empty<int>();
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="UxLedNum" /> class.
        /// </summary>
        public UxLedNum()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.Selectable, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SetStyle(ControlStyles.UserPaint, true);
            SizeChanged += LEDNum_SizeChanged;
            AutoScaleMode = AutoScaleMode.None;
            Size = new Size(40, 70);
            if (_drawRect == Rectangle.Empty)
                _drawRect = new Rectangle(1, 1, Width - 2, Height - 2);
        }

        /// <summary>
        /// Handles the SizeChanged event of the LEDNum control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private void LEDNum_SizeChanged(object? sender, EventArgs e)
        {
            _drawRect = new Rectangle(1, 1, Width - 2, Height - 2);
        }

        /// <summary>
        /// The m value
        /// </summary>
        private char _value = '0';

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        [Description("值"), Category("自定义")]
        public char Value
        {
            get => _value;
            set
            {
                if (!NumDic.ContainsKey(value))
                {
                    return;
                }

                if (_value == value) return;
                _value = value;
                Refresh();
            }
        }

        /// <summary>
        /// Sets the character value.
        /// </summary>
        /// <value>The character value.</value>
        public LEDNumChar ValueChar
        {
            get => (LEDNumChar)_value;
            set => Value = (char)value;
        }

        /// <summary>
        /// The m line width
        /// </summary>
        private int _lineWidth = 8;

        /// <summary>
        /// Gets or sets the width of the line.
        /// </summary>
        /// <value>The width of the line.</value>
        [Description("线宽度，为了更好的显示效果，请使用偶数"), Category("自定义")]
        public int LineWidth
        {
            get => _lineWidth;
            set
            {
                _lineWidth = value;
                Refresh();
            }
        }

        /// <summary>
        /// 获取或设置控件的前景色。
        /// </summary>
        /// <value>The color of the fore.</value>
        /// <PermissionSet>
        ///   <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
        /// </PermissionSet>
        [Description("颜色"), Category("自定义")]
        public override Color ForeColor
        {
            get => base.ForeColor;
            set => base.ForeColor = value;
        }

        /// <summary>
        /// 引发 <see cref="E:System.Windows.Forms.Control.Paint" /> 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 <see cref="T:System.Windows.Forms.PaintEventArgs" />。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SetGDIHigh();
            switch (_value)
            {
                case '.':
                {
                    var r2 = new Rectangle(_drawRect.Left + (_drawRect.Width - _lineWidth) / 2,
                        _drawRect.Bottom - _lineWidth * 2, _lineWidth, _lineWidth);
                    e.Graphics.FillRectangle(new SolidBrush(ForeColor), r2);
                    break;
                }
                case ':':
                {
                    var r1 = new Rectangle(_drawRect.Left + (_drawRect.Width - _lineWidth) / 2,
                        _drawRect.Top + (_drawRect.Height / 2 - _lineWidth) / 2, _lineWidth, _lineWidth);
                    e.Graphics.FillRectangle(new SolidBrush(ForeColor), r1);
                    var r2 = new Rectangle(_drawRect.Left + (_drawRect.Width - _lineWidth) / 2,
                        _drawRect.Top + (_drawRect.Height / 2 - _lineWidth) / 2 + _drawRect.Height / 2, _lineWidth,
                        _lineWidth);
                    e.Graphics.FillRectangle(new SolidBrush(ForeColor), r2);
                    break;
                }
                default:
                {
                    var vs = NumDic[_value];
                    if (vs.Contains(1))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Left + 2, _drawRect.Top),
                            new Point(_drawRect.Right - 2, _drawRect.Top),
                            new Point(_drawRect.Right - _lineWidth - 2, _drawRect.Top + _lineWidth),
                            new Point(_drawRect.Left + _lineWidth + 2, _drawRect.Top + _lineWidth),
                            new Point(_drawRect.Left + 2, _drawRect.Top)
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    if (vs.Contains(2))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Right, _drawRect.Top),
                            new Point(_drawRect.Right, _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Right - _lineWidth / 2,
                                _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2 + _lineWidth / 2),
                            new Point(_drawRect.Right - _lineWidth,
                                _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Right - _lineWidth, _drawRect.Top + _lineWidth),
                            new Point(_drawRect.Right, _drawRect.Top)
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    if (vs.Contains(3))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Right, _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Right, _drawRect.Bottom),
                            new Point(_drawRect.Right - _lineWidth, _drawRect.Bottom - _lineWidth),
                            new Point(_drawRect.Right - _lineWidth,
                                _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Right - _lineWidth / 2,
                                _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2 - _lineWidth / 2),
                            new Point(_drawRect.Right, _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    if (vs.Contains(4))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Left + 2, _drawRect.Bottom),
                            new Point(_drawRect.Right - 2, _drawRect.Bottom),
                            new Point(_drawRect.Right - _lineWidth - 2, _drawRect.Bottom - _lineWidth),
                            new Point(_drawRect.Left + _lineWidth + 2, _drawRect.Bottom - _lineWidth),
                            new Point(_drawRect.Left + 2, _drawRect.Bottom)
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    if (vs.Contains(5))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Left, _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Left, _drawRect.Bottom),
                            new Point(_drawRect.Left + _lineWidth, _drawRect.Bottom - _lineWidth),
                            new Point(_drawRect.Left + _lineWidth,
                                _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Left + _lineWidth / 2,
                                _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2 - _lineWidth / 2),
                            new Point(_drawRect.Left, _drawRect.Bottom - (_drawRect.Height - _lineWidth - 4) / 2),
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }


                    if (vs.Contains(6))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Left, _drawRect.Top),
                            new Point(_drawRect.Left, _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Left + _lineWidth / 2,
                                _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2 + _lineWidth / 2),
                            new Point(_drawRect.Left + _lineWidth,
                                _drawRect.Top + (_drawRect.Height - _lineWidth - 4) / 2),
                            new Point(_drawRect.Left + _lineWidth, _drawRect.Top + _lineWidth),
                            new Point(_drawRect.Left, _drawRect.Top)
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    if (vs.Contains(7))
                    {
                        var path = new GraphicsPath();
                        path.AddLines(new[]
                        {
                            new Point(_drawRect.Left + _lineWidth / 2, _drawRect.Height / 2 + 1),
                            new Point(_drawRect.Left + _lineWidth, _drawRect.Height / 2 - _lineWidth / 2 + 1),
                            new Point(_drawRect.Right - _lineWidth, _drawRect.Height / 2 - _lineWidth / 2 + 1),
                            new Point(_drawRect.Right - _lineWidth / 2, _drawRect.Height / 2 + 1),
                            new Point(_drawRect.Right - _lineWidth, _drawRect.Height / 2 + _lineWidth / 2 + 1),
                            new Point(_drawRect.Left + _lineWidth, _drawRect.Height / 2 + _lineWidth / 2 + 1),
                            new Point(_drawRect.Left + _lineWidth / 2, _drawRect.Height / 2 + 1)
                        });
                        path.CloseAllFigures();
                        e.Graphics.FillPath(new SolidBrush(ForeColor), path);
                    }

                    break;
                }
            }
        }
    }

    /// <summary>Enum LEDNumChar</summary>
    public enum LEDNumChar : int
    {
        /// <summary>
        /// The character 0
        /// </summary>
        CHAR_0 = (int)'0',
        /// <summary>
        /// The character 1
        /// </summary>
        CHAR_1 = (int)'1',
        /// <summary>
        /// The character 2
        /// </summary>
        CHAR_2 = (int)'2',
        /// <summary>
        /// The character 3
        /// </summary>
        CHAR_3 = (int)'3',
        /// <summary>
        /// The character 4
        /// </summary>
        CHAR_4 = (int)'4',
        CHAR_5 = (int)'5',
        /// <summary>
        /// The character 6
        /// </summary>
        CHAR_6 = (int)'6',
        /// <summary>
        /// The character 7
        /// </summary>
        CHAR_7 = (int)'7',
        /// <summary>
        /// The character 8
        /// </summary>
        CHAR_8 = (int)'8',
        /// <summary>
        /// The character 9
        /// </summary>
        CHAR_9 = (int)'9',
        /// <summary>
        /// The character a
        /// </summary>
        CHAR_A = (int)'A',
        /// <summary>
        /// The character b
        /// </summary>
        CHAR_b = (int)'b',
        /// <summary>
        /// The character c
        /// </summary>
        CHAR_C = (int)'C',
        /// <summary>
        /// The character c
        /// </summary>
        CHAR_c = (int)'c',
        /// <summary>
        /// The character d
        /// </summary>
        CHAR_d = (int)'d',
        /// <summary>
        /// The character e
        /// </summary>
        CHAR_E = (int)'E',
        /// <summary>
        /// The character f
        /// </summary>
        CHAR_F = (int)'F',
        /// <summary>
        /// The character h
        /// </summary>
        CHAR_H = (int)'H',
        /// <summary>
        /// The character h
        /// </summary>
        CHAR_h = (int)'h',
        /// <summary>
        /// The character j
        /// </summary>
        CHAR_J = (int)'J',
        /// <summary>
        /// The character l
        /// </summary>
        CHAR_L = (int)'L',
        /// <summary>
        /// The character o
        /// </summary>
        CHAR_o = (int)'o',
        /// <summary>
        /// The character p
        /// </summary>
        CHAR_P = (int)'P',
        /// <summary>
        /// The character r
        /// </summary>
        CHAR_r = (int)'r',
        /// <summary>
        /// The character u
        /// </summary>
        CHAR_U = (int)'U',
        /// <summary>
        /// The character horizontal line
        /// </summary>
        CHAR_HorizontalLine = (int)'-',
        /// <summary>
        /// The character colon
        /// </summary>
        CHAR_Colon = (int)':',
        /// <summary>
        /// The character dot
        /// </summary>
        CHAR_Dot = (int)'.',
    }
}
