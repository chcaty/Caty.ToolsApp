namespace Caty.ToolsApp.Frm;

public partial class FrmBasic : Form
{
    // 窗体原始宽度
    private double formWidth;
    // 窗体原始高度
    private double formHeight;
    // 水平缩放比例
    private double scaleX;
    // 垂直缩放比例
    private double scaleY;
    //控件中心Left,Top,控件Width,控件Height,控件字体Size
    public Dictionary<string, string> controlInfo= new Dictionary<string, string>();

    public FrmBasic()
    {
        InitializeComponent();
        GetAllInitInfo(Controls[0]);
    }

    protected void GetAllInitInfo(Control CrlContainer)
    {
        if (CrlContainer.Parent == this)
        {
            formWidth = CrlContainer.Width;
            formHeight = CrlContainer.Height;
        }
        foreach(Control item in CrlContainer.Controls)
        {
            if(item.Name.Trim() != "")
            {
                controlInfo.Add(item.Name, $"{item.Left + item.Width / 2},{item.Top + item.Height/2},{item.Width},{item.Height},{item.Font.Size}");
                if((item as UserControl) == null && item.Controls.Count > 0)
                {
                    GetAllInitInfo(item);
                }
            }
        }
    }

    protected void ControlsChangeInit(Control CrlContainer)
    {
        scaleX= CrlContainer.Width / formWidth;
        scaleY= CrlContainer.Height / formHeight;
    }

    protected void ControlsChange(Control CrlContainer)
    {
        // pos数组保存当前控件中心Left,Top,控件Width,控件Height,控件字体Size
        var pos = new double[5];
        foreach(Control item in CrlContainer.Controls)
        {
            if(item.Name.Trim() != "")
            {
                if((item as UserControl) == null&& item.Controls.Count > 0)
                {
                    ControlsChange(item);
                }
                var strs = controlInfo[item.Name].Split(',');
                for(var j = 0; j<5;j++)
                {
                    pos[j] = Convert.ToDouble(strs[j]);
                }
                var itemWidth = pos[2] * scaleX;
                var itemHeight = pos[3] * scaleY;
                item.Left = (int)(pos[0] * scaleX - itemWidth / 2);
                item.Top = (int)(pos[1] * scaleY - itemHeight / 2);
                item.Width = (int)itemWidth;
                item.Height = (int)itemHeight;
                item.Font = new Font(item.Font.Name, float.Parse((pos[4] * Math.Min(scaleX, scaleY)).ToString()));
            }
        }
    }

    protected override void OnSizeChanged(EventArgs e)
    {
        base.OnSizeChanged(e);
        if (controlInfo.Count > 0)
        {
            ControlsChangeInit(Controls[0]);
            ControlsChange(Controls[0]);
        }
    }
}