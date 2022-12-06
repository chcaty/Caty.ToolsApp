using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Caty.Tools.UxForm.Controls.TextBox
{
    public class UxPictureBox: PictureBox
    {
        public UxPictureBox()
        {
            SetStyle(ControlStyles.Selectable,false);
            SetStyle(ControlStyles.UserPaint,true);
            SetStyle(ControlStyles.AllPaintingInWmPaint,true);
            SetStyle(ControlStyles.DoubleBuffer,true);

            Cursor = null;
            Enabled = true;
            SizeMode = PictureBoxSizeMode.Normal;
        }

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case win32.WM_LBUTTONDOWN or win32.WM_RBUTTONDOWN or win32.WM_LBUTTONDBLCLK or win32.WM_MOUSELEAVE or win32.WM_MOUSEMOVE:
                    win32.PostMessage(Parent.Handle, (uint)m.Msg, m.WParam, m.LParam);
                    break;
                case win32.WM_LBUTTONUP:
                    Parent.Invalidate();
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
