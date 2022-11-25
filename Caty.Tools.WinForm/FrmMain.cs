using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Rss;
using Caty.Tools.UxForm;
using Caty.Tools.WinForm.Frm;
using Caty.Tools.WinForm.Helper;
using Caty.Tools.WinForm.UxControl;
using CefSharp;
using CefSharp.WinForms;

namespace Caty.Tools.WinForm;

public partial class FrmMain : FrmBasic
{
    private static double test = 1000 * 60;
    private static readonly double hours = 1000 * 60 * 60;

    private readonly System.Timers.Timer _updateRss = new(hours)
    {
        Enabled = true, //是否执行绑定的事件
        AutoReset = true, //设置是执行一次（false）还是一直执行（true）
    };//实例化Timer类，设置间隔时间30分钟

    private readonly System.Timers.Timer _checkUpdate = new(test)
    {
        Enabled = true, //是否执行绑定的事件
        AutoReset = true, //设置是执行一次（false）还是一直执行（true）
    };//实例化Timer类，设置间隔时间30分钟

    private readonly IRssSourceService _rssSourceService;
    private readonly IRssFeedService _rssFeedService;
    private readonly IRssItemService _rssItemService;
    private RssSource _rssSource;

    public FrmMain(IRssSourceService rssSourceService,IRssFeedService rssFeedService, IRssItemService rssItemService)
    {
        _rssSourceService = rssSourceService;
        _rssFeedService = rssFeedService;
        _rssItemService = rssItemService;
        Cef.Initialize(new CefSettings());
        InitializeComponent();
        GetAllInitInfo(Controls[0]);
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
        LoadRssInfo();
        UpdateRssInfo();
        _updateRss.Elapsed += UpdateRssExecute;//绑定的事件
        _updateRss.Start();
        _checkUpdate.Elapsed += CheckUpdate;
        _checkUpdate.Start();
    }

    public void UpdateRssExecute(object? source, System.Timers.ElapsedEventArgs e)
    {
        _updateRss.Stop();//关闭定时器
        BeginInvoke(UpdateRssInfo);
        _updateRss.Start();//重新开始定时器
    }

    public void CheckUpdate(object? source, System.Timers.ElapsedEventArgs e)
    {
        _checkUpdate.Stop();//关闭定时器
        //ProcessStartInfo processStartInfo = new()
        //{
        //    //参数:【升级程序】HHUpdateApp.exe程序所在路径
        //    FileName = "./AutoUpdate/HHUpdateApp.exe",
        //    //参数1:【应用程序】的名词，例如：LOLClient；参数1:检查更新模式
        //    Arguments = "Caty.ToolsApp " + "0"
        //};
        //Process proc = Process.Start(processStartInfo);
        //proc?.WaitForExit();
        _checkUpdate.Start();//重新开始定时器
    }

