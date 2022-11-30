using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Rss;
using Caty.Tools.WinForm.UxForm;
using CefSharp.WinForms;

namespace Caty.Tools.WinForm.UxControl
{
    public partial class RssShowControl : UserControl
    {
        private List<RssSource> _sources;
        private List<RssItem> _items;
        private RssSource currentSource;
        public event EventHandler? ButtonAddClick;
        public event EventHandler? ButtonEditClick;
        public event EventHandler? ButtonReadClick;

        public RssShowControl()
        {
            InitializeComponent();
        }

        public RssShowControl(List<RssSource> sources)
        {
            _sources = sources;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            ButtonAddClick?.Invoke(sender, e);
        }

        private void btn_edit_Click(object sender, EventArgs e)
        {
            //FrmRssConfig frm = new(currentSource);
            //frm.ShowDialog();
            //if (frm.IsSave)
            //{
            //    _rssSourceService.Add(frm.Source);
            //}
            //Task.Run(() => { BeginInvoke(LoadRssInfo); });
        }

        private void btn_read_Click(object sender, EventArgs e)
        {

        }

        private async void LoadRssInfo()
        {
            //var rssSources = await _rssSourceService.List(true);
            //panel_source.Controls.Clear();
            //if (rssSources == null) return;
            //foreach (var rssSource in rssSources)
            //{
            //    var btn = new Button
            //    {
            //        BackColor = SystemColors.Control,
            //        Dock = DockStyle.Top,
            //        Text = rssSource.RssName,
            //        Height = 50,
            //        FlatStyle = FlatStyle.Flat,
            //    };
            //    SetButtonClick(btn, rssSource);
            //    panel_source.Controls.Add(btn);
            //}

            Text = $@"工作姬  最后更新时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss}";
        }

        private void SetButtonClick(Control btn, RssSource rssSource)
        {
            //async void ShowRssContent(object? sender, EventArgs e)
            //{
            //    spc_content.Panel1.Controls.Clear();
            //    currentSource = rssSource;
            //    var feed = await _rssFeedService.GetFeedBySourceId(rssSource.Id);
            //    if (feed == null) return;
            //    var items = await _rssItemService.List(feed.Id);
            //    if (items == null) return;
            //    foreach (var item in items)
            //    {
            //        SetRssDetail(item);
            //    }
            //}

            //btn.Click += ShowRssContent;

            //void GetFocus(object? sender, EventArgs e)
            //{
            //    btn_edit.Enabled = true;
            //    btn.BackColor = SystemColors.ControlDark;
            //}

            //btn.GotFocus += GetFocus;

            //void LostFocus(object? sender, EventArgs e)
            //{
            //    btn_edit.Enabled = false;
            //    btn.BackColor = SystemColors.Control;
            //}

            //btn.LostFocus += LostFocus;
        }

        private void SetRssDetail(RssItem item)
        {
            //var rssControl = new RssContentControl(item)
            //{
            //    Dock = DockStyle.Top,
            //};

            //void ShowDetail(object? sender, EventArgs e)
            //{
            //    rssControl.Focus();
            //    spc_content.Panel2.Controls.Clear();
            //    var browser = new ChromiumWebBrowser(item.ContentLink)
            //    {
            //        Dock = DockStyle.Fill,
            //    };
            //    item.IsRead = true;
            //    _rssItemService.Update(item);
            //    spc_content.Panel2.Controls.Add(browser);
            //}

            //rssControl.ControlClick += ShowDetail;

            //void GetFocus(object? sender, EventArgs e)
            //{
            //    rssControl.SetBackColor(SystemColors.ControlDark);
            //}

            //rssControl.Enter += GetFocus;

            //void LostFocus(object? sender, EventArgs e)
            //{
            //    rssControl.SetBackColor(SystemColors.Control);
            //}

            //rssControl.Leave += LostFocus;
            //spc_content.Panel1.Controls.Add(rssControl);
        }

    }
}
