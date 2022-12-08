using System.Drawing.Drawing2D;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace Caty.Tools.UxForm.Helpers
{
    internal static class ControlHelper
    {
        /// <summary>
        /// The m LST freeze control
        /// </summary>
        private static readonly Dictionary<Control, bool> MLstFreezeControl = new();

        /// <summary>
        /// 功能描述:停止更新控件
        /// </summary>
        /// <param name="control">control</param>
        /// <param name="blnToFreeze">是否停止更新</param>
        public static void FreezeControl(Control control, bool blnToFreeze)
        {
            switch (blnToFreeze)
            {
                case true when control.IsHandleCreated && control.Visible && !control.IsDisposed &&
                               (!MLstFreezeControl.ContainsKey(control) ||
                                (MLstFreezeControl.ContainsKey(control) && MLstFreezeControl[control] == false)):
                    MLstFreezeControl[control] = true;
                    control.Disposed += control_Disposed;
                    SendMessage(control.Handle, 11, new UIntPtr(0), new IntPtr(0));
                    break;
                case false when !control.IsDisposed && MLstFreezeControl.ContainsKey(control) &&
                                MLstFreezeControl[control] == true:
                    MLstFreezeControl.Remove(control);
                    SendMessage(control.Handle, 11, new UIntPtr(1), new IntPtr(0));
                    control.Invalidate(true);
                    break;
            }
        }

        /// <summary>
        /// 功能描述:获取字符串宽度
        /// </summary>
        /// <param name="strSource">strSource</param>
        /// <param name="g">g</param>
        /// <param name="font">font</param>
        /// <returns>返回值</returns>
        public static int GetStringWidth(
            string strSource,
            System.Drawing.Graphics g,
            System.Drawing.Font font)
        {
            var strs = strSource.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var fltWidth = strs.Select(item => g.MeasureString(strSource.Replace(" ", "A"), font)).Select(sizeF => sizeF.Width).Prepend(0).Max();

            return (int)fltWidth;
        }

        /// <summary>
        /// Handles the Disposed event of the control control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
        private static void control_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (MLstFreezeControl.ContainsKey((Control)sender))
                    MLstFreezeControl.Remove((Control)sender);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// 功能描述:检查文本控件输入类型是否有效
        /// 作　　者:HZH
        /// 创建日期:2019-02-28 10:23:34
        /// 任务编号:POS
        /// </summary>
        /// <param name="strValue">值</param>
        /// <param name="inputType">控制类型</param>
        /// <param name="decMaxValue">最大值</param>
        /// <param name="decMinValue">最小值</param>
        /// <param name="intLength">小数位长度</param>
        /// <param name="strRegexPattern">正则</param>
        /// <returns>返回值</returns>
        public static bool CheckInputType(
            string? strValue,
            TextInputType inputType,
            decimal decMaxValue = default,
            decimal decMinValue = default,
            int intLength = 2,
            string? strRegexPattern = null)
        {
            bool result;
            switch (inputType)
            {
                case TextInputType.NotControl:
                    result = true;
                    return result;
                case TextInputType.UnsignNumber:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }

                    if (strValue.Contains('-'))
                    {
                        result = false;
                        return result;
                    }
                    break;
                case TextInputType.Number:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }

                    if (!Regex.IsMatch(strValue, "^-?\\d*(\\.?\\d*)?$"))
                    {
                        result = false;
                        return result;
                    }
                    break;
                case TextInputType.Integer:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }

                    if (!Regex.IsMatch(strValue, "^-?\\d*$"))
                    {
                        result = false;
                        return result;
                    }
                    break;
                case TextInputType.PositiveInteger:
                    if (string.IsNullOrEmpty(strValue))
                    {
                        result = true;
                        return result;
                    }

                    if (!Regex.IsMatch(strValue, "^\\d+$"))
                    {
                        result = false;
                        return result;
                    }
                    break;
                case TextInputType.Regex:
                    result = (string.IsNullOrEmpty(strRegexPattern) || Regex.IsMatch(strValue, strRegexPattern));
                    return result;
            }
            if (strValue == "-")
            {
                return true;
            }

            if (!decimal.TryParse(strValue, out var d))
            {
                result = false;
            }
            else if (d < decMinValue || d > decMaxValue)
            {
                result = false;
            }
            else
            {
                if (inputType is TextInputType.Number or TextInputType.UnsignNumber or TextInputType.PositiveNumber)
                {
                    if (strValue.Contains('.'))
                    {
                        var text = strValue[strValue.IndexOf(".", StringComparison.Ordinal)..];
                        if (text.Length > intLength + 1)
                        {
                            result = false;
                            return result;
                        }
                    }
                }
                result = true;
            }
            return result;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        #region 动画特效
        [DllImport("user32.dll")]
        public static extern bool AnimateWindow(IntPtr whnd, int dwtime, int dwflag);
        //dwflag的取值如下
        public const int AW_HOR_POSITIVE = 0x00000001;
        //从左到右显示
        public const int AW_HOR_NEGATIVE = 0x00000002;
        //从右到左显示
        public const int AW_VER_POSITIVE = 0x00000004;
        //从上到下显示
        public const int AW_VER_NEGATIVE = 0x00000008;
        //从下到上显示
        public const int AW_CENTER = 0x00000010;
        //若使用了AW_HIDE标志，则使窗口向内重叠，即收缩窗口；否则使窗口向外扩展，即展开窗口
        public const int AW_HIDE = 0x00010000;
        //隐藏窗口，缺省则显示窗口
        public const int AW_ACTIVATE = 0x00020000;
        //激活窗口。在使用了AW_HIDE标志后不能使用这个标志
        public const int AW_SLIDE = 0x00040000;
        //使用滑动类型。缺省则为滚动动画类型。当使用AW_CENTER标志时，这个标志就被忽略
        public const int AW_BLEND = 0x00080000;
        //透明度从高到低
        #endregion
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        /// <summary>
        /// 设置GDI高质量模式抗锯齿
        /// </summary>
        /// <param name="g">The g.</param>
        public static void SetGDIHigh(this Graphics g)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;  //使绘图质量最高，即消除锯齿
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;
        }

        /// <summary>
        /// 根据矩形和圆得到一个圆角矩形Path
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this Rectangle rect, int cornerRadius)
        {
            var roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// Creates the rounded rectangle path.
        /// </summary>
        /// <param name="rect">The rect.</param>
        /// <param name="cornerRadius">The corner radius.</param>
        /// <returns>GraphicsPath.</returns>
        public static GraphicsPath CreateRoundedRectanglePath(this RectangleF rect, int cornerRadius)
        {
            var roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        }

        /// <summary>
        /// Gets the colors.
        /// </summary>
        /// <value>The colors.</value>
        public static Color[] Colors { get; }

        static ControlHelper()
        {
            var list = new List<Color>
            {
                Color.FromArgb(55, 162, 218),
                Color.FromArgb(50, 197, 233),
                Color.FromArgb(103, 224, 227),
                Color.FromArgb(159, 230, 184),
                Color.FromArgb(255, 219, 92),
                Color.FromArgb(255, 159, 127),
                Color.FromArgb(251, 114, 147),
                Color.FromArgb(224, 98, 174),
                Color.FromArgb(230, 144, 209),
                Color.FromArgb(231, 188, 243),
                Color.FromArgb(157, 150, 245),
                Color.FromArgb(131, 120, 234),
                Color.FromArgb(150, 191, 255),
                Color.FromArgb(243, 67, 54),
                Color.FromArgb(156, 39, 176),
                Color.FromArgb(103, 58, 183),
                Color.FromArgb(63, 81, 181),
                Color.FromArgb(33, 150, 243),
                Color.FromArgb(0, 188, 211),
                Color.FromArgb(3, 169, 244),
                Color.FromArgb(0, 150, 136),
                Color.FromArgb(139, 195, 74),
                Color.FromArgb(76, 175, 80),
                Color.FromArgb(204, 219, 57),
                Color.FromArgb(233, 30, 99),
                Color.FromArgb(254, 234, 59),
                Color.FromArgb(254, 192, 7),
                Color.FromArgb(254, 152, 0),
                Color.FromArgb(255, 87, 34),
                Color.FromArgb(121, 85, 72),
                Color.FromArgb(158, 158, 158),
                Color.FromArgb(96, 125, 139),
                Color.FromArgb(252, 117, 85),
                Color.FromArgb(172, 113, 191),
                Color.FromArgb(115, 131, 253),
                Color.FromArgb(78, 206, 255),
                Color.FromArgb(121, 195, 82),
                Color.FromArgb(255, 163, 28),
                Color.FromArgb(255, 185, 15),
                Color.FromArgb(255, 181, 197),
                Color.FromArgb(255, 110, 180),
                Color.FromArgb(255, 69, 0),
                Color.FromArgb(255, 48, 48),
                Color.FromArgb(154, 205, 50),
                Color.FromArgb(155, 205, 155),
                Color.FromArgb(154, 50, 205),
                Color.FromArgb(131, 111, 255),
                Color.FromArgb(124, 205, 124),
                Color.FromArgb(0, 206, 209),
                Color.FromArgb(0, 178, 238),
                Color.FromArgb(56, 142, 142)
            };

            var typeFromHandle = typeof(Color);
            var properties = typeFromHandle.GetProperties();
            list.AddRange(from propertyInfo in properties where propertyInfo.PropertyType == typeof(Color) && (propertyInfo.Name.StartsWith("Dark") || propertyInfo.Name.StartsWith("Medium")) select propertyInfo.GetValue(null, null) into value select (Color)value);
            Colors = list.ToArray();
        }

        /// <summary>
        /// 相对于屏幕显示的位置
        /// </summary>
        /// <param name="screen">窗体需要显示的屏幕</param>
        /// <param name="left">left</param>
        /// <param name="top">top</param>
        /// <returns></returns>
        public static Point GetScreenLocation(Screen screen, int left, int top)
        {
            return new Point(screen.Bounds.Left + left, screen.Bounds.Top + top);
        }
    }
}
