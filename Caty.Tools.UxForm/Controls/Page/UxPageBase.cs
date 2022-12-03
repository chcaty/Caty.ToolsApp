using System.ComponentModel;

namespace Caty.Tools.UxForm.Controls.Page;

[ToolboxItem(false)]
public partial class UxPageBase : UserControl,IPageControl
{
    public virtual event PageControlEventHandler? ShowSourceChanged;
    public virtual List<object> DataSource { get; set; }

    [Description("每页显示数量"), Category("自定义")]
    public virtual int PageSize { get; set; } = 1;

    private int _startIndex;
    [Description("开始的下标"), Category("自定义")]
    public virtual int StartIndex
    {
        get => _startIndex;
        set
        {
            _startIndex = value;
            if (_startIndex <= 0)
                _startIndex = 0;
        }
    }

    public virtual int PageCount { get; set; }
    public virtual int PageIndex { get; set; }

    public UxPageBase()
    {
        InitializeComponent();
    }

    private void UxPageBase_Load(object sender, EventArgs e)
    {
        if (DataSource == null)
            ShowBtn(false, false);
        else
        {
            ShowBtn(false, DataSource.Count > PageSize);
        }
    }

    public virtual void FirstPage()
    {
        if(DataSource ==null)
            return;
        _startIndex = 0;
        var currentSource = GetCurrentSource();
        ShowSourceChanged?.Invoke(currentSource);
    }

    public virtual void PreviousPage()
    {
        if (DataSource == null)
            return;
        if (_startIndex == 0)
            return;
        _startIndex -= PageSize;
        if (_startIndex < 0)
            _startIndex = 0;
        var currentSource = GetCurrentSource();

        ShowSourceChanged?.Invoke(currentSource);
    }

    public virtual void NextPage()
    {
        if (DataSource == null)
            return;
        if (_startIndex + PageSize >= DataSource.Count)
        {
            return;
        }
        _startIndex += PageSize;
        if (_startIndex< 0)
            _startIndex = 0;
        var currentSource = GetCurrentSource();

        ShowSourceChanged?.Invoke(currentSource);
    }

    public virtual void EndPage()
    {
        if (DataSource == null)
            return;
        _startIndex = DataSource.Count - PageSize;
        if (_startIndex < 0)
            _startIndex = 0;
        var currentSource = GetCurrentSource();

        ShowSourceChanged?.Invoke(currentSource);
    }

    public virtual void Reload()
    {
        var currentSource = GetCurrentSource();
        ShowSourceChanged?.Invoke(currentSource);
    }

    public virtual List<object> GetCurrentSource()
    {
        if (DataSource == null)
            return null;
        var intShowCount = PageSize;
        if (intShowCount + StartIndex > DataSource.Count)
            intShowCount = DataSource.Count - StartIndex;
        var objs = new object[intShowCount];
        DataSource.CopyTo(_startIndex, objs, 0, intShowCount);
        var list = objs.ToList();

        var blnLeft = false;
        var blnRight = false;
        if (list.Count > 0)
        {
            blnLeft = DataSource.IndexOf(list[0]) > 0;

            blnRight = DataSource.IndexOf(list[^1]) < DataSource.Count - 1;
        }
        ShowBtn(blnLeft,blnRight);
        return list;
    }

    /// <summary>
    /// 控制按钮显示
    /// </summary>
    /// <param name="blnLeftBtn">是否显示上一页，第一页</param>
    /// <param name="blnRightBtn">是否显示下一页，最后一页</param>
    protected virtual void ShowBtn(bool blnLeftBtn, bool blnRightBtn) { }
}