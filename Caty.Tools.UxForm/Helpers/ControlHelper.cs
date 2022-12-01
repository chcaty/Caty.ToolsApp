using System.Runtime.InteropServices;

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
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint wMsg, UIntPtr wParam, IntPtr lParam);
    }
}
