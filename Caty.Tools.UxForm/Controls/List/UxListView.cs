using System.Collections;
using System.ComponentModel;
using Caty.Tools.UxForm.Controls.Page;
using Caty.Tools.UxForm.Helpers;

namespace Caty.Tools.UxForm.Controls.List;

public partial class UxListView : UserControl
{
    /// <summary>
    /// The m int cell width
    /// </summary>
    private int _intCellWidth = 130;//单元格宽度
    /// <summary>
    /// The m int cell height
    /// </summary>
    private int _intCellHeight = 120;//单元格高度

    /// <summary>
    /// The m item type
    /// </summary>
    private Type _itemType = typeof(UxListViewItem);

    /// <summary>
    /// Gets or sets the type of the item.
    /// </summary>
    /// <value>The type of the item.</value>
    /// <exception cref="System.Exception">单元格控件没有继承实现接口IListViewItem</exception>
    /// <exception cref="Exception">单元格控件没有继承实现接口IListViewItem</exception>
    [Description("单元格类型，如果无法满足您的需求，你可以自定义单元格控件，并实现接口IListViewItem"), Category("自定义")]
    public Type ItemType
    {
        get => _itemType;
        set
        {
            if (!typeof(IListViewItem).IsAssignableFrom(value) || !value.IsSubclassOf(typeof(Control)))
                throw new Exception("单元格控件没有继承实现接口IListViewItem");
            _itemType = value;
        }
    }

    /// <summary>
    /// The m page
    /// </summary>
    private UxPageBase _page;
    /// <summary>
    /// 翻页控件
    /// </summary>
    /// <value>The page.</value>
    /// <exception cref="System.Exception">翻页控件没有继承UCPagerControlBase</exception>
    /// <exception cref="Exception">翻页控件没有继承UCPagerControlBase</exception>
    [Description("翻页控件，如果UCPagerControl不满足你的需求，请自定义翻页控件并继承UCPagerControlBase"), Category("自定义")]
    public UxPageBase Page
    {
        get => _page;
        set
        {
            _page = value;
            if (value != null)
            {
                if (!(value is IPageControl) || !value.GetType().IsSubclassOf(typeof(UxPageBase)))
                    throw new Exception("翻页控件没有继承UCPagerControlBase");
                panMain.AutoScroll = false;
                panPage.Visible = true;
                Controls.SetChildIndex(panMain, 0);
                _page.ShowSourceChanged += page_ShowSourceChanged;
                _page.Dock = DockStyle.Fill;
                panPage.Controls.Clear();
                panPage.Controls.Add(_page);
                GetCellCount();
                DataSource = _page.GetCurrentSource();
            }
            else
            {
                panMain.AutoScroll = true;
                _page = null;
                panPage.Visible = false;
            }
        }
    }

    /// <summary>
    /// The m data source
    /// </summary>
    private object _dataSource;

    /// <summary>
    /// Gets or sets the data source.
    /// </summary>
    /// <value>The data source.</value>
    /// <exception cref="System.Exception">数据源不是有效的数据类型，列表</exception>
    /// <exception cref="Exception">数据源不是有效的数据类型，列表</exception>
    [Description("数据源,如果使用翻页控件，请使用翻页控件的DataSource"), Category("自定义")]
    public object DataSource
    {
        get => _dataSource;
        set
        {
            if (value == null)
            {
                _dataSource = value;
                ReloadSource();
                return;
            }
            if (value is not IList)
            {
                throw new Exception("数据源不是有效的数据类型，列表");
            }
            _dataSource = value;
            ReloadSource();
        }
    }

    /// <summary>
    /// The m int cell count
    /// </summary>
    private int _intCellCount;//单元格总数
    /// <summary>
    /// Gets the cell count.
    /// </summary>
    /// <value>The cell count.</value>
    [Description("单元格总数"), Category("自定义")]
    public int CellCount
    {
        get => _intCellCount;
        private set
        {
            _intCellCount = value;
            if (value <= 0 || _page == null) return;
            _page.PageSize = _intCellCount;
            _page.Reload();
        }
    }

    /// <summary>
    /// The m selected source
    /// </summary>
    private List<object> _selectedSource = new();

    /// <summary>
    /// Gets or sets the selected source.
    /// </summary>
    /// <value>The selected source.</value>
    [Description("选中的数据"), Category("自定义")]
    public List<object> SelectedSource
    {
        get => _selectedSource;
        set
        {
            _selectedSource = value;
            ReloadSource();
        }
    }

    /// <summary>
    /// Gets or sets a value indicating whether this instance is multiple.
    /// </summary>
    /// <value><c>true</c> if this instance is multiple; otherwise, <c>false</c>.</value>
    [Description("是否多选"), Category("自定义")]
    public bool IsMultiple { get; set; } = true;

