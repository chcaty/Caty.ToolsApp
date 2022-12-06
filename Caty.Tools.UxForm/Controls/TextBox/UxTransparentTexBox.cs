using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace Caty.Tools.UxForm.Controls.TextBox
{
    public partial class UxTransparentTexBox : UxTextBoxBase
    {
        private UxPictureBox myPictureBox;
        private bool myUpToDate = false;
        private bool myCaretUpToDate = false;
        private Bitmap myBitmap;
        private Bitmap myAlphaBitmap;

        private int myFontHeight = 10;
        private System.Windows.Forms.Timer myTimer1;

        private bool myCaretState = true;
        private bool myPaintedFirstTime = false;
        private Color myBackColor = Color.White;
        private int myBackAlpha = 10;

        private System.ComponentModel.Container components = null;

        public UxTransparentTexBox()
        {
            InitializeComponent();

            BackColor = myBackColor;

            SetStyle(ControlStyles.UserPaint, false);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            myPictureBox = new UxPictureBox();
            Controls.Add(myPictureBox);
            myPictureBox.Dock = DockStyle.Fill;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            myAlphaBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            Invalidate();
        }

        protected override void OnGiveFeedback(GiveFeedbackEventArgs gfbevent)
        {
            base.OnGiveFeedback(gfbevent);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            Point ptCursor = Cursor.Position;
            Form f = this.FindForm();
            ptCursor = f.PointToClient(ptCursor);
            if(!this.Bounds.Contains(ptCursor))
                base.OnMouseLeave(e);
        }

        protected override void OnChangeUICues(UICuesEventArgs e)
        {
            base.OnChangeUICues(e);
            myUpToDate = false;
            Invalidate();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            myCaretUpToDate = false;
            myUpToDate = false;
            Invalidate();

            myTimer1 = new System.Windows.Forms.Timer(this.components);
            myTimer1.Interval = (int)win32.GetCaretBlinkTime();

            myTimer1.Tick += new EventHandler(myTimer1_Tick);
            myTimer1.Enabled = true;
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            myCaretUpToDate = false;
            myUpToDate = false;
            Invalidate();

            myTimer1.Dispose();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            if (myPaintedFirstTime)
            {
                SetStyle(ControlStyles.UserPaint, false);
            }
            base.OnFontChanged(e);
            if (myPaintedFirstTime)
            {
                SetStyle(ControlStyles.UserPaint, true);
            }

            myFontHeight = GetFontHeight();

            myUpToDate = false;
            Invalidate();
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            myUpToDate = false;
            Invalidate();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == win32.WM_PAINT)
            {
                myPaintedFirstTime = true;
                if (!myUpToDate || !myCaretUpToDate)
                {
                    GetBitmaps();
                }
                myUpToDate = true;
                myCaretUpToDate = true;

                if (myPictureBox.Image != null)
                {
                    myPictureBox.Image.Dispose();
                }

                if (string.IsNullOrEmpty(this.Text) && !string.IsNullOrEmpty(this.PromptText))
                {
                    Bitmap bit = (Bitmap)myAlphaBitmap.Clone();
                    Graphics graphics = Graphics.FromImage(bit);
                    SizeF sizef = graphics.MeasureString(this.PromptText, this.PromptFont);
                    graphics.DrawString(this.PromptText, this.PromptFont, new SolidBrush(PromptColor), new PointF(3, (bit.Height - sizef.Height) / 2));
                    graphics.Dispose();
                    myPictureBox.Image = bit;
                }
                else
                {
                    myPictureBox.Image = (Image)myAlphaBitmap.Clone();
                }
            }
            else if (m.Msg == win32.WM_HSCROLL || m.Msg == win32.WM_VSCROLL)
            {
                myUpToDate = false;
                Invalidate();
            }
            else if (m.Msg == win32.WM_LBUTTONDOWN || m.Msg == win32.WM_RBUTTONDOWN || m.Msg == win32.WM_LBUTTONDBLCLK)
            {
                myUpToDate = false;
                Invalidate();
            }
            else if(m.Msg == win32.WM_MOUSEMOVE)
            {
                if (m.WParam.ToInt32() != 0)
                {
                    myUpToDate = false;
                    Invalidate();
                }
            }

            if (m.Msg == 15 || m.Msg == 7 || m.Msg == 8)
            {
                base.OnPaint(null);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public new BorderStyle BorderStyle
        {
            get {return base.BorderStyle; }
            set
            {
                if (this.myPaintedFirstTime)
                {
                    this.SetStyle(ControlStyles.UserPaint,false);
                }
                base.BorderStyle = value;
                if (this.myPaintedFirstTime)
                {
                    this.SetStyle(ControlStyles.UserPaint,true);
                }

                this.myBitmap = null;
                this.myAlphaBitmap = null;
                myUpToDate = false;
                Invalidate();
            }
        }

        public new Color BackColor
        {
            get { return Color.FromArgb(base.BackColor.R, base.BackColor.G, base.BackColor.B); }
            set
            {
                myBackColor = value;
                base.BackColor = value;
                myUpToDate = false;
            }
        }

        public override bool Multiline
        {
            get
            {
                return base.Multiline;
            }
            set
            {
                if (this.myPaintedFirstTime)
                {
                    this.SetStyle(ControlStyles.UserPaint,false);
                }

                base.Multiline = value;
                if (this.myPaintedFirstTime)
                {
                    this.SetStyle(ControlStyles.UserPaint,true);
                }

                this.myBitmap = null;
                this.myAlphaBitmap = null;
                myUpToDate = false;
                Invalidate();
            }
        }

        private int GetFontHeight()
        {
            Graphics graphics = this.CreateGraphics();
            SizeF sizeF = graphics.MeasureString("X", this.Font);
            graphics.Dispose();
            return (int)sizeF.Height;
        }

        private void GetBitmaps()
        {
            if (myBitmap == null && myAlphaBitmap == null || myBitmap.Width != Width || myBitmap.Height != Height ||
                myAlphaBitmap.Width != Width || myAlphaBitmap.Height != Height)
            {
                myBitmap = null;
                myAlphaBitmap = null;
            }

            if (myBitmap ==null)
            {
                myBitmap = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                myUpToDate = false;
            }

            if (!myUpToDate)
            {
                SetStyle(ControlStyles.UserPaint, false);
                win32.CaptureWindow(this, ref myBitmap);
                SetStyle(ControlStyles.UserPaint, true);
                SetStyle(ControlStyles.SupportsTransparentBackColor, true);
                BackColor = Color.FromArgb(myBackAlpha, myBackColor);
            }

            Rectangle rectangle = new Rectangle(0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height);
            ImageAttributes imageAttributes = new ImageAttributes();

            ColorMap[] colorMap = new ColorMap[1];
            colorMap[0] = new ColorMap();
            colorMap[0].OldColor = Color.FromArgb(255, myBackColor);
            colorMap[0].NewColor = Color.FromArgb(myBackAlpha, myBackColor);

            imageAttributes.SetRemapTable(colorMap);
            if (myAlphaBitmap != null)
            {
                myAlphaBitmap.Dispose();
            }

            myAlphaBitmap = new Bitmap(this.ClientRectangle.Width , this.ClientRectangle.Height);
            Graphics graphics = Graphics.FromImage(myAlphaBitmap);

            graphics.DrawImage(myBitmap, rectangle, 0, 0, this.ClientRectangle.Width, this.ClientRectangle.Height,
                GraphicsUnit.Pixel, imageAttributes);
            graphics.Dispose();

            if (this.Focused && (this.SelectionLength == 0))
            {
                Graphics temp = Graphics.FromImage(myAlphaBitmap);
                if (myCaretState)
                {
                    Point caret = this.findCaret();
                    Pen pen = new Pen(this.ForeColor, 3);
                    temp.DrawLine(pen, caret.X + 2, caret.Y + 0, caret.X + 2, caret.Y + myFontHeight);
                    temp.Dispose();
                }
            }
        }

        private Point findCaret()
        {
            Point pointCaret = new Point(0);
            int selectionStart = this.SelectionStart;
            IntPtr intPtr = new IntPtr(selectionStart);

            int i_point = win32.SendMessage(this.Handle, win32.EM_POSFROMCHAR, intPtr, IntPtr.Zero);
            pointCaret = new Point(i_point);

            if (selectionStart == 0)
            {
                pointCaret = new Point(0);
            }
            else if (selectionStart >= this.Text.Length) 

            {
                intPtr = new IntPtr(selectionStart - 1);
                i_point = win32.SendMessage(this.Handle, win32.EM_POSFROMCHAR, intPtr, IntPtr.Zero);
                pointCaret = new Point(i_point);

                Graphics graphics = this.CreateGraphics();
                string str = this.Text.Substring(this.Text.Length - 1, 1) + "X";
                SizeF sizeF = graphics.MeasureString(str, this.Font);
                SizeF xSizeF = graphics.MeasureString("X", this.Font);
                graphics.Dispose();
                if (selectionStart == this.Text.Length)
                {
                    string last = this.Text.Substring(Text.Length - 1,1);
                    if (last == "")
                    {
                        pointCaret.X = 1;
                        pointCaret.Y = pointCaret.Y + myFontHeight;
                    }
                }
            }
            return pointCaret;
        }

        private void myTimer1_Tick(object sender, EventArgs e)
        {
            myCaretState = !myCaretState;
            myCaretUpToDate = false;
            Invalidate();
        }
    }
}
