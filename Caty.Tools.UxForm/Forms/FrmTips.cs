using Caty.Tools.UxForm.Helpers;
using Timer = System.Windows.Forms.Timer;

namespace Caty.Tools.UxForm.Forms
{
    public partial class FrmTips : FrmBase
    {
        public ContentAlignment ShowAlignment { get; set; } = ContentAlignment.BottomLeft;

        private static readonly List<FrmTips> Tips = new();
        private int _closeTime;
        private static KeyValuePair<string, FrmTips> _lastTips;

        public int CloseTime
        {
            get => _closeTime;
            set
            {
                _closeTime = value;
                if (value > 0)
                    timer1.Interval = value;
            }
        }

        public FrmTips()
        {
            SetStyle(ControlStyles.UserPaint,true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void FrmTips_Load(object sender, EventArgs e)
        {
            if (_closeTime <= 0) return;
            timer1.Interval = _closeTime;
            timer1.Enabled = true;
        }
        private void FrmTips_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_lastTips.Value == this)
                _lastTips = new KeyValuePair<string, FrmTips>();
            lock (Tips)
            {
                Tips.Remove(this);
            }
            ReshowTips();
            for (var i = Application.OpenForms.Count - 1; i >= 0; i--)
            {
                if (Application.OpenForms[i].IsDisposed || !Application.OpenForms[i].Visible ||
                    Application.OpenForms[i] is FrmTips)
                {
                }
                else
                {
                    var t = new Timer
                    {
                        Interval = 100
                    };
                    var frm = Application.OpenForms[i];
                    t.Tick += (a, b) =>
                    {
                        t.Enabled = false;
                        if (!frm.IsDisposed)
                            ControlHelper.SetForegroundWindow(frm.Handle);
                    };
                    t.Enabled = true;
                    break;
                }
            }
        }

        private void FrmTips_FormClosed(object sender, FormClosedEventArgs e)
        {
            Dispose();
            GC.Collect();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }

        private void btnClose_MouseDown(object sender, MouseEventArgs e)
        {
            timer1.Enabled = false;
            Close();
        }


        public void ResetTimer()
        {
            if (_closeTime <= 0) return;
            timer1.Enabled = false;
            timer1.Enabled = true;
        }

        public static FrmTips ShowTips(Form form, string message, int autoCloseTime, bool isShowCloseButton,
            ContentAlignment alignment = ContentAlignment.BottomLeft, Point? point = null,
            TipsSizeMode mode = TipsSizeMode.Small, Size? size = null, TipsState state = TipsState.Default)
        {
            if (_lastTips.Key == message + state && !_lastTips.Value.IsDisposed && _lastTips.Value.Visible)
            {
                _lastTips.Value.ResetTimer();
                return _lastTips.Value;
            }

            var frmTips = new FrmTips
            {
                Size = mode switch
                {
                    TipsSizeMode.Small => new Size(350, 35),
                    TipsSizeMode.Large => new Size(350, 50),
                    TipsSizeMode.Medium => new Size(360, 65),
                    TipsSizeMode.None => size ?? new Size(350, 35),
                    _ => throw new ArgumentOutOfRangeException(nameof(mode), mode, null)
                },

                BackColor = Color.FromArgb((int)state)
            };

            frmTips.lblMsg.ForeColor = state == TipsState.Default ? SystemColors.ControlText : Color.White;

            frmTips.pctStat.Image = state switch
            {
                TipsState.Default => Properties.Resources.alarm,
                TipsState.Success => Properties.Resources.success,
                TipsState.Info => Properties.Resources.alarm,
                TipsState.Warning => Properties.Resources.warning,
                TipsState.Error => Properties.Resources.error,
                _ => Properties.Resources.alarm
            };

            frmTips.lblMsg.Text = message;
            frmTips.CloseTime = autoCloseTime;
            frmTips.btnClose.Visible = isShowCloseButton;

            frmTips.ShowAlignment = alignment;
            frmTips.Owner = form;
            lock (Tips)
            {
                Tips.Add(frmTips);
            }
            ReshowTips();
            frmTips.Show(form);
            if (form is { IsDisposed: false })
            {
                ControlHelper.SetForegroundWindow(form.Handle);
            }

            _lastTips = new KeyValuePair<string, FrmTips>(message + state, frmTips);
            return frmTips;
        }

