using System.ComponentModel;
using Caty.Tools.UxForm.Controls.TextBox;
using Caty.Tools.UxForm.Forms;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls;

public partial class UxDatePicker : UxControlBase
{
    private FrmAnchor _frmAnchor;

    private UxDateTimeSelectPanel? _selectPanel;
    private DateTimePickerType _type = DateTimePickerType.DateTime;

    [Description("时间类型"), Category("自定义")]
    public DateTimePickerType TimeType
    {
        get => _type;
        set
        {
            _type = value;
            switch (value)
            {
                case DateTimePickerType.DateTime:
                    txtYear.Visible = true;
                    label1.Visible = true;
                    txtMonth.Visible = true;
                    label2.Visible = true;
                    txtDay.Visible = true;
                    label3.Visible = true;
                    txtHour.Visible = true;
                    label4.Visible = true;
                    txtMinute.Visible = true;
                    label5.Visible = true;
                    break;
                case DateTimePickerType.Date:
                    txtYear.Visible = true;
                    label1.Visible = true;
                    txtMonth.Visible = true;
                    label2.Visible = true;
                    txtDay.Visible = true;
                    label3.Visible = true;
                    txtHour.Visible = false;
                    label4.Visible = false;
                    txtMinute.Visible = false;
                    label5.Visible = false;
                    break;
                default:
                    txtYear.Visible = false;
                    label1.Visible = false;
                    txtMonth.Visible = false;
                    label2.Visible = false;
                    txtDay.Visible = false;
                    label3.Visible = false;
                    txtHour.Visible = true;
                    label4.Visible = true;
                    txtMinute.Visible = true;
                    label5.Visible = true;
                    break;
            }
        }
    }

    private DateTime _currentTime = DateTime.Now;

    private int _timeFontSize = 20;

    [Description("时间字体大小"), Category("自定义")]
    public int TimeFontSize
    {
        get => _timeFontSize;
        set
        {
            if (_timeFontSize == value) return;
            _timeFontSize = value;
            foreach (Control c in panel1.Controls)
            {
                c.Font = new Font(c.Font.Name, value);
            }
        }
    }

    [Description("时间"), Category("自定义")]
    public DateTime CurrentTime
    {
        get => _currentTime;
        set
        {
            _currentTime = value;
            SetTimeToControl();
        }
    }

    private void SetTimeToControl()
    {
        txtYear.Text = _currentTime.Year.ToString();
        txtMonth.Text = _currentTime.Month.ToString().PadLeft(2, '0');
        txtDay.Text = _currentTime.Day.ToString().PadLeft(2, '0');
        txtHour.Text = _currentTime.Hour.ToString().PadLeft(2, '0');
        txtMinute.Text = _currentTime.Minute.ToString().PadLeft(2, '0');
    }

    public UxDatePicker()
    {
        InitializeComponent();
    }

    private void UxDatePicker_Load(object sender, EventArgs e)
    {
        SetTimeToControl();
        panel1.Height = txtDay.Height;
        panel1.Height = txtHour.Height;
        SetEvent(this);
    }

    private void SetEvent(Control? c)
    {
        if (c == null) return;
        c.MouseDown += c_MouseDown;
        foreach (Control item in c.Controls)
        {
            SetEvent(item);
        }
    }

    void c_MouseDown(object sender, MouseEventArgs e)
    {
        if (_selectPanel == null)
        {
            _selectPanel = new UxDateTimeSelectPanel();
            _selectPanel.SelectedTimeEvent += SelectPanel_SelectedTimeEvent;
            _selectPanel.CancelTimeEvent += SelectPanel_CancelTimeEvent;
        }

        _selectPanel.CurrentTime = _currentTime;
        _selectPanel.TimeType = _type;
        _frmAnchor = new FrmAnchor(this, _selectPanel);
        _frmAnchor.Show(FindForm());
    }

    private void SelectPanel_CancelTimeEvent(object sender, EventArgs e)
    {
        _frmAnchor.Hide();
    }

    private void SelectPanel_SelectedTimeEvent(object sender, EventArgs e)
    {
        if (_selectPanel != null) 
            CurrentTime = _selectPanel.CurrentTime;
        _frmAnchor.Hide();
    }

    private void txtYear_TextChanged(object sender, EventArgs e)
    {
        if (txtYear.Text.Length == 4)
            ActiveControl = txtMonth;
    }

    private void txtMonth_TextChanged(object sender, EventArgs e)
    {
        if (txtMonth.Text.Length == 2 || txtMonth.Text.ToInt() >= 3)
        {
            ActiveControl = txtDay;
        }
    }

    private void txtDay_TextChanged(object sender, EventArgs e)
    {
        if (_type == DateTimePickerType.Date)
            return;
        if (txtDay.Text.Length == 2 || txtDay.Text.ToInt() >= 4)
        {
            ActiveControl = txtHour;
        }
    }

    private void txtHour_TextChanged(object sender, EventArgs e)
    {
        if (txtHour.Text.Length == 2 || txtHour.Text.ToInt() >= 3)
        {
            ActiveControl = txtMinute;
        }
    }

    private void txtYear_Leave(object sender, EventArgs e)
    {
        if (txtYear.Text.ToInt() < 1990)
        {
            txtYear.Text = _currentTime.Year.ToString();
        }

        _currentTime = (txtYear.Text + _currentTime.ToString("-MM-dd HH:mm:ss")).ToDate();
    }

    private void txtMonth_Leave(object sender, EventArgs e)
    {
        if (txtMonth.Text.ToInt() < 1)
        {
            txtMonth.Text = _currentTime.Month.ToString().PadLeft(2, '0');
        }

        txtMonth.Text = txtMonth.Text.PadLeft(2, '0');
        _currentTime = (_currentTime.ToString("yyyy-" + txtMonth.Text + "-dd HH:mm:ss")).ToDate();
    }

    private void txtDay_Leave(object sender, EventArgs e)
    {
        if (txtDay.Text.ToInt() < 1 ||
            txtDay.Text.ToInt() > DateTime.DaysInMonth(_currentTime.Year, _currentTime.Month))
        {
            txtDay.Text = _currentTime.Day.ToString().PadLeft(2, '0');
        }

        txtDay.Text = txtDay.Text.PadLeft(2, '0');
        _currentTime = (_currentTime.ToString("yyyy-MM-" + txtDay.Text + " HH:mm:ss")).ToDate();
    }

    private void txtHour_Leave(object sender, EventArgs e)
    {
        if (txtHour.Text.ToInt() < 1)
        {
            txtHour.Text = _currentTime.Hour.ToString().PadLeft(2, '0');
        }

        txtHour.Text = txtHour.Text.PadLeft(2, '0');
        _currentTime = (_currentTime.ToString("yyyy-MM-dd " + txtHour.Text + ":mm:ss")).ToDate();
    }

    private void txtMinute_Leave(object sender, EventArgs e)
    {
        if (txtMinute.Text.ToInt() < 1)
        {
            txtMinute.Text = _currentTime.Minute.ToString().PadLeft(2, '0');
        }

        txtMinute.Text = txtMinute.Text.PadLeft(2, '0');
        _currentTime = (_currentTime.ToString("yyyy-MM-dd HH:" + txtMinute.Text + ":ss")).ToDate();
    }

    private void txt_SizeChanged(object sender, EventArgs e)
    {
        panel1.Height = (sender as UxTextBoxBase).Height;
    }
}