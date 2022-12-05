using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls;

public partial class UxButtonsGroup : UserControl
{

    /// <summary>
    /// 选中改变事件
    /// </summary>
    public event EventHandler SelectedItemChanged;

    private Dictionary<string, string> _dataSource = new();

    /// <summary>
    /// 数据源
    /// </summary>
    public Dictionary<string, string> DataSource
    {
        get => _dataSource;
        set
        {
            _dataSource = value;
            Reload();
        }
    }

    private List<string> _selectItem = new();

    /// <summary>
    /// 选中项
    /// </summary>
    public List<string> SelectItem
    {
        get => _selectItem;
        set
        {
            _selectItem = value;
            SetSelected();
        }
    }

    /// <summary>
    /// 是否多选
    /// </summary>
    public bool IsMultiple { get; set; } = false;

    public UxButtonsGroup()
    {
        InitializeComponent();
    }

    private void Reload()
    {
        try
        {
            ControlHelper.FreezeControl(flowLayoutPanel1, true);
            flowLayoutPanel1.Controls.Clear();
            foreach (var btn in DataSource.Select(item => new UxButtonBase
                     {
                         BackColor = Color.Transparent,
                         BtnBackColor = Color.White,
                         BtnFont = new Font("微软雅黑", 10F),
                         BtnForeColor = Color.Gray,
                         BtnText = item.Value,
                         CornerRadius = 5,
                         Cursor = Cursors.Hand,
                         FillColor = Color.White,
                         Font = new Font("微软雅黑", 15F, FontStyle.Regular, GraphicsUnit.Pixel),
                         IsRadius = true,
                         IsShowRect = true,
                         IsShowTips = false,
                         Location = new Point(5, 5),
                         Margin = new Padding(5),
                         Name = item.Key,
                         RectColor = Color.FromArgb(224, 224, 224),
                         RectWidth = 1,
                         Size = new Size(72, 38),
                         TabStop = false
                     }))
            {
                btn.BtnClick += btn_BtnClick;
                flowLayoutPanel1.Controls.Add(btn);
            }
        }
        finally
        {
            ControlHelper.FreezeControl(flowLayoutPanel1, false);
        }

        SetSelected();
    }

    private void btn_BtnClick(object sender, EventArgs e)
    {
        var btn = sender as UxButtonBase;
        if (btn?.Name != null && _selectItem.Contains(btn.Name))
        {
            btn.RectColor = Color.FromArgb(224, 224, 224);
            _selectItem.Remove(btn.Name);
        }
        else
        {
            if (!IsMultiple)
            {
                foreach (var item in _selectItem)
                {
                    var lst = flowLayoutPanel1.Controls.Find(item, false);
                    if (lst.Length != 1) continue;
                    if (lst[0] is UxButtonBase uxButtonBase) uxButtonBase.RectColor = Color.FromArgb(224, 224, 224);
                }

                _selectItem.Clear();
            }

            if (btn != null)
            {
                btn.RectColor = Color.FromArgb(255, 77, 59);
                _selectItem.Add(btn.Name);
            }
        }

        SelectedItemChanged(this, e);
    }

    private void SetSelected()
    {
        if (_selectItem is not { Count: > 0 } || DataSource is not { Count: > 0 }) return;
        try
        {
            ControlHelper.FreezeControl(flowLayoutPanel1, true);
            if (IsMultiple)
            {
                foreach (var item in _selectItem)
                {
                    var lst = flowLayoutPanel1.Controls.Find(item, false);
                    if (lst.Length != 1) continue;
                    if (lst[0] is UxButtonBase btn) btn.RectColor = Color.FromArgb(255, 77, 59);
                }
            }
            else
            {
                UxButtonBase? btn = null;
                foreach (var item in _selectItem)
                {
                    var lst = flowLayoutPanel1.Controls.Find(item, false);
                    if (lst.Length != 1) continue;
                    btn = lst[0] as UxButtonBase;
                    break;
                }

                if (btn == null) return;
                _selectItem = new List<string> { btn.Name };
                btn.RectColor = Color.FromArgb(255, 77, 59);
            }
        }
        finally
        {
            ControlHelper.FreezeControl(flowLayoutPanel1, false);
        }
    }
}