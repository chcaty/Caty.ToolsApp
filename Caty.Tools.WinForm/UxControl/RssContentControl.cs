using Caty.Tools.Model.Rss;

namespace Caty.Tools.WinForm.UxControl
{
    public partial class RssContentControl : UserControl
    {
        private RssItem _item;
        public event EventHandler ControlClick;

        public RssContentControl()
        {
            InitializeComponent();
        }

        public RssContentControl(RssItem item)
        {
            InitializeComponent();
            _item = item;
        }

        private void RssContentControl_Load(object sender, EventArgs e)
        {
            lb_desc.Text = _item.Summary;
            lb_name.Text = _item.Title;
            lb_pubDate.Text = $"发布时间：{_item.PublishDate:yyyy-MM-dd HH:mm:ss}";
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
        }

        private void RssContentControl_Click(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }

        private void lb_name_Click(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }

        private void lb_pubDate_Click(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }

        private void lb_desc_Click(object sender, EventArgs e)
        {
            ControlClick?.Invoke(sender, e);
        }
    }
}
