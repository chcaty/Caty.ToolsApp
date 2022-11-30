using System.Globalization;

namespace Caty.Tools.UxForm;

public partial class FrmBasic : Form
{
    // 窗体原始宽度
    private double _formWidth;

    // 窗体原始高度
    private double _formHeight;

    // 水平缩放比例
    private double _scaleX;

    // 垂直缩放比例
    private double _scaleY;

    //控件中心Left,Top,控件Width,控件Height,控件字体Size
    public Dictionary<string, string> ControlInfo = new();

    public FrmBasic()
    {
        InitializeComponent();
        GetAllInitInfo(Controls[0]);
    }

    protected void GetAllInitInfo(Control crlContainer)
    {
        if (crlContainer.Parent == this)
        {
            _formWidth = crlContainer.Width;
            _formHeight = crlContainer.Height;
        }

        foreach (Control item in crlContainer.Controls)
        {
            if (item.Name.Trim() == "") continue;
            ControlInfo.Add(item.Name,
                $"{item.Left + item.Width / 2},{item.Top + item.Height / 2},{item.Width},{item.Height},{item.Font.Size}");
            if (item is not UserControl && item.Controls.Count > 0)
            {
                GetAllInitInfo(item);
            }
        }
    }

    protected void ControlsChangeInit(Control crlContainer)
    {
        _scaleX = crlContainer.Width / _formWidth;
        _scaleY = crlContainer.Height / _formHeight;
    }

    protected void ControlsChange(Control crlContainer)
    {
        // pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
        var pos = new double[5];
        foreach (Control item in crlContainer.Controls)
        {
            if (item.Name.Trim() == "") continue;
            if (item is not UserControl && item.Controls.Count > 0)
            {
                ControlsChange(item);
            }

            var strs = ControlInfo[item.Name].Split(',');
            for (var j = 0; j < 5; j++)
            {
                pos[j] = Convert.ToDouble(strs[j]);
            }

            var itemWidth = pos[2] * _scaleX;
            var itemHeight = pos[3] * _scaleY;
            item.Left = (int)(pos[0] * _scaleX - itemWidth / 2);
            item.Top = (int)(pos[1] * _scaleY - itemHeight / 2);
            item.Width = (int)itemWidth;
            item.Height = (int)itemHeight;
            item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(_scaleX, _scaleY)).ToString(CultureInfo.InvariantCulture)));
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        if (ControlInfo.Count <= 0) return;
        ControlsChangeInit(Controls[0]);
        ControlsChange(Controls[0]);
    }
}