using Caty.Tools.Model.Rss;
using Caty.Tools.UxForm;
using Caty.Tools.WinForm.Helper;
using Caty.Tools.WinForm.UxControl;

namespace Caty.Tools.WinForm.Frm;

public partial class FrmRssConfig : FrmDialog
{
    private RssSource _source;

    public FrmRssConfig()
    {
        InitializeComponent();
    }

    public FrmRssConfig(RssSource source)
    {
        InitializeComponent();
        _source = source;
    }

    private void FrmRssConfig_Load(object sender, EventArgs e)
    {
        //txt_name.Text = 
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
        //Config.UpdateConfig("RssSources",sources);
        Dispose();
        Close();
    }

    private void btn_close_Click(object sender, EventArgs e)
    {
        Dispose();
        Close();
    }
}