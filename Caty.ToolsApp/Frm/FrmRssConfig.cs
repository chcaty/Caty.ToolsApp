using Caty.ToolsApp.Model.Rss;

namespace Caty.ToolsApp.Frm;

public partial class FrmRssConfig : FrmDialog
{
    private readonly List<RssSource> _sources;
    public FrmRssConfig(List<RssSource> rssSources)
    {
        InitializeComponent();
        _sources = rssSources;
    }

    private void FrmRssConfig_Load(object sender, EventArgs e)
    {
        foreach (var rssConfig in _sources.Select(source => new RssConfigControl(source.RssName, source.RssUrl, source.RssDescription, source.IsEnabled)
                 {
                     Dock = DockStyle.Top
                 }))
        {
            Controls.Add(rssConfig);
        }
    }
}