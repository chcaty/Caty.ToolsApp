using Caty.Tools.UxForm.Helpers;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Caty.Tools.UxForm.Controls.KeyBoard;

namespace Caty.Tools.UxForm.Controls
{
    public partial class UxTextBox : UxControlBase
    {
        private bool _mIsShowClearBtn = true;
        private int _mIntSelectionStart;
        private int _mIntSelectionLength;
        /// <summary>
        /// 功能描述:是否显示清理按钮
        /// 作　　者:HZH
        /// 创建日期:2019-02-28 16:13:52
        /// </summary>
        [Description("是否显示清理按钮"), Category("自定义")]
        public bool IsShowClearBtn
        {
            get => _mIsShowClearBtn;
            set
            {
                _mIsShowClearBtn = value;
                if (value)
                {
                    btnClear.Visible = !(txtInput.Text == "\r\n") && !string.IsNullOrEmpty(txtInput.Text);
                }
                else
                {
                    btnClear.Visible = false;
                }
            }
        }

        private bool _mIsShowSearchBtn;
        /// <summary>
        /// 是否显示查询按钮
        /// </summary>

        [Description("是否显示查询按钮"), Category("自定义")]
        public bool IsShowSearchBtn
        {
            get => _mIsShowSearchBtn;
            set
            {
                _mIsShowSearchBtn = value;
                btnSearch.Visible = value;
            }
        }

        [Description("是否显示键盘"), Category("自定义")]
        public bool IsShowKeyboard
        {
            get => btnKeyBord.Visible;
            set => btnKeyBord.Visible = value;
        }
        [Description("字体"), Category("自定义")]
        public new Font Font
        {
            get => txtInput.Font;
            set => txtInput.Font = value;
        }

        [Description("输入类型"), Category("自定义")]
        public TextInputType InputType
        {
            get => txtInput.InputType;
            set => txtInput.InputType = value;
        }

        /// <summary>
        /// 水印文字
        /// </summary>
        [Description("水印文字"), Category("自定义")]
        public string PromptText
        {
            get => txtInput.PromptText;
            set => txtInput.PromptText = value;
        }

        [Description("水印字体"), Category("自定义")]
        public Font PromptFont
        {
            get => txtInput.PromptFont;
            set => txtInput.PromptFont = value;
        }

        [Description("水印颜色"), Category("自定义")]
        public Color PromptColor
        {
            get => txtInput.PromptColor;
            set => txtInput.PromptColor = value;
        }

