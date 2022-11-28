using Caty.Tools.Model.Rss;

namespace Caty.Tools.WinForm.UxControl
{
    public partial class RssContentControl : UserControl
    {
        private readonly RssItem _item;
        public Color UxBackColor = SystemColors.Control;
        public event EventHandler? ControlClick;

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
            lb_pubDate.Text = $@"发布时间：{_item.PublishDate:yyyy-MM-dd HH:mm:ss}";
        }

        public override bool Focused => lb_desc.Focused || lb_name.Focused || lb_pubDate.Focused;

        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            lb_pubDate.BackColor = UxBackColor;
            lb_desc.BackColor = UxBackColor;
            lb_name.BackColor = UxBackColor;
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
