using Caty.Tools.Model.Rss;
using Caty.Tools.Service.Rss;
using Caty.Tools.UxForm.Forms;

namespace Caty.Tools.WinForm.UxForm;

public partial class FrmRssConfig : FrmBasicDialog
{
    private RssSource? _source;
    private readonly bool _isAdd;
    private readonly IRssSourceService _rssSourceService;

    public FrmRssConfig(IRssSourceService rssSourceService)
    {
        InitializeComponent();
        _isAdd= true;
        _rssSourceService = rssSourceService;
    }

    public FrmRssConfig(RssSource? source, IRssSourceService rssSourceService)
    {
        InitializeComponent();
        _source = source;
        _isAdd = false;
        _rssSourceService = rssSourceService;
    }

    private void FrmRssConfig_Load(object sender, EventArgs e)
    {
        if (_source != null)
        {
            txt_name.Text = _source.RssName;
            lb_id.Text = $@"{_source.Id}";
            rtb_Url.Text = _source.RssUrl;
            ck_IsEnable.Checked = _source.IsEnabled;
        }
        else
        {
            _source = new RssSource();
        }
    }

    private void btn_save_Click(object sender, EventArgs e)
    {
        if (_source != null)
        {
            if (_isAdd)
            {
                _rssSourceService.Add(_source);
            }
            else
            {
                _rssSourceService.Update(_source);
            }
        }
        btn_close_Click(sender, e);
    }

    private void btn_close_Click(object sender, EventArgs e)
    {
        Dispose();
        Close();
    }

    private void txt_name_TextChanged(object sender, EventArgs e)
    {
        if (_source != null)
        {
            _source.RssName = txt_name.Text;
        }
    }

    private void rtb_Url_TextChanged(object sender, EventArgs e)
    {
        if (_source != null)
        {
            _source.RssUrl = rtb_Url.Text;
        }
    }

    private void ck_IsEnable_CheckedChanged(object sender, EventArgs e)
    {
        if (_source != null)
        {
            _source.IsEnabled = ck_IsEnable.Checked;
        }
    }
}