        /// <summary>
        /// 刷新显示
        /// </summary>
        public static void ReshowTips()
        {
            lock (Tips)
            {
                Tips.RemoveAll(p => p.IsDisposed);
                var enumerable = from p in Tips group p by new { p.ShowAlignment };
                var size = Screen.PrimaryScreen.Bounds.Size;
                foreach (var item in enumerable)
                {
                    var list = Tips.FindAll(p => p.ShowAlignment == item.Key.ShowAlignment);
                    for (var i = 0; i < list.Count; i++)
                    {
                        var frmTips = list[i];
                        if (frmTips.InvokeRequired)
                        {
                            frmTips.BeginInvoke(new MethodInvoker(delegate ()
                            {
                                frmTips.Location = item.Key.ShowAlignment switch
                                {
                                    ContentAlignment.TopLeft => new Point(10, 10 + (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.TopCenter => new Point((size.Width - frmTips.Width) / 2,
                                        10 + (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.TopRight => new Point(size.Width - frmTips.Width - 10,
                                        10 + (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.MiddleLeft => new Point(10,
                                        size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                        (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.MiddleCenter => new Point((size.Width - frmTips.Width) / 2,
                                        size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                        (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.MiddleRight => new Point(size.Width - frmTips.Width - 10,
                                        size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                        (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.BottomLeft => new Point(10,
                                        size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.BottomCenter => new Point((size.Width - frmTips.Width) / 2,
                                        size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                    ContentAlignment.BottomRight => new Point(size.Width - frmTips.Width - 10,
                                        size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                    _ => frmTips.Location
                                };
                            }));
                        }
                        else
                        {
                            frmTips.Location = item.Key.ShowAlignment switch
                            {
                                ContentAlignment.TopLeft => new Point(10, 10 + (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.TopCenter => new Point((size.Width - frmTips.Width) / 2,
                                    10 + (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.TopRight => new Point(size.Width - frmTips.Width - 10,
                                    10 + (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.MiddleLeft => new Point(10,
                                    size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                    (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.MiddleCenter => new Point((size.Width - frmTips.Width) / 2,
                                    size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                    (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.MiddleRight => new Point(size.Width - frmTips.Width - 10,
                                    size.Height - (size.Height - list.Count * (frmTips.Height + 10)) / 2 -
                                    (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.BottomLeft => new Point(10,
                                    size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.BottomCenter => new Point((size.Width - frmTips.Width) / 2,
                                    size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                ContentAlignment.BottomRight => new Point(size.Width - frmTips.Width - 10,
                                    size.Height - 100 - (i + 1) * (frmTips.Height + 10)),
                                _ => frmTips.Location
                            };
                        }
                    }
                }
            }
        }

        public static FrmTips ShowTipsSuccess(Form frm, string message)
        {
            return ShowTips(frm, message, 3000, false, ContentAlignment.BottomCenter, null, TipsSizeMode.Large, null,
                TipsState.Success);
        }

        public static FrmTips ShowTipsError(Form frm, string message)
        {
            return ShowTips(frm, message, 3000, false, ContentAlignment.BottomCenter, null, TipsSizeMode.Large, null,
                TipsState.Error);
        }

        public static FrmTips ShowTipsInfo(Form frm, string message)
        {
            return ShowTips(frm, message, 3000, false, ContentAlignment.BottomCenter, null, TipsSizeMode.Large, null,
                TipsState.Info);
        }

        public static FrmTips ShowTipsWarning(Form frm, string message)
        {
            return ShowTips(frm, message, 3000, false, ContentAlignment.BottomCenter, null, TipsSizeMode.Large, null,
                TipsState.Warning);
        }
    }
}
