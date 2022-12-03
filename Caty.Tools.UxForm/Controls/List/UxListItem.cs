using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.List;

[ToolboxItem(false)]
public partial class UxListItem : UserControl
{
    [Description("标题"), Category("自定义")]
    public string Title
    {
        get => label1.Text;
        set => label1.Text = value;
    }

    [Description("副标题"), Category("自定义")]
    public string SubTitle
    {
        get => label3.Text;
        set
        {
            label3.Text = value;
            label3.Visible = !string.IsNullOrEmpty(value);
        }
    }

    [Description("标题字体"), Category("自定义")]
    public Font TitleFont
    {
        get => label1.Font;
        set => label1.Font = value;
    }

    [Description("副标题字体"), Category("自定义")]
    public Font SubTitleFont
    {
        get => label3.Font;
        set => label3.Font = value;
    }

    [Description("背景色"), Category("自定义")]
    public Color ItemBackColor
    {
        get => BackColor;
        set => BackColor = value;
    }

    [Description("标题文本色"), Category("自定义")]
    public Color TitleForeColor
    {
        get => label1.ForeColor;
        set => label1.ForeColor = value;
    }

    [Description("副标题文本色"), Category("自定义")]
    public Color SubTitleForeColor
    {
        get => label3.ForeColor;
        set => label3.ForeColor = value;
    }

    [Description("是否显示右侧更多箭头"), Category("自定义")]
    public bool ShowMoreBtn
    {
        get => label2.Visible;
        set => label2.Visible = value;
    }

    [Description("项选中事件"), Category("自定义")]
    public event EventHandler ItemClick;

    /// <summary>
    /// 数据源
    /// </summary>
    public ListEntity DataSource { get; private set; }

    public UxListItem()
    {
        InitializeComponent();
        SetStyle(
            ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
            ControlStyles.OptimizedDoubleBuffer, true);
        UpdateStyles();
    }

    private void item_MouseDown(object sender, MouseEventArgs e)
    {
        if (ItemClick != null)
        {
            ItemClick(this, e);
        }
    }

    /// <summary>
    /// 设置数据
    /// </summary>
    /// <param name="data"></param>
    public void SetData(ListEntity data)
    {
        Title = data.Title;
        SubTitle = data.SubTitle;
        ShowMoreBtn = data.ShowMoreBtn;
        DataSource = data;
    }
}