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
