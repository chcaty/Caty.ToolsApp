using Caty.Tools.Model.Rss;

namespace Caty.Tools.WinForm.UxControl;

public partial class RssConfigControl : UserControl
{
    private RssSource Source{ get; }  

    public RssConfigControl() => InitializeComponent();

    public RssConfigControl(RssSource source)
    {
        Source = source;
        InitializeComponent();
    }

    private void RssConfigControl_Load(object sender, EventArgs e)
    {
        txt_name.Text = Source.RssName;
        txt_url.Text = Source.RssUrl;
        ck_enable.Checked = Source.IsEnabled;
        txt_desc.Text = Source.RssDescription;
    }

    private void txt_name_TextChanged(object sender, EventArgs e)
    {
        Source.RssName = txt_name.Text;
    }

    private void txt_url_TextChanged(object sender, EventArgs e)
    {
        Source.RssUrl = txt_url.Text;
    }

    private void txt_desc_TextChanged(object sender, EventArgs e)
    {
        Source.RssDescription = txt_desc.Text;
    }

    private void ck_enable_CheckedChanged(object sender, EventArgs e)
    {
        Source.IsEnabled = ck_enable.Checked;
    }
}