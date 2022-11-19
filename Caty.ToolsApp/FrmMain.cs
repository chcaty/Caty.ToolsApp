using Caty.ToolsApp.Frm;
using Caty.ToolsApp.Helper;
using Caty.ToolsApp.Model.Rss;
using Microsoft.Extensions.Options;

namespace Caty.ToolsApp;

public partial class FrmMain : Form
{
    private readonly System.Timers.Timer _t = new(1000 * 60 * 60)
    {
        Enabled = true, //是否执行绑定的事件
        AutoReset = true, //设置是执行一次（false）还是一直执行（true）
    };//实例化Timer类，设置间隔时间30分钟

    private readonly List<RssSource> _sources;

    public FrmMain(IOptions<List<RssSource>> options)
    {
        _sources = options.Value;
        InitializeComponent();
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
       UpdateRssInfo();
        _t.Elapsed += Execute;//绑定的事件
        _t.Start();
    }

    public void Execute(object? source, System.Timers.ElapsedEventArgs e)
    {
        _t.Stop();//关闭定时器
        Invoke(UpdateRssInfo);
        _t.Start();//重新开始定时器
    }

    public void UpdateRssInfo()
    {
        SetRssNews(GetRssInfo());
        lb_lastUpdateTime.Text = $@"最后更新时间：{DateTime.Now:yyyy-MM-dd hh:mm:ss}";
    }


    private List<RssFeed> GetRssInfo()
    {
        var urlList = _sources.Select(t => t.RssUrl);
        return Rss.GetRssFeeds(urlList);
    }

    private void SetRssNews(List<RssFeed> list)
    {
        tab_news.Controls.Clear();
        foreach (var rssFeed in list)
        {
            var tabPage = new TabPage
            {
                Text = rssFeed.Title,
                AutoScroll = true,
            };
            rssFeed.Items.OrderBy(o => o.PublishDate).AddLabel(tabPage);
            tab_news.Controls.Add(tabPage);
        }
    }

    private Image GetMoyuImage()
    {
        var client = new HttpClient();
        var stream = client.GetStreamAsync("https://api.vvhan.com/api/moyu").Result;
        return Image.FromStream(stream);
    }

    private void btn_moyu_Click(object sender, EventArgs e)
    {
        var frmPicture = new FrmPicture(GetMoyuImage(),"摸鱼日历");
        frmPicture.ShowDialog();
    }

    private void btn_rssConfig_Click(object sender, EventArgs e)
    {
        var frmPicture = new FrmRssConfig(_sources);
        frmPicture.ShowDialog();
    }

    private void btn_rand_Click(object sender, EventArgs e)
    {
        var url = Bing.GetBingImageUrlAsync();
        var filePath = Bing.DownloadImageAndSaveFile(url);
        _ = Bing.SystemParametersInfo(20, 0, filePath, 2);
    }
}