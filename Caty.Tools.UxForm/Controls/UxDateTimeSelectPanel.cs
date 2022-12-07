using System.ComponentModel;
using System.Globalization;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls
{
    [ToolboxItem(false)]
    public partial class UxDateTimeSelectPanel : UserControl
    {
        [Description("确定事件"), Category("自定义")] 
        public event EventHandler? SelectedTimeEvent;
        [Description("取消事件"), Category("自定义")] 
        public event EventHandler? CancelTimeEvent;

        [Description("自动选中下一级"), Category("自定义")]
        public bool AutoSelectNext { get; set; } = true;

        private DateTime _nowTime = DateTime.Now;

        public DateTime CurrentTime
        {
            get => _nowTime;
            set
            {
                _nowTime = value;
                SetTimeToControl();
            }
        }

        private UxButtonBase _thisButton;

        [Description("时间类型"), Category("自定义")]
        public DateTimePickerType TimeType { get; set; } = DateTimePickerType.DateTime;

        public UxDateTimeSelectPanel()
        {
            InitializeComponent();
            panTime.SelectSourceEvent += panTime_SelectSourceEvent;
            TabStop = false;
        }

        public UxDateTimeSelectPanel(DateTime dt)
        {
            _nowTime = dt;
            InitializeComponent();
            panTime.SelectSourceEvent += panTime_SelectSourceEvent;
            TabStop = false;
        }

        private void panTime_SelectSourceEvent(object sender, EventArgs e)
        {
            var strKey = sender.ToString();
            if (_thisButton == btnYear)
            {
                _nowTime = (strKey + "-" + _nowTime.Month + "-" + _nowTime.Day + " " + _nowTime.Hour + ":" + _nowTime.Minute).ToDate();
            }
            else if (_thisButton == btnMonth)
            {
                _nowTime = (_nowTime.Year + "-" + strKey + "-" + _nowTime.Day + " " + _nowTime.Hour + ":" + _nowTime.Minute).ToDate();
            }
            else if (_thisButton == btnDay)
            {
                _nowTime = (_nowTime.Year + "-" + _nowTime.Month + "-" + strKey + " " + _nowTime.Hour + ":" + _nowTime.Minute).ToDate();
            }
            else if (_thisButton == btnHour)
            {
                _nowTime = (_nowTime.Year + "-" + _nowTime.Month + "-" + _nowTime.Day + " " + strKey + ":" + _nowTime.Minute).ToDate();
            }
            else if (_thisButton == btnMinute)
            {
                _nowTime = (_nowTime.Year + "-" + _nowTime.Month + "-" + _nowTime.Day + " " + _nowTime.Hour + ":" + strKey).ToDate();
            }

            SetTimeToControl();
            if (!Visible) return;
            if (!AutoSelectNext) return;
            if (_thisButton == btnYear)
            {
                SetSelectType(btnMonth);
            }
            else if (_thisButton == btnMonth)
            {
                SetSelectType(btnDay);
            }
            else if (_thisButton == btnDay)
            {
                if (TimeType == DateTimePickerType.DateTime || TimeType == DateTimePickerType.Time)
                    SetSelectType(btnHour);
            }
            else if (_thisButton == btnHour)
            {
                SetSelectType(btnMinute);
            }
        }

        private void UxDateTimePicker_Load(object sender, EventArgs e)
        {
            SetTimeToControl();

            switch (TimeType)
            {
                case DateTimePickerType.Date:
                    btnHour.Visible = false;
                    btnMinute.Visible = false;
                    break;
                case DateTimePickerType.Time:
                    btnYear.Visible = false;
                    btnMonth.Visible = false;
                    btnDay.Visible = false;
                    sp1.Visible = false;
                    sp2.Visible = false;
                    sp3.Visible = false;
                    break;
                case DateTimePickerType.DateTime:
                    break;
            }

            SetSelectType((int)TimeType <= 2 ? btnYear : btnHour);
        }

        private void SetTimeToControl()
        {
            btnYear.Tag = _nowTime.Year;
            btnYear.BtnText = _nowTime.Year + "年";
            btnMonth.Tag = _nowTime.Month;
            btnMonth.BtnText = _nowTime.Month.ToString().PadLeft(2, '0') + "月";
            btnDay.Tag = _nowTime.Day;
            btnDay.BtnText = _nowTime.Day.ToString().PadLeft(2, '0') + "日";
            btnHour.Tag = _nowTime.Hour;
            btnHour.BtnText = _nowTime.Hour.ToString().PadLeft(2, '0') + "时";
            btnMinute.Tag = _nowTime.Minute;
            btnMinute.BtnText = _nowTime.Minute.ToString().PadLeft(2, '0') + "分";
        }

        private void SetSelectType(UxButtonBase btn)
        {
            try
            {
                ControlHelper.FreezeControl(this, true);
                if (_thisButton != null)
                {
                    _thisButton.FillColor = Color.White;
                    _thisButton.BtnForeColor = Color.FromArgb(255, 77, 59);
                }

                _thisButton = btn;
                if (_thisButton == null) return;
                _thisButton.FillColor = Color.FromArgb(255, 77, 59);
                _thisButton.BtnForeColor = Color.White;

                var lstSource = new List<KeyValuePair<string, string>>();
                panTime.SuspendLayout();

                if (btn == btnYear)
                {
                    panLeft.Visible = true;
                    panRight.Visible = true;
                }
                else
                {
                    panLeft.Visible = false;
                    panRight.Visible = false;
                }

                if (btn == btnYear)
                {
                    panTime.Row = 5;
                    panTime.Column = 6;
                    var intYear = _nowTime.Year - _nowTime.Year % 30;
                    for (var i = 0; i < 30; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>((intYear + i).ToString(),
                            (intYear + i).ToString()));
                    }
                }
                else if (btn == btnMonth)
                {
                    panTime.Row = 3;
                    panTime.Column = 4;
                    for (var i = 1; i <= 12; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>(i.ToString(),
                            i.ToString().PadLeft(2, '0') + "月\r\n" + (("2019-" + i + "-01").ToDate().ToString("MMM",
                                CultureInfo.CreateSpecificCulture("en-GB")))));
                    }
                }
                else if (btn == btnDay)
                {
                    panTime.Column = 7;
                    var intDayCount = DateTime.DaysInMonth(_nowTime.Year, _nowTime.Month);
                    var intIndex = (int)(_nowTime.DayOfWeek);
                    panTime.Row = (intDayCount + intIndex) / 7 + ((intDayCount + intIndex) % 7 != 0 ? 1 : 0);
                    for (var i = 0; i < intIndex; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>("", ""));
                    }

                    for (var i = 1; i <= intDayCount; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString().PadLeft(2, '0')));
                    }
                }
                else if (btn == btnHour)
                {
                    panTime.Row = 4;
                    panTime.Column = 6;
                    for (var i = 0; i <= 24; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>(i.ToString(), i + "时"));
                    }
                }
                else if (btn == btnMinute)
                {
                    panTime.Row = 5;
                    panTime.Column = 12;
                    for (var i = 0; i <= 60; i++)
                    {
                        lstSource.Add(new KeyValuePair<string, string>(i.ToString(), i.ToString().PadLeft(2, '0')));
                    }
                }

                panTime.Source = lstSource;
                panTime.SetSelect(btn.Tag.ToStringExt());
                panTime.ResumeLayout(true);
            }
            finally
            {
                ControlHelper.FreezeControl(this, false);
            }
        }

        private void btnTime_BtnClick(object sender, EventArgs e)
        {
            SetSelectType((UxButtonBase)sender);
        }

        private void panLeft_MouseDown(object sender, MouseEventArgs e)
        {
            var lstSource = new List<KeyValuePair<string, string>>();
            var intYear = panTime.Source[0].Key.ToInt() - panTime.Source[0].Key.ToInt() % 30 - 30;
            panTime.SuspendLayout();
            panTime.Row = 5;
            panTime.Column = 6;
            for (var i = 0; i < 30; i++)
            {
                lstSource.Add(new KeyValuePair<string, string>((intYear + i).ToString(), (intYear + i).ToString()));
            }

            panTime.Source = lstSource;
            panTime.SetSelect(btnYear.Tag.ToStringExt());
            panTime.ResumeLayout(true);
        }

        private void panRight_MouseDown(object sender, MouseEventArgs e)
        {
            var lstSource = new List<KeyValuePair<string, string>>();
            var intYear = panTime.Source[0].Key.ToInt() - panTime.Source[0].Key.ToInt() % 30 + 30;
            panTime.SuspendLayout();
            panTime.Row = 5;
            panTime.Column = 6;
            for (var i = 0; i < 30; i++)
            {
                lstSource.Add(new KeyValuePair<string, string>((intYear + i).ToString(), (intYear + i).ToString()));
            }

            panTime.Source = lstSource;
            panTime.SetSelect(btnYear.Tag.ToStringExt());
            panTime.ResumeLayout(true);
        }

        private void btnOk_BtnClick(object sender, EventArgs e)
        {
            SelectedTimeEvent?.Invoke(_nowTime, null);
        }

        private void btnCancel_BtnClick(object sender, EventArgs e)
        {
            CancelTimeEvent?.Invoke(null, null);
        }
    }
}
