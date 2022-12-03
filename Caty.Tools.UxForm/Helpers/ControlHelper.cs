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
            var strs = strSource.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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
            string strValue,
            TextInputType inputType,
            decimal decMaxValue = default,
            decimal decMinValue = default,
            int intLength = 2,
            string strRegexPattern = null)
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

                    if (strValue.IndexOf("-") >= 0)
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
                if (inputType == TextInputType.Number || inputType == TextInputType.UnsignNumber || inputType == TextInputType.PositiveNumber)
                {
                    if (strValue.IndexOf(".") >= 0)
                    {
                        var text = strValue.Substring(strValue.IndexOf("."));
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
    }
}
