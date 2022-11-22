using Caty.ToolsApp.Model.Rss;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Caty.ToolsApp.UxControl
{
    public partial class RssContentControl : UserControl
    {
        private RssItem _item;

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
            lb_name.Text = _item.Title;
        }
    }
}