    /// <summary>
    /// Occurs when [selected item event].
    /// </summary>
    [Description("选中项事件"), Category("自定义")]
    public event EventHandler? SelectedItemEvent;
    /// <summary>
    /// Delegate ReloadGridStyleEventHandle
    /// </summary>
    /// <param name="intCellCount">The int cell count.</param>
    public delegate void ReloadGridStyleEventHandle(int intCellCount);
    /// <summary>
    /// 样式改变事件
    /// </summary>
    [Description("样式改变事件"), Category("自定义")]
    public event ReloadGridStyleEventHandle? ReloadGridStyleEvent;
    /// <summary>
    /// Initializes a new instance of the <see cref="UCListView" /> class.
    /// </summary>
    public UxListView()
    {
        InitializeComponent();
    }
    /// <summary>
    /// ms the page show source changed.
    /// </summary>
    /// <param name="currentSource">The current source.</param>
    private void page_ShowSourceChanged(object currentSource)
    {
        DataSource = currentSource;
    }
    #region 重新加载数据源
    /// <summary>
    /// 功能描述:重新加载数据源
    /// </summary>
    public void ReloadSource()
    {
        try
        {
            if (DesignMode)
                return;
            ControlHelper.FreezeControl(this, true);

            if (panMain.Controls.Count <= 0)
            {
                ReloadGridStyle();
            }
            if (_dataSource == null || ((IList)_dataSource).Count <= 0)
            {
                for (var i = panMain.Controls.Count - 1; i >= 0; i--)
                {
                    panMain.Controls[i].Visible = false;
                }

                return;
            }
            var intCount = Math.Min(((IList)_dataSource).Count, panMain.Controls.Count);

            for (var i = 0; i < intCount; i++)
            {
                ((IListViewItem)panMain.Controls[i]).DataSource = ((IList)_dataSource)[i];
                ((IListViewItem)panMain.Controls[i]).SetSelected(_selectedSource.Contains(((IList)_dataSource)[i]));
                panMain.Controls[i].Visible = true;
            }

            for (var i = panMain.Controls.Count - 1; i >= intCount; i--)
            {
                if (panMain.Controls[i].Visible)
                    panMain.Controls[i].Visible = false;
            }

        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }
    }
    #endregion

    #region 刷新表格
    /// <summary>
    /// 功能描述:刷新表格样式
    /// </summary>
    public void ReloadGridStyle()
    {
        if (DesignMode)
            return;
        var frmMain = FindForm();
        if (frmMain is not { IsDisposed: false, Visible: true } || !Visible) return;
        GetCellCount();
        try
        {
            ControlHelper.FreezeControl(this, true);
            if (panMain.Controls.Count < _intCellCount)
            {
                var intControlsCount = panMain.Controls.Count;
                for (var i = 0; i < _intCellCount - intControlsCount; i++)
                {
                    var uc = (Control)Activator.CreateInstance(_itemType);
                    uc.Margin = new Padding(5, 5, 5, 5);

                    (uc as IListViewItem).SelectedItemEvent += UxListView_SelectedItemEvent;
                    uc.Visible = false;
                    panMain.Controls.Add(uc);
                }
            }
            else if (panMain.Controls.Count > _intCellCount)
            {
                var intControlsCount = panMain.Controls.Count;
                for (var i = intControlsCount - 1; i > _intCellCount - 1; i--)
                {
                    panMain.Controls.RemoveAt(i);
                }
            }
            foreach (Control item in panMain.Controls)
            {
                item.Size = new Size(_intCellWidth, _intCellHeight);
            }
        }
        finally
        {
            ControlHelper.FreezeControl(this, false);
        }

        ReloadGridStyleEvent?.Invoke(_intCellCount);

    }

    /// <summary>
    /// Handles the SelectedItemEvent event of the UCListView control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void UxListView_SelectedItemEvent(object sender, EventArgs e)
    {
        var selectedItem = sender as IListViewItem;

        if (_selectedSource.Contains(selectedItem.DataSource))
        {
            _selectedSource.Remove(selectedItem.DataSource);
            selectedItem.SetSelected(false);
        }
        else
        {
            if (IsMultiple)
            {
                _selectedSource.Add(selectedItem.DataSource);
                selectedItem.SetSelected(true);
            }
            else
            {
                if (_selectedSource.Count > 0)
                {
                    var intCount = Math.Min(((IList)_dataSource).Count, panMain.Controls.Count);
                    for (var i = 0; i < intCount; i++)
                    {
                        var item = ((IListViewItem)panMain.Controls[i]);
                        if (!_selectedSource.Contains(item.DataSource)) continue;
                        item.SetSelected(false);
                        break;
                    }
                }

                _selectedSource = new List<object> { selectedItem.DataSource };
                selectedItem.SetSelected(true);

            }
        }

        SelectedItemEvent?.Invoke(sender, e);
    }
    #endregion

    #region 获取cell总数
    /// <summary>
    /// 功能描述:获取cell总数
    /// </summary>
    private void GetCellCount()
    {
        if (DesignMode)
            return;
        if (panMain.Width == 0)
            return;
        var item = (Control)Activator.CreateInstance(_itemType);


        var intXCount = (panMain.Width - 10) / (item.Width + 10);
        _intCellWidth = item.Width + ((panMain.Width - 10) % (item.Width + 10)) / intXCount;

        var intYCount = (panMain.Height - 10) / (item.Height + 10);
        _intCellHeight = item.Height + ((panMain.Height - 10) % (item.Height + 10)) / intYCount;
        var intCount = intXCount * intYCount;

        if (Page == null)
        {
            if (_dataSource == null)
            {
                intCount = 0;
            }
            else
            {
                if (((IList)_dataSource).Count > intCount)
                {
                    intXCount = (panMain.Width - 10 - 20) / (item.Width + 10);
                    _intCellWidth = item.Width + ((panMain.Width - 10 - 20) % (item.Width + 10)) / intXCount;
                }
                intCount = Math.Max(intCount, ((IList)_dataSource).Count);
            }
        }

        CellCount = intCount;
    }
    #endregion

    /// <summary>
    /// Handles the Resize event of the panMain control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="EventArgs" /> instance containing the event data.</param>
    private void PanMain_Resize(object sender, EventArgs e)
    {
        ReloadGridStyle();
    }
}