using Caty.Tools.Model.Rss;

namespace Caty.Tools.WinForm.UxControl;

public partial class RssContentControl : UserControl
{
    public RssItem? Item;
    public Color UxBackColor = SystemColors.Control;
    public event EventHandler? ControlClick;

    public RssContentControl()
    {
        InitializeComponent();
    }

    public RssContentControl(RssItem item)
    {
        InitializeComponent();
        Item = item;
    }

    private void RssContentControl_Load(object sender, EventArgs e)
    {
        SetAutoSize(true);
        //rtb_desc.Text = _item.Summary;
        if (Item == null) return;
        txt_name.Text = Item.Title;
        txt_pubDate.Text = $@"发布时间：{Item.PublishDate:yyyy-MM-dd HH:mm:ss}";
        if (Item.IsRead) return;
        UxBackColor = Color.FloralWhite;
        BackColor = UxBackColor;
    }

    protected override void OnBackColorChanged(EventArgs e)
    {
        base.OnBackColorChanged(e);
        SetBackColor(UxBackColor);
    }

    private void RssContentControl_Click(object sender, EventArgs e)
    {
        ControlClick?.Invoke(sender, e);
    }

    public void SetBackColor(Color color)
    {
        txt_pubDate.BackColor = color;
        //rtb_desc.BackColor = color;
        txt_name.BackColor = color;
        //lb_split.BackColor = color;
    }

    private void SetAutoSize(bool enable)
    {
        txt_name.AutoSize = enable;
        txt_pubDate.AutoSize = enable;
        //lb_split.AutoSize = enable;
        //rtb_desc.AutoSize = enable;
    }
}