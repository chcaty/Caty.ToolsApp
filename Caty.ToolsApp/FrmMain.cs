using Caty.ToolsApp.Frm;
using Caty.ToolsApp.Helper;
using Caty.ToolsApp.Model.Rss;
using Caty.ToolsApp.UxControl;
using CefSharp;
using CefSharp.WinForms;
using Microsoft.Extensions.Options;

namespace Caty.ToolsApp;

public partial class FrmMain : FrmBasic
{
    private static double test = 1000 *60;
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


    private readonly List<RssSource> _sources;

    public FrmMain(IOptions<List<RssSource>> options)
    {
        _sources = options.Value;
        Cef.Initialize(new CefSettings());
        InitializeComponent();
        GetAllInitInfo(Controls[0]);
    }

    private void FrmMain_Load(object sender, EventArgs e)
    {
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

    public void UpdateRssInfo()
    {
        var rssSources = _sources.Where(w => w.IsEnabled);
        foreach (var rssSource in rssSources)
        {
            var btn = new Button
            {
                BackColor = SystemColors.Control,
                Dock = DockStyle.Top,
                Text = rssSource.RssName,
                Height= 50,
            };
            void showRssContent(object sender, EventArgs e)
            {
                spc_content.Panel1.Controls.Clear();
                var feeds = Rss.GetRssFeed(rssSource.RssUrl);
                if (feeds != null && feeds.Items.Count > 0)
                {
                    foreach (var item in feeds.Items)
                    {
                        var rssControl = new RssContentControl(item)
                        {
                            Dock = DockStyle.Top,
                        };
                        void showDetail(object sender, EventArgs e)
                        {
                            spc_content.Panel2.Controls.Clear();
                            var browser = new ChromiumWebBrowser(item.ContentLink)
                            {
                                Dock= DockStyle.Fill,
                            };
                            spc_content.Panel2.Controls.Add(browser);
                        }
                        rssControl.ControlClick += showDetail;
                        spc_content.Panel1.Controls.Add(rssControl);
                    }
                }
            }
            btn.Click += showRssContent;
            spc_source.Panel1.Controls.Add(btn);
        }
        Text = $@"工作姬  最后更新时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
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

    private void btn_rssConfig_Click(object sender, EventArgs e)
    {
        var frmPicture = new FrmRssConfig(_sources);
        frmPicture.ShowDialog();
        Task.Run(() =>
        {
            BeginInvoke(UpdateRssInfo);
        });
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

    private void notifyIcon_main_DoubleClick(object sender, EventArgs e)
    {
        if(WindowState == FormWindowState.Minimized)
        {
            ShowMainFrom();
            // 激活窗体并给予它焦点
            Activate();
        }
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
}