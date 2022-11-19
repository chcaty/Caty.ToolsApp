namespace Caty.ToolsApp;

public partial class RssConfigControl : UserControl
{
    public RssConfigControl()
    {
        InitializeComponent();
    }
    public RssConfigControl(string name, string url, string desc, bool enable)
    {
        InitializeComponent();
        ck_enable.Checked = enable;
        txt_name.Text = name;
        txt_url.Text = url;
        txt_desc.Text = desc;
    }

    public void SetName(string name)
    {
        txt_name.Text = name;
    }

    public void SetUrl(string url)
    {
        txt_url.Text = url;
    }

    public void SetDesc(string desc)
    {
        txt_desc.Text = desc;
    }

    public void SetEnable(bool enable)
    {
        ck_enable.Checked = enable;
    }
}