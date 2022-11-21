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
            panel_middle.Controls.Add(rssConfig);
        }
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
        Config.UpdateConfig("RssSources",sources);
        Dispose();
        Close();
    }

    private void btn_close_Click(object sender, EventArgs e)
    {
        Dispose();
        Close();
    }

    private void btn_add_Click(object sender, EventArgs e)
    {
        var add = new RssSource();
        sources.Add(add);
        panel_middle.Controls.Add(new RssConfigControl(add) { Dock = DockStyle.Top });
    }
}