    private async void LoadRssInfo()
    {
        var rssSources = await _rssSourceService.List(true);
        panel_source.Controls.Clear();
        if (rssSources == null) return;
        foreach (var rssSource in rssSources)
        {
            var btn = new Button
            {
                BackColor = SystemColors.Control,
                Dock = DockStyle.Top,
                Text = rssSource.RssName,
                Height = 50
            };
            setButtonClick(btn,rssSource);
            panel_source.Controls.Add(btn);
        }
        Text = $"工作姬  最后更新时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }

    private void setButtonClick(Button btn, RssSource rssSource)
    {
        async void showRssContent(object sender, EventArgs e)
        {
            spc_content.Panel1.Controls.Clear();
            _rssSource = rssSource;
            var feed = await _rssFeedService.GetFeedBySourceId(rssSource.Id);
            if (feed != null)
            {
                var items = await _rssItemService.List(feed.Id);
                if (items == null) return;
                foreach (var item in items)
                {
                    setRssDetail(item);
                }
            }
        }
        btn.Click += showRssContent;
        void getFocus(object sender, EventArgs e)
        {
            btn_edit.Enabled = true;
            btn.BackColor= SystemColors.ControlDark;
        }
        btn.GotFocus += getFocus;
        void lostFocus(object sender, EventArgs e)
        {
            btn_edit.Enabled = false;
            btn.BackColor = SystemColors.Control;
        }
        btn.LostFocus += lostFocus;
    }

    private void setRssDetail(RssItem item)
    {
        var rssControl = new RssContentControl(item)
        {
            Dock = DockStyle.Top,
        };
        void showDetail(object sender, EventArgs e)
        {
            rssControl.Focus();
            spc_content.Panel2.Controls.Clear();
            var browser = new ChromiumWebBrowser(item.ContentLink)
            {
                Dock = DockStyle.Fill,
            };
            spc_content.Panel2.Controls.Add(browser);
        }
        rssControl.ControlClick += showDetail;
        void getFocus(object sender, EventArgs e)
        {
            rssControl.backColor = SystemColors.ControlLight;
            rssControl.BackColor = SystemColors.ControlLight;
        }
        rssControl.GotFocus += getFocus;
        void lostFocus(object sender, EventArgs e)
        {
            rssControl.backColor = SystemColors.Control;
            rssControl.BackColor = SystemColors.Control;
        }
        rssControl.LostFocus += lostFocus;
        spc_content.Panel1.Controls.Add(rssControl);
    }

    /// <summary>
    /// 从Rss源处更新RssInfo
    /// </summary>
    public async void UpdateRssInfo()
    {
        var sources = await _rssSourceService.List(true);
        if (sources == null) return;
        foreach (var source in sources)
        {
            var feed = await _rssFeedService.GetFeedBySourceId(source.Id);
            if (feed == null)
            {
                var add = Rss.GetRssFeed(source.RssUrl);
                if (add == null) return;
                add.SourceId= source.Id;
                feed = await _rssFeedService.Add(add);
            } 
            var items = Rss.GetRssItems(source.RssUrl);
            if (items == null) continue;
            var addItems = new List<RssItem>();
            foreach (var item in items)
            {
                var isRepeat = await _rssItemService.CheckRepeat(feed.Id, item.ContentLink);
                if(!isRepeat)
                {
                    item.FeedId = feed.Id;
                    addItems.Add(item);
                }
            }
            await _rssItemService.Add(addItems);
        }
        Text = $"工作姬  最后更新时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
    }

    private Image GetMoyuImage()
    {
        var client = new HttpClient();
        var stream = client.GetStreamAsync("https://api.vvhan.com/api/moyu").Result;
        return Image.FromStream(stream);
    }

    private void btn_moyu_Click(object sender, EventArgs e)
    {
        var frmPicture = new FrmPicture(GetMoyuImage(), "摸鱼日历");
        frmPicture.ShowDialog();
    }

    private void btn_rand_Click(object sender, EventArgs e)
    {
        var url = Bing.GetBingImageUrlAsync();
        var filePath = Bing.DownloadImageAndSaveFile(url);
        _ = Bing.SystemParametersInfo(20, 0, filePath, 2);
    }

    private void notifyIcon_main_Click(object sender, EventArgs e)
    {
        ShowMainFrom();
    }

    private void notifyIcon_main_MouseClick(object sender, MouseEventArgs e)
    {
        if(e.Button== MouseButtons.Right)
        {
            contextMenuStrip_notify.Show();
        }else if(e.Button== MouseButtons.Left)
        {
            ShowMainFrom();
            Visible = true;
        }
    }

    private void item_close_Click(object sender, EventArgs e)
    {
        if (MessageBox.Show("是否确认退出程序？", "退出", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
        {
            Dispose();
            Close();
        }
    }

    private void item_main_Click(object sender, EventArgs e)
    {
        ShowMainFrom();
    }

    private void ShowMainFrom()
    {
        // 还原窗体显示
        WindowState = FormWindowState.Normal;
        // 任务栏区显示图标
        ShowInTaskbar = true;
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

    private void btn_add_Click(object sender, EventArgs e)
    {
        FrmRssConfig frm = new(_rssSourceService);
        frm.ShowDialog();
        Task.Run(() =>
        {
            BeginInvoke(LoadRssInfo);
        });
    }

    private void btn_edit_Click(object sender, EventArgs e)
    {
        FrmRssConfig frm = new(_rssSource, _rssSourceService);
        frm.ShowDialog();
        Task.Run(() =>
        {
            BeginInvoke(LoadRssInfo);
        });
    }
}