using Caty.Tools.Model.Rss;

namespace Caty.Tools.WinForm.UxControl
{
    public partial class RssShowControl : UserControl
    {
        private List<RssSource> _sources;
        private List<RssItem> _items;

        public RssShowControl()
        {
            InitializeComponent();
        }

        public RssShowControl(List<RssSource> sources)
        {
            _sources = sources;
        }
    }
}