        /// <summary>
        /// 获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。
        /// </summary>
        [Description("获取或设置一个值，该值指示当输入类型InputType=Regex时，使用的正则表达式。")]
        public string RegexPattern
        {
            get => txtInput.RegexPattern;
            set => txtInput.RegexPattern = value;
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最大值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最大值。")]
        public decimal MaxValue
        {
            get => txtInput.MaxValue;
            set => txtInput.MaxValue = value;
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        [Description("当InputType为数字类型时，能输入的最小值。")]
        public decimal MinValue
        {
            get => txtInput.MinValue;
            set => txtInput.MinValue = value;
        }
        /// <summary>
        /// 当InputType为数字类型时，能输入的最小值
        /// </summary>
        [Description("当InputType为数字类型时，小数位数。")]
        public int DecLength
        {
            get => txtInput.DecLength;
            set => txtInput.DecLength = value;
        }

        [Description("键盘打开样式"), Category("自定义")]
        public KeyBoardType KeyBoardType { get; set; } = KeyBoardType.UxKeyBorderAll_EN;

        [Description("查询按钮点击事件"), Category("自定义")]
        public event EventHandler SearchClick;

        [Description("文本改变事件"), Category("自定义")]
        public new event EventHandler TextChanged;
        [Description("键盘按钮点击事件"), Category("自定义")]
        public event EventHandler KeyboardClick;

        [Description("文本"), Category("自定义")]
        public string InputText
        {
            get => txtInput.Text;
            set => txtInput.Text = value;
        }

        [Description("获取焦点是否变色"), Category("自定义")]
        public bool IsFocusColor { get; set; } = true;

        private Color _fillColor;
        public new Color FillColor
        {
            get => _fillColor;
            set
            {
                _fillColor = value;
                base.FillColor = value;
                txtInput.BackColor = value;
            }
        }
        public UxTextBox()
        {
            InitializeComponent();
            txtInput.SizeChanged += UxTextBox_SizeChanged;
            SizeChanged += UxTextBox_SizeChanged;
            txtInput.GotFocus += (a, b) =>
            {
                if (IsFocusColor)
                    RectColor = Color.FromArgb(78, 169, 255);
            };
            txtInput.LostFocus += (a, b) =>
            {
                if (IsFocusColor)
                    RectColor = Color.FromArgb(220, 220, 220);
            };
        }

        private void UxTextBox_SizeChanged(object sender, EventArgs e)
        {
            txtInput.Location = txtInput.Location with { Y = (Height - txtInput.Height) / 2 };
        }

        private void txtInput_TextChanged(object sender, EventArgs e)
        {
            if (_mIsShowClearBtn)
            {
                btnClear.Visible = !(txtInput.Text == "\r\n") && !string.IsNullOrEmpty(txtInput.Text);
            }
            if (TextChanged != null)
            {
                TextChanged(sender, e);
            }
        }

        private void btnClear_MouseDown(object sender, MouseEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }

        private void btnSearch_MouseDown(object sender, MouseEventArgs e)
        {
            if (SearchClick != null)
            {
                SearchClick(sender, e);
            }
        }

        private Forms.FrmAnchor _mFrmAnchor;
        private void btnKeyBoard_MouseDown(object sender, MouseEventArgs e)
        {
            _mIntSelectionStart = txtInput.SelectionStart;
            _mIntSelectionLength = txtInput.SelectionLength;
            FindForm().ActiveControl = this;
            FindForm().ActiveControl = txtInput;
            switch (KeyBoardType)
            {
                case KeyBoardType.UxKeyBorderAll_EN:
                    if (_mFrmAnchor == null)
                    {
                        if (_mFrmAnchor == null)
                        {
                            var key = new UxKeyBoardAll();
                            key.CharType = KeyBoardCharType.Char;
                            key.RetractClick += (a, b) =>
                            {
                                _mFrmAnchor.Hide();
                            };
                            _mFrmAnchor = new Forms.FrmAnchor(this, key);
                            _mFrmAnchor.VisibleChanged += (a, b) =>
                            {
                                if (!_mFrmAnchor.Visible) return;
                                txtInput.SelectionStart = _mIntSelectionStart;
                                txtInput.SelectionLength = _mIntSelectionLength;
                            };
                        }
                    }
                    break;
                case KeyBoardType.UxKeyBorderAll_Num:

                    if (_mFrmAnchor == null)
                    {
                        var key = new UxKeyBoardAll();
                        key.CharType = KeyBoardCharType.Number;
                        key.RetractClick += (a, b) =>
                        {
                            _mFrmAnchor.Hide();
                        };
                        _mFrmAnchor = new Forms.FrmAnchor(this, key);
                        _mFrmAnchor.VisibleChanged += (a, b) =>
                        {
                            if (!_mFrmAnchor.Visible) return;
                            txtInput.SelectionStart = _mIntSelectionStart;
                            txtInput.SelectionLength = _mIntSelectionLength;
                        };
                    }

                    break;
                case KeyBoardType.UxKeyBorderNum:
                    if (_mFrmAnchor == null)
                    {
                        var key = new UxKeyBoardNum();
                        _mFrmAnchor = new Forms.FrmAnchor(this, key);
                        _mFrmAnchor.VisibleChanged += (a, b) =>
                        {
                            if (!_mFrmAnchor.Visible) return;
                            txtInput.SelectionStart = _mIntSelectionStart;
                            txtInput.SelectionLength = _mIntSelectionLength;
                        };
                    }
                    break;
                case KeyBoardType.UxKeyBorderHand:

                    _mFrmAnchor = new Forms.FrmAnchor(this, new Size(504, 361));
                    _mFrmAnchor.VisibleChanged += m_frmAnchor_VisibleChanged;
                    _mFrmAnchor.Disposed += m_frmAnchor_Disposed;
                    var p = new Panel
                    {
                        Dock = DockStyle.Fill,
                        Name = "keyborder"
                    };
                    _mFrmAnchor.Controls.Add(p);

                    var btnDelete = new UxButtonBase
                    {
                        Name = "btnDelete",
                        Size = new Size(80, 28),
                        FillColor = Color.White,
                        IsRadius = false,
                        CornerRadius = 1,
                        IsShowRect = true,
                        RectColor = Color.FromArgb(189, 197, 203),
                        Location = new Point(198, 332),
                        BtnFont = new System.Drawing.Font("微软雅黑", 8),
                        BtnText = "删除"
                    };
                    btnDelete.BtnClick += (a, b) =>
                    {
                        SendKeys.Send("{BACKSPACE}");
                    };
                    _mFrmAnchor.Controls.Add(btnDelete);
                    btnDelete.BringToFront();

                    var btnEnter = new UxButtonBase
                    {
                        Name = "btnEnter",
                        Size = new Size(82, 28),
                        FillColor = Color.White,
                        IsRadius = false,
                        CornerRadius = 1,
                        IsShowRect = true,
                        RectColor = Color.FromArgb(189, 197, 203),
                        Location = new Point(278, 332),
                        BtnFont = new Font("微软雅黑", 8),
                        BtnText = "确定"
                    };
                    btnEnter.BtnClick += (a, b) =>
                    {
                        SendKeys.Send("{ENTER}");
                        _mFrmAnchor.Hide();
                    };
                    _mFrmAnchor.Controls.Add(btnEnter);
                    btnEnter.BringToFront();
                    _mFrmAnchor.VisibleChanged += (a, b) =>
                    {
                        if (!_mFrmAnchor.Visible) return;
                        txtInput.SelectionStart = _mIntSelectionStart;
                        txtInput.SelectionLength = _mIntSelectionLength;
                    };
                    break;
                case KeyBoardType.Null:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            if (!_mFrmAnchor.Visible)
                _mFrmAnchor.Show(FindForm());
            if (KeyboardClick != null)
            {
                KeyboardClick(sender, e);
            }
        }

        private void m_frmAnchor_Disposed(object sender, EventArgs e)
        {
            if (_mHandAppWin == IntPtr.Zero) return;
            if (_mHandPWin is { HasExited: false })
                _mHandPWin.Kill();
            _mHandPWin = null;
            _mHandAppWin = IntPtr.Zero;
        }


        private IntPtr _mHandAppWin;
        private Process _mHandPWin = null;
        string m_HandExeName = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "HandInput\\handinput.exe");

        private void m_frmAnchor_VisibleChanged(object sender, EventArgs e)
        {
            if (_mFrmAnchor.Visible)
            {
                var lstP = Process.GetProcessesByName("handinput");
                if (lstP.Length > 0)
                {
                    foreach (var item in lstP)
                    {
                        item.Kill();
                    }
                }
                _mHandAppWin = IntPtr.Zero;

                if (_mHandPWin == null)
                {
                    _mHandPWin = null;

                    _mHandPWin = Process.Start(m_HandExeName);
                    _mHandPWin.WaitForInputIdle();
                }
                while (_mHandPWin.MainWindowHandle == IntPtr.Zero)
                {
                    Thread.Sleep(10);
                }
                _mHandAppWin = _mHandPWin.MainWindowHandle;
                var p = _mFrmAnchor.Controls.Find("keyborder", false)[0];
                SetParent(_mHandAppWin, p.Handle);
                ControlHelper.SetForegroundWindow(FindForm().Handle);
                MoveWindow(_mHandAppWin, -111, -41, 626, 412, true);
            }
            else
            {
                if (_mHandAppWin == IntPtr.Zero) return;
                if (_mHandPWin is { HasExited: false })
                    _mHandPWin.Kill();
                _mHandPWin = null;
                _mHandAppWin = IntPtr.Zero;
            }
        }

        private void UCTextBoxEx_MouseDown(object sender, MouseEventArgs e)
        {
            ActiveControl = txtInput;
        }

        private void UCTextBoxEx_Load(object sender, EventArgs e)
        {
            if (!Enabled)
            {
                base.FillColor = Color.FromArgb(240, 240, 240);
                txtInput.BackColor = Color.FromArgb(240, 240, 240);
            }
            else
            {
                FillColor = _fillColor;
                txtInput.BackColor = _fillColor;
            }
        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);
        [DllImport("user32.dll", EntryPoint = "ShowWindow")]
        private static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, int hWndlnsertAfter, int X, int Y, int cx, int cy, uint Flags);
        private const int GWL_STYLE = -16;
        private const int WS_CHILD = 0x40000000;//设置窗口属性为child

        [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
        public static extern int GetWindowLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        public static extern int SetWindowLong(IntPtr hwnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll")]
        private static extern IntPtr SetActiveWindow(IntPtr handle);
    }
}
