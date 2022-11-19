using Caty.ToolsApp.Helper;
using Caty.ToolsApp.Model.Rss;

namespace Caty.ToolsApp.Frm;

public partial class FrmRssConfig : FrmDialog
{
    private List<RssSource> sources;
    public FrmRssConfig(List<RssSource> rssSources)
    {
        InitializeComponent();
        sources = rssSources;
    }

    private void FrmRssConfig_Load(object sender, EventArgs e)
    {
        foreach (var rssConfig in sources.Select(source => new RssConfigControl(source)
                 {
                     Dock = DockStyle.Top
                 }))
        {
            panel_top.Controls.Add(rssConfig);
        }
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
        Config.UpdateConfig("RssSources",sources);
    }

    private void btn_close_Click(object sender, EventArgs e)
    {
        Close();
    }
}