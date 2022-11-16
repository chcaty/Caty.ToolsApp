using Caty.ToolsApp.Frm;
using Caty.ToolsApp.Rss;

namespace Caty.ToolsApp
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            var list = RssCommon.GetRssFeeds(new[]
            {
                "https://sspai.com/feed", 
                "https://www.appinn.com/feed/"
            });
            SetRssNews(list);
        }

        private void SetRssNews(List<RssFeed> list)
        {
            foreach (var rssFeed in list)
            {
                var tabPage = new TabPage
                {
                    Text = rssFeed.Title
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
        
    }
}